using Microsoft;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vanara;
using Vanara.PInvoke;

namespace Fischless.Fetch.Unlocker;

/// <summary>
/// Credit to https://github.com/34736384/genshin-fps-unlock
/// </summary>
internal sealed class GameFpsUnlocker(Process gameProcess) : IGameFpsUnlocker
{
    private readonly Process gameProcess = gameProcess;
    private uint targetFps;
    private readonly UnlockerStatus status = new();

    public GameFpsUnlocker SetTargetFps(uint targetFps)
    {
        this.targetFps = targetFps;
        return this;
    }

    /// <inheritdoc/>
    public async ValueTask UnlockAsync(UnlockTimingOptions options, IProgress<UnlockerStatus>? progress = null, CancellationToken token = default)
    {
        Verify.Operation(status.IsUnlockerValid, "This Unlocker is invalid");

        (FindModuleResult result, GameModule moduleEntryInfo) = await FindModuleAsync(options.FindModuleDelay, options.FindModuleLimit).ConfigureAwait(false);
        Verify.Operation(result != FindModuleResult.TimeLimitExeeded, "Error finding required modules: timeout; please retry");
        Verify.Operation(result != FindModuleResult.NoModuleFound, "Error finding required modules: could not read any module, the protection driver may have been loaded; please retry");

        // Read UnityPlayer.dll
        UnsafeFindFpsAddress(moduleEntryInfo);
        progress?.Report(status);

        // When player switch between scenes, we have to re adjust the fps
        // So we keep a loop here
        await LoopAdjustFpsAsync(options.AdjustFpsDelay, progress, token).ConfigureAwait(false);
    }

    private static unsafe bool UnsafeReadModulesMemory(System.Diagnostics.Process process, in GameModule moduleEntryInfo, out VirtualMemory memory)
    {
        ref readonly Module unityPlayer = ref moduleEntryInfo.UnityPlayer;
        ref readonly Module userAssembly = ref moduleEntryInfo.UserAssembly;

        memory = new VirtualMemory(unityPlayer.Size + userAssembly.Size);
        return Kernel32X.ReadProcessMemory(process.Handle, unityPlayer.Address, memory.AsSpan()[..(int)unityPlayer.Size], out _)
            && Kernel32X.ReadProcessMemory(process.Handle, userAssembly.Address, memory.AsSpan()[(int)unityPlayer.Size..], out _);
    }

    private static unsafe bool UnsafeReadProcessMemory(Process process, nuint baseAddress, out nuint value)
    {
        value = 0;
        bool result = Kernel32X.ReadProcessMemory(process.Handle, baseAddress, ref value, out _);
        Verify.Operation(result, "Error reading process modules' memory: could not read valid value in given address");
        return result;
    }

    private static unsafe bool UnsafeWriteProcessMemory(Process process, nuint baseAddress, int value)
    {
        return Kernel32X.WriteProcessMemory(process.Handle, baseAddress, ref value, out _);
    }

    private static unsafe FindModuleResult UnsafeTryFindModule(in nint hProcess, in ReadOnlySpan<char> moduleName, out Module module)
    {
        HMODULE[] buffer = new HMODULE[128];
        if (!Kernel32X.K32EnumProcessModules(hProcess, buffer, out uint actualSize))
        {
            Marshal.ThrowExceptionForHR(Marshal.GetLastPInvokeError());
        }

        if (actualSize == 0)
        {
            module = default!;
            return FindModuleResult.NoModuleFound;
        }

        foreach (ref readonly HMODULE hModule in buffer.AsSpan()[..(int)(actualSize / sizeof(HMODULE))])
        {
            char[] baseName = new char[256];

            if (Kernel32X.K32GetModuleBaseNameW(hProcess, hModule, baseName) == 0)
            {
                continue;
            }

            fixed (char* lpBaseName = baseName)
            {
                ReadOnlySpan<char> szModuleName = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(lpBaseName);
                if (!szModuleName.SequenceEqual(moduleName))
                {
                    continue;
                }
            }

            if (!Kernel32X.K32GetModuleInformation(hProcess, hModule, out Kernel32.MODULEINFO moduleInfo))
            {
                continue;
            }

            module = new((nuint)moduleInfo.lpBaseOfDll, moduleInfo.SizeOfImage);
            return FindModuleResult.Ok;
        }

        module = default;
        return FindModuleResult.ModuleNotLoaded;
    }

    private static int IndexOfPattern(in ReadOnlySpan<byte> memory)
    {
        // B9 3C 00 00 00 FF 15
        ReadOnlySpan<byte> part = [0xB9, 0x3C, 0x00, 0x00, 0x00, 0xFF, 0x15];
        return memory.IndexOf(part);
    }

