using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

[StructLayout(LayoutKind.Sequential)]
public struct HIDP_VALUE_CAPS
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

    [MarshalAs(UnmanagedType.U1)]
    public bool HasNull;

    [MarshalAs(UnmanagedType.U1)]
    private byte Reserved;

    [MarshalAs(UnmanagedType.U2)]
    public ushort BitSize;

    [MarshalAs(UnmanagedType.U2)]
    public ushort ReportCount;

    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 5)]
    private ushort[] Reserved2;

    [MarshalAs(UnmanagedType.U4)]
    public uint UnitsExp;

    [MarshalAs(UnmanagedType.U4)]
    public uint Units;

    [MarshalAs(UnmanagedType.U4)]
    public int LogicalMin;

    [MarshalAs(UnmanagedType.U4)]
    public int LogicalMax;

    [MarshalAs(UnmanagedType.U4)]
    public int PhysicalMin;

    [MarshalAs(UnmanagedType.U4)]
    public int PhysicalMax;

    [MarshalAs(UnmanagedType.U2)]
    public HIDUsage UsageMin;

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
