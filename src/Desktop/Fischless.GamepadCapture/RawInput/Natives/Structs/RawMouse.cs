using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

[StructLayout(LayoutKind.Sequential)]
public struct RawMouse
{
    public RawInputHeader Header;
    public ushort Flags;
    public ushort ButtonData;
    public ushort ButtonFlags;
    public uint RawButtons;
    public int LastX;
    public int LastY;
    public uint ExtraInformation;
}
