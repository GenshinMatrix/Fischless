using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

[StructLayout(LayoutKind.Sequential)]
public struct DEVPROPKEY
{
    public Guid fmtid;
    public uint pid;
}
