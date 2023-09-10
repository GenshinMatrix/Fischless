using Vanara.PInvoke;
using static Vanara.PInvoke.Kernel32;

namespace Fischless.Fetch.Unlocker;

internal readonly unsafe struct VirtualMemory : IDisposable
{
    public readonly void* Pointer;
    public readonly uint Length;

    public unsafe VirtualMemory(uint dwSize)
    {
        Length = dwSize;
        MEM_ALLOCATION_TYPE commitAndReserve = MEM_ALLOCATION_TYPE.MEM_COMMIT | MEM_ALLOCATION_TYPE.MEM_RESERVE;
        Pointer = Kernel32.VirtualAlloc(default, dwSize, commitAndReserve, MEM_PROTECTION.PAGE_READWRITE).ToPointer();
    }

    public static unsafe implicit operator Span<byte>(VirtualMemory memory)
    {
        return memory.GetBuffer();
    }

    public unsafe Span<byte> GetBuffer()
    {
        return new Span<byte>(Pointer, (int)Length);
    }

    public void Dispose()
    {
        VirtualFree((nint)Pointer, 0, MEM_ALLOCATION_TYPE.MEM_RELEASE);
    }
}