    private static FindModuleResult UnsafeGetGameModuleInfo(in nint hProcess, out GameModule info)
    {
        FindModuleResult unityPlayerResult = UnsafeTryFindModule(hProcess, "UnityPlayer.dll", out Module unityPlayer);
        FindModuleResult userAssemblyResult = UnsafeTryFindModule(hProcess, "UserAssembly.dll", out Module userAssembly);

        if (unityPlayerResult == FindModuleResult.Ok && userAssemblyResult == FindModuleResult.Ok)
        {
            info = new(unityPlayer, userAssembly);
            return FindModuleResult.Ok;
        }

        if (unityPlayerResult == FindModuleResult.NoModuleFound && userAssemblyResult == FindModuleResult.NoModuleFound)
        {
            info = default;
            return FindModuleResult.NoModuleFound;
        }

        info = default;
        return FindModuleResult.ModuleNotLoaded;
    }

    private async ValueTask<ValueResult<FindModuleResult, GameModule>> FindModuleAsync(TimeSpan findModuleDelay, TimeSpan findModuleLimit)
    {
        ValueStopwatch watch = ValueStopwatch.StartNew();
        using (PeriodicTimer timer = new(findModuleDelay))
        {
            while (await timer.WaitForNextTickAsync().ConfigureAwait(false))
            {
                FindModuleResult result = UnsafeGetGameModuleInfo(gameProcess.Handle, out GameModule gameModule);
                if (result == FindModuleResult.Ok)
                {
                    return new(FindModuleResult.Ok, gameModule);
                }

                if (result == FindModuleResult.NoModuleFound)
                {
                    return new(FindModuleResult.NoModuleFound, default);
                }

                if (watch.GetElapsedTime() > findModuleLimit)
                {
                    break;
                }
            }
        }

        return new(FindModuleResult.TimeLimitExeeded, default);
    }

    private async ValueTask LoopAdjustFpsAsync(TimeSpan adjustFpsDelay, IProgress<UnlockerStatus> progress, CancellationToken token)
    {
        using (PeriodicTimer timer = new(adjustFpsDelay))
        {
            while (await timer.WaitForNextTickAsync(token).ConfigureAwait(false))
            {
                if (!gameProcess.HasExited && status.FpsAddress != 0U)
                {
                    UnsafeWriteProcessMemory(gameProcess, status.FpsAddress, (int)targetFps);
                    progress?.Report(status);
                }
                else
                {
                    status.IsUnlockerValid = false;
                    status.FpsAddress = 0;
                    progress?.Report(status);
                    return;
                }
            }
        }
    }

    private unsafe void UnsafeFindFpsAddress(in GameModule moduleEntryInfo)
    {
        bool readOk = UnsafeReadModulesMemory(gameProcess, moduleEntryInfo, out VirtualMemory localMemory);
        Verify.Operation(readOk, "Error reading required modules' memory: could not copy module memory to destination");

        using (localMemory)
        {
            int offset = IndexOfPattern(localMemory.AsSpan()[(int)moduleEntryInfo.UnityPlayer.Size..]);
            Must.Range(offset >= 0, "Error matching memory pattern: no expected content");

            byte* pLocalMemory = (byte*)localMemory.Pointer;
            ref readonly Module unityPlayer = ref moduleEntryInfo.UnityPlayer;
            ref readonly Module userAssembly = ref moduleEntryInfo.UserAssembly;

            nuint localMemoryUnityPlayerAddress = (nuint)pLocalMemory;
            nuint localMemoryUserAssemblyAddress = localMemoryUnityPlayerAddress + unityPlayer.Size;

            nuint rip = localMemoryUserAssemblyAddress + (uint)offset;
            rip += 5U;
            rip += (nuint)(*(int*)(rip + 2U) + 6);

            nuint address = userAssembly.Address + (rip - localMemoryUserAssemblyAddress);

            nuint ptr = 0;
            SpinWait.SpinUntil(() => UnsafeReadProcessMemory(gameProcess, address, out ptr) && ptr != 0);

            rip = ptr - unityPlayer.Address + localMemoryUnityPlayerAddress;

            // CALL or JMP
            while (*(byte*)rip == 0xE8 || *(byte*)rip == 0xE9)
            {
                rip += (nuint)(*(int*)(rip + 1) + 5);
            }

            nuint localMemoryActualAddress = rip + *(uint*)(rip + 2) + 6;
            nuint actualOffset = localMemoryActualAddress - localMemoryUnityPlayerAddress;
            status.FpsAddress = unityPlayer.Address + actualOffset;
        }
    }

    private readonly struct GameModule
    {
        public readonly bool HasValue = false;
        public readonly Module UnityPlayer;
        public readonly Module UserAssembly;

        public GameModule(in Module unityPlayer, in Module userAssembly)
        {
            HasValue = true;
            UnityPlayer = unityPlayer;
            UserAssembly = userAssembly;
        }
    }

    private readonly struct Module(nuint address, uint size)
    {
        public readonly bool HasValue = true;
        public readonly nuint Address = address;
        public readonly uint Size = size;
    }
}

