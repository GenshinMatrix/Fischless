using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

[StructLayout(LayoutKind.Sequential)]
public struct HIDP_CAPS
{
    [MarshalAs(UnmanagedType.U2)]
    public HIDUsage Usage;

    [MarshalAs(UnmanagedType.U2)]
    public HIDUsagePage UsagePage;

    [MarshalAs(UnmanagedType.U2)]
    public ushort InputReportByteLength;

    [MarshalAs(UnmanagedType.U2)]
    public ushort OutputReportByteLength;

    [MarshalAs(UnmanagedType.U2)]
    public ushort FeatureReportByteLength;

    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 17)]
    private ushort[] Reserved;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberLinkCollectionNodes;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberInputButtonCaps;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberInputValueCaps;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberInputDataIndices;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberOutputButtonCaps;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberOutputValueCaps;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberOutputDataIndices;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberFeatureButtonCaps;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberFeatureValueCaps;

    [MarshalAs(UnmanagedType.U2)]
    public ushort NumberFeatureDataIndices;
};
