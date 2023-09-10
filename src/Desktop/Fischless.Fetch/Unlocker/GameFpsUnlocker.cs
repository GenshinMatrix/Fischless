// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using System.Diagnostics;
using System.Runtime.InteropServices;
using Vanara.PInvoke;
using static Vanara.PInvoke.Kernel32;

namespace Fischless.Fetch.Unlocker;

#pragma warning disable CS8500

public sealed class GameFpsUnlocker : IGameFpsUnlocker
{
    private readonly Process gameProcess;

    private nuint fpsAddress;
    private bool isValid = true;

    public GameFpsUnlocker(Process gameProcess)
    {
        this.gameProcess = gameProcess;
    }

    public int TargetFps { get; set; } = 60;

    public async Task UnlockAsync(UnlockTimingOptions options)
    {
        if (!isValid)
        {
            throw new InvalidOperationException("This Unlocker is invalid.");
        }

        GameModuleEntryInfo moduleEntryInfo = await FindModuleAsync(options.FindModuleDelay, options.FindModuleLimit).ConfigureAwait(false);

        if (!moduleEntryInfo.HasValue)
        {
            throw new InvalidOperationException("Unable to find UnityPlayer and UserAssembly.");
        }

        // Read UnityPlayer.dll
        UnsafeTryReadModuleMemoryFindFpsAddress(moduleEntryInfo);

        // When player switch between scenes, we have to re adjust the fps
        // So we keep a loop here
        await LoopAdjustFpsAsync(options.AdjustFpsDelay).ConfigureAwait(false);
    }

    private static unsafe bool UnsafeReadModulesMemory(Process process, in GameModuleEntryInfo moduleEntryInfo, out VirtualMemory memory)
    {
        MODULEENTRY32 unityPlayer = moduleEntryInfo.UnityPlayer;
        MODULEENTRY32 userAssembly = moduleEntryInfo.UserAssembly;

        memory = new VirtualMemory(unityPlayer.modBaseSize + userAssembly.modBaseSize);
        byte* lpBuffer = (byte*)memory.Pointer;
        return ReadProcessMemory((HPROCESS)process.Handle, unityPlayer.modBaseAddr, (nint)lpBuffer, unityPlayer.modBaseSize, out _)
            && ReadProcessMemory((HPROCESS)process.Handle, userAssembly.modBaseAddr, (nint)lpBuffer + (nint)unityPlayer.modBaseSize, userAssembly.modBaseSize, out _);
    }

    private static unsafe bool UnsafeReadProcessMemory(Process process, nuint baseAddress, out nuint value)
    {
        ulong temp = 0;
        bool result = ReadProcessMemory((HPROCESS)process.Handle, (nint)baseAddress, (nint)(&temp), 8, out _);
        if (!result)
        {
            throw new InvalidOperationException("ReadProcessMemory failed");
        }

        value = (nuint)temp;
        return result;
    }

    private static unsafe bool UnsafeWriteProcessMemory(Process process, nuint baseAddress, int write)
    {
        return WriteProcessMemory((HPROCESS)process.Handle, (nint)baseAddress, (nint)write, sizeof(int), out _);
    }

    private static unsafe MODULEENTRY32 UnsafeFindModule(int processId, in ReadOnlySpan<byte> moduleName)
    {
        TH32CS flags = TH32CS.TH32CS_SNAPMODULE | TH32CS.TH32CS_SNAPMODULE32;
        HSNAPSHOT snapshot = CreateToolhelp32Snapshot(flags, (uint)processId);
        try
        {
            Marshal.ThrowExceptionForHR(Marshal.GetLastPInvokeError());
            foreach (MODULEENTRY32 entry in StructMarshal.EnumerateModuleEntry32(snapshot))
            {
                ReadOnlySpan<byte> szModuleNameLocal = MemoryMarshal.CreateReadOnlySpanFromNullTerminated((byte*)&entry.szModule);
                if (entry.th32ProcessID == processId && szModuleNameLocal.SequenceEqual(moduleName))
                {
                    return entry;
                }
            }

            return default;
        }
        finally
        {
            CloseHandle(snapshot);
        }
    }

    private static int IndexOfPattern(in ReadOnlySpan<byte> memory)
    {
        // E8 ?? ?? ?? ?? 85 C0 7E 07 E8 ?? ?? ?? ?? EB 05
        int second = 0;
        ReadOnlySpan<byte> secondPart = new byte[] { 0x85, 0xC0, 0x7E, 0x07, 0xE8, };
        ReadOnlySpan<byte> thirdPart = new byte[] { 0xEB, 0x05, };

        while (second >= 0 && second < memory.Length)
        {
            second += memory[second..].IndexOf(secondPart);
            if (memory[second - 5].Equals(0xE8) && memory.Slice(second + 9, 2).SequenceEqual(thirdPart))
            {
                return second - 5;
            }

            second += 5;
        }

        return -1;
    }

