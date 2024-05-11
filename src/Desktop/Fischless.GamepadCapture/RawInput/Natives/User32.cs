using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

internal static class User32
{
    public const int WM_INPUT = 0x00FF;

    public const uint MAPVK_VSC_TO_VK_EX = 3;

    public const ushort KEYBOARD_OVERRUN_MAKE_CODE = 0xFF;

    /// <summary>
    /// Function to retrieve raw input data.
    /// </summary>
    /// <param name="hRawInput">Handle to the raw input.</param>
    /// <param name="uiCommand">Command to issue when retrieving data.</param>
    /// <param name="pData">Raw input data.</param>
    /// <param name="pcbSize">Number of bytes in the array.</param>
    /// <param name="cbSizeHeader">Size of the header.</param>
    /// <returns>0 if successful if pData is null, otherwise number of bytes if pData is not null.</returns>
    [DllImport(Libs.User32)]
    public static extern bool GetRawInputData(nint hRawInput, RawInputCommand uiCommand, out RawKeyboard pData, out int pcbSize, int cbSizeHeader);

    [DllImport(Libs.User32)]
    public static extern bool GetRawInputData(nint hRawInput, RawInputCommand uiCommand, out RawHID pData, out int pcbSize, int cbSizeHeader);

    [DllImport(Libs.User32)]
    public static extern bool GetRawInputData(nint hRawInput, RawInputCommand uiCommand, out RawMouse pData, out int pcbSize, int cbSizeHeader);

    public static bool GetRawInputData(nint hRawInput, out RawKeyboard pData)
        => GetRawInputData(hRawInput, RawInputCommand.Input, out pData, out _, Marshal.SizeOf(typeof(RawInputHeader)))
            && GetRawInputData(hRawInput, RawInputCommand.Input, out pData, out _, Marshal.SizeOf(typeof(RawInputHeader)));

    public static bool GetRawInputData(nint hRawInput, out RawHID pData)
        => GetRawInputData(hRawInput, RawInputCommand.Input, out pData, out _, Marshal.SizeOf(typeof(RawInputHeader)))
            && GetRawInputData(hRawInput, RawInputCommand.Input, out pData, out _, Marshal.SizeOf(typeof(RawInputHeader)));

    public static bool GetRawInputData(nint hRawInput, out RawMouse pData)
        => GetRawInputData(hRawInput, RawInputCommand.Input, out pData, out _, Marshal.SizeOf(typeof(RawInputHeader)))
            && GetRawInputData(hRawInput, RawInputCommand.Input, out pData, out _, Marshal.SizeOf(typeof(RawInputHeader)));

    /// <summary>Function to register a raw input device.</summary>
    /// <param name="pRawInputDevices">Array of raw input devices.</param>
    /// <param name="uiNumDevices">Number of devices.</param>
    /// <param name="cbSize">Size of the RAWINPUTDEVICE structure.</param>
    /// <returns>TRUE if successful, FALSE if not.</returns>
    [DllImport(Libs.User32)]
    public static extern bool RegisterRawInputDevices([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] RAWINPUTDEVICE[] pRawInputDevices, int uiNumDevices, int cbSize);

    [DllImport(Libs.User32)]
    static extern int GetRawInputDeviceInfo(nint hDevice, RawInputCommand uiCommand, byte[] pData, out int pcbSize);

    public static bool GetRawInputDeviceInfo(nint hDevice, RawInputCommand uiCommand, out byte[] pData)
    {
        pData = null!;
        if (GetRawInputDeviceInfo(hDevice, uiCommand, null!, out int size) != 0) return false;

        pData = new byte[size];
        return GetRawInputDeviceInfo(hDevice, uiCommand, pData, out size) > 0;
    }

    [DllImport(Libs.User32)]
    public static extern uint MapVirtualKey(uint uCode, uint uMapType);
}
