using System.Runtime.InteropServices;
using System.Text;

namespace Fischless.GamepadCapture.RawInput;

internal static class Hid
{
    [DllImport(Libs.Hid, SetLastError = true)]
    public static extern NTSTATUS HidP_GetCaps(byte[] PreparsedData, out HIDP_CAPS Capabilities);

    [DllImport(Libs.Hid, SetLastError = true)]
    public static extern NTSTATUS HidP_GetButtonCaps(
        HIDP_REPORT_TYPE ReportType,
        [In, Out] HIDP_BUTTON_CAPS[] ButtonCaps,
        ref ushort ButtonCapsLength,
        byte[] PreparsedData
    );

    [DllImport(Libs.Hid, SetLastError = true)]
    public static extern NTSTATUS HidP_GetValueCaps(
        HIDP_REPORT_TYPE ReportType,
        [In, Out] HIDP_VALUE_CAPS[] ValueCaps,
        ref ushort ValueCapsLength,
        byte[] PreparsedData
    );

    [DllImport(Libs.Hid, SetLastError = true)]
    public static extern NTSTATUS HidP_GetUsages(
        HIDP_REPORT_TYPE ReportType,
        HIDUsagePage UsagePage,
        ushort LinkCollection,
        [In, Out] ushort[] UsageList,
        ref uint UsageLength,
        byte[] PreparsedData,
        byte[] Report,
        uint ReportLength
    );

    [DllImport(Libs.Hid, SetLastError = true)]
    public static extern NTSTATUS HidP_GetUsageValue(
        HIDP_REPORT_TYPE ReportType,
        HIDUsagePage UsagePage,
        ushort LinkCollection,
        HIDUsage Usage,
        out int UsageValue,
        byte[] PreparsedData,
        byte[] Report,
        uint ReportLength
    );

    [DllImport(Libs.Hid, SetLastError = true)]
    public static extern bool HidD_FreePreparsedData(byte[] PreparsedData);

    [DllImport(Libs.Hid, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool HidD_GetProductString(nint hFile, StringBuilder buffer, int bufferLength);
}
