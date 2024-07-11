using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Vanara.PInvoke;

namespace Fischless.Fetch.Unlocker;

internal class GameFpsUnlockerImpl
{
    private const int STILL_ACTIVE = 0x00000103;

    private class DeferManager(List<Action> deferredActions) : IDisposable
    {
        private readonly List<Action> deferredActions = deferredActions;

        public DeferManager() : this([])
        {
        }

        public void Defer(Action action)
        {
            deferredActions.Add(action);
        }

        public void Dispose()
        {
            deferredActions.ForEach(action => action?.Invoke());
            deferredActions.Clear();
        }
    }

    private static class Interop
    {
        public static string GetLastErrorAsString(Win32Error errorCode)
        {
            StringBuilder messageBuffer = new(256);
            int formatResult = Kernel32.FormatMessage(
                Kernel32.FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM | Kernel32.FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS,
                IntPtr.Zero,
                (uint)errorCode,
                0,
                messageBuffer,
                (uint)messageBuffer.Capacity,
                IntPtr.Zero
            );

            if (formatResult == NTStatus.STATUS_SUCCESS)
            {
                return $"Unknown error (Code {errorCode})";
            }

            return messageBuffer.ToString().Trim();
        }
    }

    private static class Extension
    {
        public static int[] ToPatternArray(string pattern)
        {
            return pattern.Split(' ').Select(p => p == "??" ? -1 : byte.Parse(p, NumberStyles.HexNumber, CultureInfo.InvariantCulture)).ToArray();
        }
    }

    private static bool GetModule2(Kernel32.SafeHPROCESS hProcess, string moduleName, out Kernel32.MODULEENTRY32 pEntry)
    {
        pEntry = new Kernel32.MODULEENTRY32 { dwSize = (uint)Marshal.SizeOf<Kernel32.MODULEENTRY32>() };
        HINSTANCE[] modules = new HINSTANCE[1024];

        if (!Kernel32.EnumProcessModules(hProcess, modules, (uint)(modules.Length * Marshal.SizeOf<HINSTANCE>()), out uint cbNeeded))
        {
            return false;
        }

        Array.Resize(ref modules, (int)(cbNeeded / IntPtr.Size));

        foreach (HINSTANCE module in modules)
        {
            StringBuilder szModuleName = new(Kernel32.MAX_PATH);

            if (Kernel32.GetModuleBaseName(hProcess, module, szModuleName, (uint)szModuleName.Capacity) == 0)
            {
                continue;
            }

            if (moduleName != szModuleName.ToString())
            {
                continue;
            }

            if (Kernel32.GetModuleInformation(hProcess, module, out Kernel32.MODULEINFO modInfo, (uint)Marshal.SizeOf<Kernel32.MODULEINFO>()))
            {
                pEntry.modBaseAddr = modInfo.lpBaseOfDll;
                pEntry.modBaseSize = modInfo.SizeOfImage;
                return true;
            }
        }
        return false;
    }

    private static unsafe nint PatternScan(nint module, uint dataLength, string pattern)
    {
        byte* scanBytes = (byte*)module;
        int[] d = Extension.ToPatternArray(pattern);
        ulong s = (ulong)d.Length;

        for (ulong i = 0ul; i < dataLength - s; ++i)
        {
            bool found = true;
            for (ulong j = 0ul; j < s; ++j)
            {
                if (scanBytes[i + j] != d[j] && d[j] != -1)
                {
                    found = false;
                    break;
                }
            }
            if (found)
            {
                return IntPtr.Add(module, (int)i);
            }
        }
        return IntPtr.Zero;
    }

    private static nint InjectPatch(nint unityModule, nint unityBaseAdd, nint fpsPtr, nint tarHandle)
    {
        return IntPtr.Zero;
    }

    public static unsafe void Start(GameFpsUnlockerOption option, string? gamePath = null, uint? pid = null, CancellationTokenSource? cts = null)
    {
        if (!option.TargetFps.HasValue)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(gamePath) && pid == null)
        {
            return;
        }

        int targetFps = option.TargetFps.Value;
        using DeferManager deferManager = new();

        Kernel32.SafeHPROCESS hProcess = null!;