    private static unsafe GameModuleEntryInfo UnsafeGetGameModuleEntryInfo(int processId)
    {
        MODULEENTRY32 unityPlayer = UnsafeFindModule(processId, "UnityPlayer.dll"u8);
        MODULEENTRY32 userAssembly = UnsafeFindModule(processId, "UserAssembly.dll"u8);

        if (unityPlayer.modBaseSize != 0 && userAssembly.modBaseSize != 0)
        {
            return new(unityPlayer, userAssembly);
        }

        return default;
    }

    private async Task<GameModuleEntryInfo> FindModuleAsync(TimeSpan findModuleDelay, TimeSpan findModuleLimit)
    {
        ValueStopwatch watch = ValueStopwatch.StartNew();
        using (PeriodicTimer timer = new(findModuleDelay))
        {
            while (await timer.WaitForNextTickAsync().ConfigureAwait(false))
            {
                GameModuleEntryInfo moduleInfo = UnsafeGetGameModuleEntryInfo(gameProcess.Id);
                if (moduleInfo.HasValue)
                {
                    return moduleInfo;
                }

                if (watch.GetElapsedTime() > findModuleLimit)
                {
                    break;
                }
            }
        }

        return default;
    }

    private async Task LoopAdjustFpsAsync(TimeSpan adjustFpsDelay)
    {
        using (PeriodicTimer timer = new(adjustFpsDelay))
        {
            while (await timer.WaitForNextTickAsync().ConfigureAwait(false))
            {
                if (!gameProcess.HasExited && fpsAddress != 0)
                {
                    UnsafeWriteProcessMemory(gameProcess, fpsAddress, TargetFps);
                }
                else
                {
                    isValid = false;
                    fpsAddress = 0;
                    return;
                }
            }
        }
    }

    private unsafe void UnsafeTryReadModuleMemoryFindFpsAddress(in GameModuleEntryInfo moduleEntryInfo)
    {
        bool readOk = UnsafeReadModulesMemory(gameProcess, moduleEntryInfo, out VirtualMemory localMemory);
        if (!readOk)
        {
            throw new InvalidOperationException("UnsafeReadModulesMemory failed");
        }

        using (localMemory)
        {
            int offset = IndexOfPattern(localMemory.GetBuffer()[(int)moduleEntryInfo.UnityPlayer.modBaseSize..]);

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("Match pattern failed");
            }

            byte* pLocalMemory = (byte*)localMemory.Pointer;
            MODULEENTRY32 unityPlayer = moduleEntryInfo.UnityPlayer;
            MODULEENTRY32 userAssembly = moduleEntryInfo.UserAssembly;

            nuint localMemoryUnityPlayerAddress = (nuint)pLocalMemory;
            nuint localMemoryUserAssemblyAddress = localMemoryUnityPlayerAddress + unityPlayer.modBaseSize;

            nuint rip = localMemoryUserAssemblyAddress + (uint)offset;
            rip += *(uint*)(rip + 1) + 5;
            rip += *(uint*)(rip + 3) + 7;

            nuint address = (nuint)userAssembly.modBaseAddr + (rip - localMemoryUserAssemblyAddress);

            nuint ptr = 0;
            SpinWait.SpinUntil(() => UnsafeReadProcessMemory(gameProcess, address, out ptr) && ptr != 0);

            rip = ptr - (nuint)unityPlayer.modBaseAddr + localMemoryUnityPlayerAddress;
            while (*(byte*)rip == 0xE8 || *(byte*)rip == 0xE9)
            {
                rip += (nuint)(*(int*)(rip + 1) + 5);
            }

            nuint localMemoryActualAddress = rip + *(uint*)(rip + 2) + 6;
            nuint actualOffset = localMemoryActualAddress - localMemoryUnityPlayerAddress;
            fpsAddress = (nuint)unityPlayer.modBaseAddr + actualOffset;
        }
    }

    private readonly struct GameModuleEntryInfo
    {
        public readonly bool HasValue = false;
        public readonly MODULEENTRY32 UnityPlayer;
        public readonly MODULEENTRY32 UserAssembly;

        public GameModuleEntryInfo(MODULEENTRY32 unityPlayer, MODULEENTRY32 userAssembly)
        {
            HasValue = true;
            UnityPlayer = unityPlayer;
            UserAssembly = userAssembly;
        }
    }
}
