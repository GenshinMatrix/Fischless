using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

[StructLayout(LayoutKind.Sequential)]
public struct RawKeyboard
{
    public RawInputHeader Header;
    public ushort MakeCode;
    public RawKeyboardFlags Flags;
    private ushort Reserved;
    public ushort VKey;
    public uint Message;
    public uint ExtraInformation;
}
