// pch.cpp: source file corresponding to the pre-compiled header

#include "pch.h"
#include <Windows.h>
#include <TlHelp32.h>
#include <vector>
#include <string>
#include <thread>
#include <Psapi.h>

BYTE _shellcode_genshin[] =
{
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
};

static uintptr_t PatternScan_Region(uintptr_t startAddress, size_t regionSize, const char* signature)
{
    auto pattern_to_byte = [](const char* pattern)
        {
            std::vector<int> bytes;
            const char* start = pattern;
            const char* end = pattern + strlen(pattern);

            for (const char* current = start; current < end; ++current) {
                if (*current == '?') {
                    ++current;
                    if (*current == '?')
                        ++current;
                    bytes.push_back(-1);
                }
                else {
                    bytes.push_back(strtoul(current, const_cast<char**>(&current), 16));
                }
            }
            return bytes;
        };

    std::vector<int> patternBytes = pattern_to_byte(signature);
    auto scanBytes = reinterpret_cast<std::uint8_t*>(startAddress);

    for (size_t i = 0; i < regionSize - patternBytes.size(); ++i)
    {
        bool found = true;
        for (size_t j = 0; j < patternBytes.size(); ++j) {
            if (scanBytes[i + j] != patternBytes[j] && patternBytes[j] != -1) {
                found = false;
                break;
            }
        }
        if (found) {
            return (uintptr_t)&scanBytes[i];
        }
    }
    return 0;
}

std::string GetLastErrorAsString(DWORD code)
{
    LPSTR buf = nullptr;
    FormatMessageA(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL, code, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPSTR)&buf, 0, NULL);
    std::string ret = buf;
    LocalFree(buf);
    return ret;
}

