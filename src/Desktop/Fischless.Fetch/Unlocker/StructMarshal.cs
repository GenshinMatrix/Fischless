using System.Runtime.CompilerServices;
using static Vanara.PInvoke.Kernel32;

namespace Fischless.Fetch.Unlocker;

#pragma warning disable CS8500

internal static class StructMarshal
{
    public static unsafe MODULEENTRY32 MODULEENTRY32()
    {
        return new() { dwSize = (uint)sizeof(MODULEENTRY32) };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<MODULEENTRY32> EnumerateModuleEntry32(HSNAPSHOT snapshot)
    {
        MODULEENTRY32 entry = MODULEENTRY32();

        if (Module32First(snapshot, ref entry))
        {
            do
            {
                yield return entry;
            }
            while (Module32Next(snapshot, ref entry));
        }
    }
}
