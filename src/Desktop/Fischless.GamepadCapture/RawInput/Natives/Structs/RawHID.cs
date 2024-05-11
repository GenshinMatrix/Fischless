using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

[StructLayout(LayoutKind.Sequential)]
public struct RawHID
{
    public RawInputHeader Header;
    public uint dwSizeHid;
    public uint dwCount;

    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 256)]
    public byte[] bRawData;
}