DWORD64 inject_patch(LPVOID unity_module, DWORD64 unity_baseaddr, DWORD64 _ptr_fps, HANDLE Tar_handle, int FpsValue)
{
    BYTE search_sec[] = ".text";//max 8 byte
    uintptr_t WinPEfileVA = *(uintptr_t*)(&unity_module) + 0x3c; //dos_header
    uintptr_t PEfptr = *(uintptr_t*)(&unity_module) + *(uint32_t*)WinPEfileVA; //get_winPE_VA
    _IMAGE_NT_HEADERS64 _FilePE_Nt_header = *(_IMAGE_NT_HEADERS64*)PEfptr;
    _IMAGE_SECTION_HEADER _sec_temp{};
    DWORD64 Module_TarSec_RVA;
    DWORD64 Module_TarSecEnd_RVA;
    DWORD Module_TarSec_Size;
    if (_FilePE_Nt_header.Signature == 0x00004550)
    {
        DWORD sec_num = _FilePE_Nt_header.FileHeader.NumberOfSections;//获得指定节段参数
        DWORD num = sec_num;
        while (num)
        {
            _sec_temp = *(_IMAGE_SECTION_HEADER*)(PEfptr + 264 + (40 * (static_cast<unsigned long long>(sec_num) - num)));

            //printf_s("sec_%d_is:  %s\n", sec_num - num, _sec_temp.Name);
            int i = 8;
            int len = sizeof(search_sec) - 1;
            int cmp = 0;
            while ((i != 0) && _sec_temp.Name[8 - i] && search_sec[8 - i])
            {
                if (_sec_temp.Name[8 - i] == search_sec[8 - i])
                {
                    cmp++;
                }
                i--;
            }
            if (cmp == len)
            {
                Module_TarSec_RVA = _sec_temp.VirtualAddress + (DWORD64)unity_module;
                Module_TarSec_Size = _sec_temp.Misc.VirtualSize;
                Module_TarSecEnd_RVA = Module_TarSec_RVA + Module_TarSec_Size;
                goto __Get_target_sec;
            }
            num--;
        }
        printf_s("Get Target Section Fail !\n");
        return 0;
    }
    return 0;

__Get_target_sec:
    DWORD64 address = 0;
    {
        DWORD64 Hook_addr_fpsget = 0;   //in buffer
        DWORD64 Hook_addr_tar_fpsget = 0;
        DWORD64 Hook_addr_fpsSet = 0;   //in buffer
        DWORD64 Hook_addr_tar_fpsSet = 0;
        DWORD64 _addr_tar_fpsget_TarFun = 0;
        DWORD64 _addr_tar_fpsSet_TarFun = 0;
        while (address = PatternScan_Region(Module_TarSec_RVA, Module_TarSec_Size, "CC 8B 05 ?? ?? ?? ?? C3 CC"))//搜索正确patch点位//get_fps
        {
            uintptr_t rip = address;
            rip += 3;
            rip += *(int32_t*)(rip)+4;
            if ((rip - (uintptr_t)unity_module + (uintptr_t)unity_baseaddr) == _ptr_fps)
            {
                Hook_addr_fpsget = address + 1;
                Hook_addr_tar_fpsget = Hook_addr_fpsget - (uintptr_t)unity_module + (uintptr_t)unity_baseaddr;
                goto __Get_fpsGet_addr;
            }
            else
            {
                *(uint64_t*)(address + 1) = 0xCCCCCCCCCCCCCCCC;
            }
        }
        printf_s("\nPatch pattern1 outdate...\n");
        return 0;

    __Get_fpsGet_addr:
        while (address = PatternScan_Region(Module_TarSec_RVA, Module_TarSec_Size, "CC 89 0D ?? ?? ?? ?? C3 CC"))//搜索正确patch点位//set_fps
        {
            uintptr_t rip = address;
            rip += 3;
            rip += *(int32_t*)(rip)+4;
            if ((rip - (uintptr_t)unity_module + (uintptr_t)unity_baseaddr) == _ptr_fps)
            {
                Hook_addr_fpsSet = address + 1;
                Hook_addr_tar_fpsSet = Hook_addr_fpsSet - (uintptr_t)unity_module + (uintptr_t)unity_baseaddr;
                goto __Get_fpsSet_addr;
            }
            else
            {
                *(uint64_t*)(address + 1) = 0xCCCCCCCCCCCCCCCC;
            }
        }
        printf_s("\nPatch pattern2 outdate...\n");
        return 0;

    __Get_fpsSet_addr:
        uint64_t _Addr_OpenProcess = 0;
        uint64_t _Addr_ReadProcessmem = 0;
        uint64_t _Addr_Sleep = 0;
        if (address = PatternScan_Region(Module_TarSec_RVA, Module_TarSec_Size, "33 D2 B9 00 04 00 00 FF 15 ?? ?? ?? ??"))//get API OpenProcess
        {
            uintptr_t rip = address;
            rip += 9;
            rip += *(int32_t*)(rip)+4;
            if (*(uint64_t*)(rip) == 0)
            {
                rip = rip - (uintptr_t)unity_module + (uintptr_t)unity_baseaddr;
                while (_Addr_OpenProcess == 0)
                {
                    if (ReadProcessMemory(Tar_handle, (LPCVOID)rip, &_Addr_OpenProcess, 8, 0) == 0)
                    {
                        DWORD ERR_code = GetLastError();
                        printf_s("\nGet Target Openprocess API Fail! ( 0x%X ) - %s\n", ERR_code, GetLastErrorAsString(ERR_code).c_str());
                        return 0;
                    }
                }
            }
            else { _Addr_OpenProcess = *(uint64_t*)rip; }
        }
        if (address = PatternScan_Region(Module_TarSec_RVA, Module_TarSec_Size, "48 89 44 24 20 FF 15 ?? ?? ?? ?? 48 8B 54 24 70"))//get API ReadProcmem
        {
            uintptr_t rip = address;
            rip += 7;
            rip += *(int32_t*)(rip)+4;
            if (*(uint64_t*)(rip) == 0)
            {
                rip = rip - (uintptr_t)unity_module + (uintptr_t)unity_baseaddr;
                while (_Addr_ReadProcessmem == 0)
                {
                    if (ReadProcessMemory(Tar_handle, (LPCVOID)rip, &_Addr_ReadProcessmem, 8, 0) == 0)
                    {
                        DWORD ERR_code = GetLastError();
                        printf_s("\nGet Target Readprocmem API Fail! ( 0x%X ) - %s\n", ERR_code, GetLastErrorAsString(ERR_code).c_str());
                        return 0;
                    }
                }
            }
            else { _Addr_ReadProcessmem = *(uint64_t*)rip; }
        }
        if (address = PatternScan_Region(Module_TarSec_RVA, Module_TarSec_Size, "41 8B C8 FF 15 ?? ?? ?? ?? 8B C7"))//get API Sleep
        {
            uintptr_t rip = address;
            rip += 5;
            rip += *(int32_t*)(rip)+4;
            if (*(uint64_t*)(rip) == 0)
            {
                rip = rip - (uintptr_t)unity_module + (uintptr_t)unity_baseaddr;
                while (_Addr_Sleep == 0)
                {
                    if (ReadProcessMemory(Tar_handle, (LPCVOID)rip, &_Addr_Sleep, 8, 0) == 0)
                    {
                        DWORD ERR_code = GetLastError();
                        printf_s("\nGet Target Sleep API Fail! ( 0x%X ) - %s\n", ERR_code, GetLastErrorAsString(ERR_code).c_str());
                        return 0;
                    }
                }
            }
            else { _Addr_Sleep = *(uint64_t*)rip; }
        }
        *(uint32_t*)(&_shellcode_genshin) = GetCurrentProcessId();    //unlocker PID
        *(uint64_t*)(&_shellcode_genshin[8]) = (uint64_t)(&FpsValue); //unlocker fps set
        *(uint64_t*)(&_shellcode_genshin[16]) = _Addr_OpenProcess;
        *(uint64_t*)(&_shellcode_genshin[24]) = _Addr_ReadProcessmem;
        *(uint64_t*)(&_shellcode_genshin[32]) = _Addr_Sleep;
        LPVOID __Tar_proc_buffer = VirtualAllocEx(Tar_handle, 0, 0x1000, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
        if (__Tar_proc_buffer)
        {
            if (WriteProcessMemory(Tar_handle, __Tar_proc_buffer, &_shellcode_genshin, sizeof(_shellcode_genshin), 0));
            {
                _addr_tar_fpsSet_TarFun = (uint64_t)__Tar_proc_buffer + 160;
                _addr_tar_fpsget_TarFun = (uint64_t)__Tar_proc_buffer + 208;
                *(uint64_t*)Hook_addr_fpsget = 0xCCCCCCCCCCCCCCCC;
                *(uint64_t*)Hook_addr_fpsSet = 0xCCCCCCCCCCCCCCCC;
                *(uint64_t*)Hook_addr_fpsget = 0x25FF;
                *(uint64_t*)(Hook_addr_fpsget + 6) = _addr_tar_fpsget_TarFun;
                *(uint64_t*)Hook_addr_fpsSet = 0x25FF;
                *(uint64_t*)(Hook_addr_fpsSet + 6) = _addr_tar_fpsSet_TarFun;
                if (WriteProcessMemory(Tar_handle, (LPVOID)Hook_addr_tar_fpsget, (LPVOID)Hook_addr_fpsget, 0x10, 0) == 0)
                {
                    DWORD ERR_code = GetLastError();
                    printf_s("\nHook get_fps Fail! ( 0x%X ) - %s\n", ERR_code, GetLastErrorAsString(ERR_code).c_str());
                }
                if (WriteProcessMemory(Tar_handle, (LPVOID)Hook_addr_tar_fpsSet, (LPVOID)Hook_addr_fpsSet, 0x10, 0) == 0)
                {
                    DWORD ERR_code = GetLastError();
                    printf_s("\nHook get_fps Fail! ( 0x%X ) - %s\n", ERR_code, GetLastErrorAsString(ERR_code).c_str());
                }
                HANDLE temp = CreateRemoteThread(Tar_handle, 0, 0, (LPTHREAD_START_ROUTINE)((uint64_t)__Tar_proc_buffer + 0x30), 0, 0, 0);
                if (temp)
                {
                    CloseHandle(temp);
                }
                return ((uint64_t)__Tar_proc_buffer + 0xD1);
            }
            DWORD ERR_code = GetLastError();
            printf_s("\nWrite Patch Fail! ( 0x%X ) - %s\n", ERR_code, GetLastErrorAsString(ERR_code).c_str());
        }
        else
        {
            DWORD ERR_code = GetLastError();
            printf_s("\nVirtual Alloc Fail! ( 0x%X ) - %s\n", ERR_code, GetLastErrorAsString(ERR_code).c_str());
            return 0;
        }
    }
}
