using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

internal static class Kernel32
{
    [DllImport(Libs.Kernel32, SetLastError = true)]
    public static extern nint CreateFile(
        string lpFileName,
        uint dwDesiredAccess,
        uint dwShareMode,
        nint lpSecurityAttributes,
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes,
        nint hTemplateFile
    );

    [DllImport(Libs.Kernel32, SetLastError = true)]
    public static extern bool CloseHandle(nint hObject);
}