file static class UnmanagedMemoryExtension
{
    public static unsafe Span<byte> AsSpan(this VirtualMemory unmanagedMemory)
    {
        return new(unmanagedMemory.Pointer, (int)unmanagedMemory.Size);
    }
}

file static class Must
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Range([DoesNotReturnIf(false)] bool condition, string? message, [CallerArgumentExpression(nameof(condition))] string? parameterName = null)
    {
        if (!condition)
        {
            throw new ArgumentOutOfRangeException(parameterName, message);
        }
    }
}

file static class Kernel32X
{
    [DebuggerStepThrough]
    public static unsafe BOOL ReadProcessMemory(nint hProcess, nuint lpBaseAddress, Span<byte> buffer, [MaybeNull] out SizeT numberOfBytesRead)
    {
        fixed (byte* lpBuffer = buffer)
        {
            return Kernel32.ReadProcessMemory(hProcess, (nint)lpBaseAddress, (nint)lpBuffer, buffer.Length, out numberOfBytesRead);
        }
    }

    [DebuggerStepThrough]
    public static unsafe BOOL ReadProcessMemory<T>(nint hProcess, nuint lpBaseAddress, ref T buffer, [MaybeNull] out SizeT numberOfBytesRead)
        where T : unmanaged
    {
        fixed (T* lpBuffer = &buffer)
        {
            return Kernel32.ReadProcessMemory(hProcess, (nint)lpBaseAddress, (nint)lpBuffer, sizeof(T), out numberOfBytesRead);
        }
    }

    [DebuggerStepThrough]
    public static unsafe BOOL WriteProcessMemory<T>(nint hProcess, nuint lpBaseAddress, ref readonly T buffer, out SizeT numberOfBytesWritten)
        where T : unmanaged
    {
        fixed (T* lpBuffer = &buffer)
        {
            return Kernel32.WriteProcessMemory(hProcess, (nint)lpBaseAddress, (nint)lpBuffer, (uint)sizeof(T), out numberOfBytesWritten);
        }
    }

    [DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Winapi, ExactSpelling = true)]
    public static unsafe extern BOOL K32EnumProcessModules(HANDLE hProcess, HMODULE* lphModule, uint cb, uint* lpcbNeeded);

    [DebuggerStepThrough]
    public static unsafe BOOL K32EnumProcessModules(nint hProcess, Span<HMODULE> hModules, out uint cbNeeded)
    {
        fixed (HMODULE* lphModule = hModules)
        {
            fixed (uint* lpcbNeeded = &cbNeeded)
            {
                return K32EnumProcessModules(hProcess, lphModule, (uint)(hModules.Length * sizeof(HMODULE)), lpcbNeeded);
            }
        }
    }

    internal readonly struct PWSTR
    {
        public readonly unsafe char* Value;

        public static unsafe implicit operator PWSTR(char* value) => *(PWSTR*)&value;

        public static unsafe implicit operator char*(PWSTR value) => *(char**)&value;
    }

    [DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern uint K32GetModuleBaseNameW(HANDLE hProcess, [AllowNull] HMODULE hModule, PWSTR lpBaseName, uint nSize);

    [DebuggerStepThrough]
    public static unsafe uint K32GetModuleBaseNameW(HANDLE hProcess, [AllowNull] HMODULE hModule, Span<char> baseName)
    {
        fixed (char* lpBaseName = baseName)
        {
            return K32GetModuleBaseNameW(hProcess, hModule, lpBaseName, (uint)baseName.Length);
        }
    }

    [DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Winapi, ExactSpelling = true)]
    public static unsafe extern BOOL K32GetModuleInformation(HANDLE hProcess, HMODULE hModule, Kernel32.MODULEINFO* lpmodinfo, uint cb);

    [DebuggerStepThrough]
    public static unsafe BOOL K32GetModuleInformation(HANDLE hProcess, HMODULE hModule, out Kernel32.MODULEINFO modinfo)
    {
        fixed (Kernel32.MODULEINFO* lpmodinfo = &modinfo)
        {
            return K32GetModuleInformation(hProcess, hModule, lpmodinfo, (uint)sizeof(Kernel32.MODULEINFO));
        }
    }
}

internal readonly struct HMODULE
{
    public readonly nint Value;
}

internal readonly struct ValueResult<TResult, TValue>(TResult isOk, TValue value)
{
    public readonly TResult IsOk = isOk;

    public readonly TValue Value = value;

    public void Deconstruct(out TResult isOk, out TValue value)
    {
        isOk = IsOk;
        value = Value;
    }
}

internal readonly struct ValueStopwatch
{
    private readonly long startTimestamp;

    private ValueStopwatch(long startTimestamp)
    {
        this.startTimestamp = startTimestamp;
    }

    public bool IsActive
    {
        get => startTimestamp != 0;
    }

    public static ValueStopwatch StartNew()
    {
        return new(Stopwatch.GetTimestamp());
    }

    public TimeSpan GetElapsedTime()
    {
        return Stopwatch.GetElapsedTime(startTimestamp);
    }
}