        if (pid == null)
        {
            hProcess = Kernel32.CreateProcess(gamePath!);
        }
        else
        {
            hProcess = Kernel32.OpenProcess(new ACCESS_MASK(Kernel32.ProcessAccess.PROCESS_ALL_ACCESS), false, pid.Value);
        }

        if (hProcess.IsInvalid)
        {
            Debug.WriteLine($"[Unlocker] CreateProcess failed with {gamePath}");
            return;
        }
        else
        {
            deferManager.Defer(() => { using (hProcess) { } });
        }

        int foundLimit = 0;
        Kernel32.MODULEENTRY32 hUnityPlayer;
        Thread.Sleep(option.FindModuleDelay);
        while (!GetModule2(hProcess, "UnityPlayer.dll", out hUnityPlayer))
        {
            if (cts?.Token.IsCancellationRequested ?? false)
            {
                return;
            }

            foundLimit += option.FindModuleDelay;
            if (foundLimit > option.FindModuleLimit)
            {
                Debug.WriteLine($"[Unlocker] GetModule2 failed in {option.FindModuleLimit} ms");
                break;
            }

            Thread.Sleep(option.FindModuleDelay);
        }

        Debug.WriteLine($"[Unlocker] UnityPlayer: {hUnityPlayer.modBaseAddr.ToInt64()}");

        nint up = Kernel32.VirtualAlloc(IntPtr.Zero, hUnityPlayer.modBaseSize, Kernel32.MEM_ALLOCATION_TYPE.MEM_COMMIT | Kernel32.MEM_ALLOCATION_TYPE.MEM_RESERVE, Kernel32.MEM_PROTECTION.PAGE_READWRITE);

        if (up == IntPtr.Zero)
        {
            Win32Error code = Kernel32.GetLastError();
            Debug.WriteLine($"[Unlocker] VirtualAlloc UP failed ({code}): {Interop.GetLastErrorAsString(code)}");
            return;
        }
        else
        {
            deferManager.Defer(() => Kernel32.VirtualFree(up, hUnityPlayer.modBaseSize, Kernel32.MEM_ALLOCATION_TYPE.MEM_COMMIT | Kernel32.MEM_ALLOCATION_TYPE.MEM_RESERVE));
        }

        if (!Kernel32.ReadProcessMemory(hProcess, hUnityPlayer.modBaseAddr, up, hUnityPlayer.modBaseSize, out _))
        {
            Win32Error code = Kernel32.GetLastError();
            Debug.WriteLine($"[Unlocker] ReadProcessMemory unity failed ({code}): {Interop.GetLastErrorAsString(code)}");
            return;
        }

        Debug.WriteLine("[Unlocker] Searching for pattern...");

        nint address = PatternScan(up, hUnityPlayer.modBaseSize, "7F 0E E8 ?? ?? ?? ?? 66 0F 6E C8");

        if (address == IntPtr.Zero)
        {
            Debug.WriteLine("[Unlocker] outdated pattern");
            return;
        }

        nint pfps = 0;
        {
            nint rip = address;
            rip += 3;
            rip += *(int*)(rip) + 6;
            rip += *(int*)(rip) + 4;
            pfps = rip - up + hUnityPlayer.modBaseAddr;
            Debug.WriteLine($"[Unlocker] FPS Offset: {pfps}");
        }
        nint patchPtr = 0;
        {
            patchPtr = InjectPatch(up, hUnityPlayer.modBaseAddr, pfps, hProcess.DangerousGetHandle());
            if (patchPtr == IntPtr.Zero)
            {
                Debug.WriteLine("[Unlocker] Inject Patch Fail!");
            }
        }

        uint exitCode = STILL_ACTIVE;
        while (exitCode == STILL_ACTIVE)
        {
            if (cts?.Token.IsCancellationRequested ?? false)
            {
                return;
            }

            Kernel32.GetExitCodeProcess(hProcess, out exitCode);
            Thread.Sleep(option.FpsDelay);

            int fps = 0;
            Kernel32.ReadProcessMemory(hProcess, pfps, new IntPtr(&fps), sizeof(int), out _);
            if (fps == -1)
            {
                continue;
            }
            if (fps != targetFps)
            {
                Kernel32.WriteProcessMemory(hProcess, pfps, new IntPtr(&targetFps), sizeof(int), out _);
            }
        }
    }
}
