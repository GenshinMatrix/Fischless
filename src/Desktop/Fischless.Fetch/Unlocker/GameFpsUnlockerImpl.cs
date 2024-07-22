using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Vanara.PInvoke;

namespace Fischless.Fetch.Unlocker;

internal class GameFpsUnlockerImpl
{
    private const int STILL_ACTIVE = 0x00000103;

    private static byte[] _shellcode_genshin =
    [
        0x00, 0x00, 0x00, 0x00,                         // uint32_t unlocker_pid            _shellcode_genshin[0]
        0x00, 0x00, 0x00, 0x00,                         // uint32_t unlocker_Handle         _shellcode_genshin[4]
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //DWORD64 unlocker_FpsValue_addr    _shellcode_genshin[8]
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //DWORD64 API_OpenProcess           _shellcode_genshin[16]
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //DWORD64 API_ReadProcessmem        _shellcode_genshin[24]
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //DWORD64 API_Sleep                 _shellcode_genshin[32]
        0x00, 0x00, 0x00, 0x00,                         //uint32_t Readmem_buffer           _shellcode_genshin[40]
        0xCC, 0xCC, 0xCC, 0xCC, //int3
        0x48, 0x83, 0xEC, 0x38,                 //sub rsp,0x38                              _shellcode_genshin[48] _sync_thread
        0x8B, 0x05, 0xC6, 0xFF, 0xFF, 0xFF,     //mov eax,dword[unlocker_pid]
        0x85, 0xC0,                             //test eax
        0x74, 0x5B,                             //je return
        0x41, 0x89, 0xC0,                       //mov r8d,eax
        0x33, 0xD2,                             //xor edx,edx
        0xB9, 0xFF, 0xFF, 0x1F, 0x00,           //mov ecx,1FFFFF
        0xFF, 0x15, 0xC2, 0xFF, 0xFF, 0xFF,     //call [API_OpenProcess]
        0x85, 0xC0,                             //test eax
        0x74, 0x47,                             //je return
        0x89, 0x05, 0xAC, 0xFF, 0xFF, 0xFF,     //mov dword[unlocker_Handle],eax
        0x89, 0xC6,                             //mov esi,eax
        0x48, 0x8B, 0x3D, 0xA7, 0xFF, 0xFF, 0xFF,//mov rdi,qword[unlocker_FpsValue_addr]
        0x0F, 0x1F, 0x00,                       //nop
        0x89, 0xF1,                             //mov ecx,esi   //Read_tar_fps
        0x48, 0x89, 0xFA,                       //mov rdx,rdi
        0x4C, 0x8D, 0x05, 0xB8, 0xFF, 0xFF, 0xFF,//lea r8,qword[Readmem_buffer]
        0x41, 0xB9, 0x04, 0x00, 0x00, 0x00,     //mov r9d,4
        0x31, 0xC0,                             //xor eax,eax
        0x48, 0x89, 0x44, 0x24, 0x20,           //mov qword ptr ss:[rsp+20],rax
        0xFF, 0x15, 0x95, 0xFF, 0xFF, 0xFF,     //call [API_ReadProcessmem]
        0x85, 0xC0,                             //test eax
        0x74, 0x12,                             //jz return
        0xB9, 0xE8, 0x03, 0x00, 0x00,           //mov ecx,0x3E8 (1000ms)
        0xFF, 0x15, 0x8E, 0xFF, 0xFF, 0xFF,     //call [API_Sleep]
        0xE8, 0x49, 0x00, 0x00, 0x00,           //call Sync_Set
        0xEB, 0xCB,                             //jmp Read_tar_fps
        0x48, 0x83, 0xC4, 0x38,                 //add rsp,0x38
        0xC3,                                   //ret
        0xCC, 0xCC,       //int3
        0x89, 0x0D, 0x22, 0x00, 0x00, 0x00,     //mov [Game_Current_set], ecx       //hook_fps_set      _shellcode_genshin[160]
        0xEB, 0x00,                             //nop
        0x83, 0xF9, 0x1E,                       //cmp ecx, 0x1E
        0x74, 0x0C,                             //je set 60
        0x83, 0xF9, 0x2D,                       //cmp ecx, 0x2D
        0x74, 0x12,                             //je return
        0xB9, 0xFF, 0xFF, 0xFF, 0xFF,           //mov ecx,[Readmem_buffer]
        0xEB, 0x05,                             //jmp set
        0xB9, 0x3C, 0x00, 0x00, 0x00,           //mov ecx,0x3C
        0x89, 0x0D, 0x0D, 0x00, 0x00, 0x00,     //mov [hook_fps_get + 1],ecx
        0xC3,                                   //ret
        0xCC, 0xCC, 0xCC,        //int3
        0x00, 0x00, 0x00, 0x00,                 //uint32_t Game_Current_set
        0xCC, 0xCC, 0xCC, 0xCC,  //int3
        0xB8,0x78, 0x00, 0x00, 0x00,            //mov eax,0x78                      //hook_fps_get      _shellcode_genshin[208]
        0xC3,                                   //ret
        0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC,//int3
        0x56,                                   //push rsi                          //Sync_Set
        0x57,                                   //push rdi
        0x48, 0x83, 0xEC, 0x18,                 //sub rsp, 0x18
        0x8B, 0x05, 0xDC, 0xFF, 0xFF, 0xFF,     //mov eax, dword[Game_Current_set]
        0x83, 0xF8, 0x2D,                       //cmp eax, 0x2D
        0x75, 0x0C,                             //jne return
        0x8B, 0x05, 0x31, 0xFF, 0xFF, 0xFF,     //mov eax, dword[Game_Current_set]
        0x89, 0x05, 0xD4, 0xFF, 0xFF, 0xFF,     //mov dword[hook_fps_get + 1], eax
        0x48, 0x83, 0xC4, 0x18,                 //add rsp, 0x18
        0x5F,                                   //pop rdi
        0x5E,                                   //pop rsi
        0xC3,                                   //ret
        0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC
    ];

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

