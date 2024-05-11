using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

[StructLayout(LayoutKind.Sequential)]
public struct HIDP_BUTTON_CAPS
{
    [MarshalAs(UnmanagedType.U2)]
    public HIDUsagePage UsagePage;

    [MarshalAs(UnmanagedType.U1)]
    public byte ReportID;

    [MarshalAs(UnmanagedType.U1)]
    public bool IsAlias;

    [MarshalAs(UnmanagedType.U2)]
    public ushort BitField;

    [MarshalAs(UnmanagedType.U2)]
    public ushort LinkCollection;

    [MarshalAs(UnmanagedType.U2)]
    public ushort LinkUsage;

    [MarshalAs(UnmanagedType.U2)]
    public ushort LinkUsagePage;

    [MarshalAs(UnmanagedType.U1)]
    public bool IsRange;

    [MarshalAs(UnmanagedType.U1)]
    public bool IsStringRange;

    [MarshalAs(UnmanagedType.U1)]
    public bool IsDesignatorRange;

    [MarshalAs(UnmanagedType.U1)]
    public bool IsAbsolute;

    [MarshalAs(UnmanagedType.U2)]
    public ushort ReportCount;

    [MarshalAs(UnmanagedType.U2)]
    private ushort Reserved2;

    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 9)]
    private uint[] Reserved;

    [MarshalAs(UnmanagedType.U2)]
    public ushort UsageMin;

    [MarshalAs(UnmanagedType.U2)]
    public ushort UsageMax;

    [MarshalAs(UnmanagedType.U2)]
    public ushort StringMin;

    [MarshalAs(UnmanagedType.U2)]
    public ushort StringMax;

    [MarshalAs(UnmanagedType.U2)]
    public ushort DesignatorMin;

    [MarshalAs(UnmanagedType.U2)]
    public ushort DesignatorMax;

    [MarshalAs(UnmanagedType.U2)]
    public ushort DataIndexMin;

    [MarshalAs(UnmanagedType.U2)]
    public ushort DataIndexMax;
}
