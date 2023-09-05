using System.Runtime.InteropServices;

namespace Fischless.Native;

[StructLayout(LayoutKind.Sequential)]
public struct OSVERSIONINFOEX
{
    public int OSVersionInfoSize;
    public int MajorVersion;
    public int MinorVersion;
    public int BuildNumber;
    public int Revision;
    public int PlatformId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string CSDVersion;
    public ushort ServicePackMajor;
    public ushort ServicePackMinor;
    public short SuiteMask;
    public byte ProductType;
    public byte Reserved;
}