        [DllImport(Lib.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern Kernel32.SafeHTHREAD CreateRemoteThread(HPROCESS hProcess, SECURITY_ATTRIBUTES lpThreadAttributes, SizeT dwStackSize, nint lpStartAddress, IntPtr lpParameter, Kernel32.CREATE_THREAD_FLAGS dwCreationFlags, out uint lpThreadId);

        [DllImport("Fischless.UnityPatch.dll", EntryPoint = "inject_patch")]
        public static extern nint InjectPatch(nint unity_module, nint unity_baseaddr, nint _ptr_fps, nint Tar_handle, int FpsValue);
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

    private static unsafe nint InjectPatch(nint unityModule, uint dataLength, nint unityBaseAddr, nint fpsPtr, nint tarHandle, int fps)
    {
        nint moduleTarSecRVA = unityModule;
        uint moduleTarSecSize = dataLength;

        goto __GetTargetSec;

        Debug.WriteLine("Get Target Section Fail !");
        return 0;

    __GetTargetSec:
        nint address = 0;
        {
            nint hookAddrFpsGet = 0;   // in buffer
            nint hookAddrTarFpsGet = 0;
            nint hookAddrFpsSet = 0;   // in buffer
            nint hookAddrTarFpsSet = 0;
            nint addrTarFpsGetTarFun = 0;
            nint addrTarFpsSetTarFun = 0;

            // get_fps
            while ((address = PatternScan(moduleTarSecRVA, moduleTarSecSize, "CC 8B 05 ?? ?? ?? ?? C3 CC")) != 0)
            {
                nint rip = address;
                rip += 3;
                rip += Marshal.ReadInt32(rip) + 4;

                if ((rip - unityModule + unityBaseAddr) == fpsPtr)
                {
                    hookAddrFpsGet = address + 1;
                    hookAddrTarFpsGet = hookAddrFpsGet - unityModule + unityBaseAddr;
                    goto __GetFpsGetAddr;
                }
                else
                {
                    Marshal.WriteInt64(address + 1, unchecked((long)0xCCCCCCCCCCCCCCCC));
                }
            }

            Debug.WriteLine("Patch pattern1 outdate...");
            return 0;

        __GetFpsGetAddr:
            // set_fps
            while ((address = PatternScan(moduleTarSecRVA, moduleTarSecSize, "CC 89 0D ?? ?? ?? ?? C3 CC")) != 0)
            {
                nint rip = address;
                rip += 3;
                rip += Marshal.ReadInt32(rip) + 4;

                if ((rip - unityModule + unityBaseAddr) == fpsPtr)
                {
                    hookAddrFpsSet = address + 1;
                    hookAddrTarFpsSet = hookAddrFpsSet - unityModule + unityBaseAddr;
                    goto __GetFpsSetAddr;
                }
                else
                {
                    Marshal.WriteInt64(address + 1, unchecked((long)0xCCCCCCCCCCCCCCCC));
                }
            }

            Debug.WriteLine("Patch pattern2 outdate...");
            return 0;

        __GetFpsSetAddr:
            nint addrOpenProcess = 0;
            nint addrReadProcessMem = 0;
            nint addrSleep = 0;

            // get API OpenProcess
            if ((address = PatternScan(moduleTarSecRVA, moduleTarSecSize, "33 D2 B9 00 04 00 00 FF 15 ?? ?? ?? ??")) != 0)
            {
                nint rip = address;
                rip += 9;
                rip += Marshal.ReadInt32(rip) + 4;

                if (Marshal.ReadInt64(rip) == 0)
                {
                    rip = rip - unityModule + unityBaseAddr;

                    while (addrOpenProcess == 0)
                    {
                        if (!Kernel32.ReadProcessMemory(tarHandle, rip, addrOpenProcess, 8, out _))
                        {
                            int errCode = Marshal.GetLastWin32Error();
                            Debug.WriteLine($"Get Target Openprocess API Fail! ( 0x{errCode:X} )");
                            return 0;
                        }
                    }
                }
                else
                {
                    addrOpenProcess = (nint)Marshal.ReadInt64(rip);
                }
            }

            // get API ReadProcmem
            if ((address = PatternScan(moduleTarSecRVA, moduleTarSecSize, "48 89 44 24 20 FF 15 ?? ?? ?? ?? 48 8B 54 24 70")) != 0)
            {
                nint rip = address;
                rip += 7;
                rip += Marshal.ReadInt32(rip) + 4;

                if (Marshal.ReadInt64(rip) == 0)
                {
                    rip = rip - unityModule + unityBaseAddr;

                    while (addrReadProcessMem == 0)
                    {
                        if (!Kernel32.ReadProcessMemory(tarHandle, rip, addrReadProcessMem, 8, out _))
                        {
                            int errCode = Marshal.GetLastWin32Error();
                            Debug.WriteLine($"Get Target Readprocmem API Fail! ( 0x{errCode:X} )");
                            return 0;
                        }
                    }
                }
                else
                {
                    addrReadProcessMem = (nint)Marshal.ReadInt64(rip);
                }
            }

            // get API Sleep
            if ((address = PatternScan(moduleTarSecRVA, moduleTarSecSize, "41 8B C8 FF 15 ?? ?? ?? ?? 8B C7")) != 0)
            {
                nint rip = address;
                rip += 5;
                rip += Marshal.ReadInt32(rip) + 4;

                if (Marshal.ReadInt64(rip) == 0)
                {
                    rip = rip - unityModule + unityBaseAddr;

                    while (addrSleep == 0)
                    {
                        if (!Kernel32.ReadProcessMemory(tarHandle, rip, addrSleep, 8, out _))
                        {
                            int errCode = Marshal.GetLastWin32Error();
                            Debug.WriteLine($"Get Target Sleep API Fail! ( 0x{errCode:X} )");
                            return 0;
                        }
                    }
                }
                else
                {
                    addrSleep = (nint)Marshal.ReadInt64(rip);
                }
            }

            // Set shellcode parameters
            BitConverter.GetBytes(Kernel32.GetCurrentProcessId()).CopyTo(_shellcode_genshin, 0);
            BitConverter.GetBytes((ulong)(&fps)).CopyTo(_shellcode_genshin, 8);
            BitConverter.GetBytes(addrOpenProcess).CopyTo(_shellcode_genshin, 16);
            BitConverter.GetBytes(addrReadProcessMem).CopyTo(_shellcode_genshin, 24);
            BitConverter.GetBytes(addrSleep).CopyTo(_shellcode_genshin, 32);

            var tarProcBuffer = Kernel32.VirtualAllocEx(tarHandle, IntPtr.Zero, new IntPtr(0x1000), Kernel32.MEM_ALLOCATION_TYPE.MEM_COMMIT | Kernel32.MEM_ALLOCATION_TYPE.MEM_RESERVE, Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READWRITE);
            if (tarProcBuffer != IntPtr.Zero)
            {
                if (Kernel32.WriteProcessMemory(tarHandle, tarProcBuffer, _shellcode_genshin, (uint)_shellcode_genshin.Length, out _))
                {
                    addrTarFpsSetTarFun = tarProcBuffer + 160;
                    addrTarFpsGetTarFun = tarProcBuffer + 208;
                    Marshal.WriteInt64((nint)hookAddrFpsGet, unchecked((long)0xCCCCCCCCCCCCCCCC));
                    Marshal.WriteInt64((nint)hookAddrFpsSet, unchecked((long)0xCCCCCCCCCCCCCCCC));
                    Marshal.WriteInt16((nint)hookAddrFpsGet, 0x25FF);
                    Marshal.WriteInt64((nint)(hookAddrFpsGet + 6), (long)addrTarFpsGetTarFun);
                    Marshal.WriteInt16((nint)hookAddrFpsSet, 0x25FF);
                    Marshal.WriteInt64((nint)(hookAddrFpsSet + 6), (long)addrTarFpsSetTarFun);

                    if (!Kernel32.WriteProcessMemory(tarHandle, new IntPtr((long)hookAddrTarFpsGet), new IntPtr((long)hookAddrFpsGet), 0x10, out _))
                    {
                        int errCode = Marshal.GetLastWin32Error();
                        Debug.WriteLine($"Hook get_fps Fail! ( 0x{errCode:X} )");
                    }

                    if (!Kernel32.WriteProcessMemory(tarHandle, new IntPtr((long)hookAddrTarFpsSet), new IntPtr((long)hookAddrFpsSet), 0x10, out _))
                    {
                        int errCode = Marshal.GetLastWin32Error();
                        Debug.WriteLine($"Hook get_fps Fail! ( 0x{errCode:X} )");
                    }

                    using Kernel32.SafeHTHREAD temp = Interop.CreateRemoteThread(tarHandle, new SECURITY_ATTRIBUTES(), 0, (tarProcBuffer + 0x30), IntPtr.Zero, 0, out _);
                    return tarProcBuffer + 0xD1;
                }
                else
                {
                    int errCode = Marshal.GetLastWin32Error();
                    Debug.WriteLine($"Write Patch Fail! ( 0x{errCode:X} )");
                }
            }
            else
            {
                int errCode = Marshal.GetLastWin32Error();
                Debug.WriteLine($"Virtual Alloc Fail! ( 0x{errCode:X} )");
                return 0;
            }
        }

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
        //nint patchPtr = 0;
        {
            //patchPtr = InjectPatch(up, hUnityPlayer.modBaseSize, hUnityPlayer.modBaseAddr, pfps, hProcess.DangerousGetHandle(), targetFps);
            //patchPtr = Interop.InjectPatch(up, hUnityPlayer.modBaseAddr, pfps, hProcess.DangerousGetHandle(), targetFps);
            //if (patchPtr == IntPtr.Zero)
            //{
            //    Debug.WriteLine("[Unlocker] Inject Patch Fail!");
            //}
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
