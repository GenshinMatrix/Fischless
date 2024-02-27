using System.Runtime.InteropServices;

namespace Fischless.Fetch.Unlocker;

internal readonly unsafe struct VirtualMemory(uint dwSize) : IDisposable
{
    private readonly uint size = dwSize;
    private readonly void* pointer = NativeMemory.Alloc(dwSize);

    public uint Size { get => size; }
    public void* Pointer { get => pointer; }

    public void Dispose()
    {
        NativeMemory.Free(pointer);
    }
}
