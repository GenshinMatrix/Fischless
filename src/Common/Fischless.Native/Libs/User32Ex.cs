using System.Runtime.InteropServices;

namespace Fischless.Native;

public static class User32Ex
{
    [DllImport(Lib.User32, SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern nint MB_GetString(uint wBtn);
}
