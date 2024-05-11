using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

public static partial class Listener
{
    private static readonly ulong[] hat_bm = [0b0001, 0b0011, 0b0010, 0b0110, 0b0100, 0b1100, 0b1000, 0b1001];

    private static ulong last;
    private static double precise;
    private static nint windowHandle;
    public static bool captureKeyboard = true;
    public static bool captureGamepad = true;
    public static bool captureMouse = true;

    static void HandleKeyboard(RawKeyboard input)
    {
        // https://stackoverflow.com/a/71885051
        if (input.MakeCode == User32.KEYBOARD_OVERRUN_MAKE_CODE) return;
        if (input.VKey >= 0xFF) return;

        ushort scanCode = input.MakeCode;
        Keys vkCode = (Keys)input.VKey;

        if (input.Flags.HasFlag(RawKeyboardFlags.RI_KEY_E0))
            scanCode |= 0xE000;

        if (input.Flags.HasFlag(RawKeyboardFlags.RI_KEY_E1))
            scanCode |= 0xE100;

        if (vkCode == Keys.ShiftKey || vkCode == Keys.ControlKey || vkCode == Keys.Menu)
            vkCode = (Keys)(ushort)User32.MapVirtualKey(scanCode, User32.MAPVK_VSC_TO_VK_EX);

        Recorder.RecordInput(precise, !input.Flags.HasFlag(RawKeyboardFlags.RI_KEY_BREAK), new Input(vkCode.ToString(), input.Header.Device));
    }

    static void HandleMouse(RawMouse input)
    {
        if (input.ButtonFlags == 0) return;

        for (int i = 0; i < 10; i++)
        {
            if (((input.ButtonFlags >> i) & 1) == 1)
            {
                Recorder.RecordInput(precise, (i & 1) == 0, new Input(((MouseKeys)(i >> 1)).ToString(), input.Header.Device));
            }
        }
    }

    static void HandleHID(RawHID input)
    {
        if (!User32.GetRawInputDeviceInfo(input.Header.Device, RawInputCommand.PreparsedData, out byte[] ppd)) return;

        try
        {
            if (Hid.HidP_GetCaps(ppd, out HIDP_CAPS caps) != NTSTATUS.HIDP_STATUS_SUCCESS) return;

            var buttoncaps = new HIDP_BUTTON_CAPS[caps.NumberInputButtonCaps];
            if (Hid.HidP_GetButtonCaps(HIDP_REPORT_TYPE.HidP_Input, buttoncaps, ref caps.NumberInputButtonCaps, ppd) != NTSTATUS.HIDP_STATUS_SUCCESS) return;

            var valuecaps = new HIDP_VALUE_CAPS[caps.NumberInputValueCaps];
            if (Hid.HidP_GetValueCaps(HIDP_REPORT_TYPE.HidP_Input, valuecaps, ref caps.NumberInputValueCaps, ppd) != NTSTATUS.HIDP_STATUS_SUCCESS) return;

            ulong inputs = 0;

            if (buttoncaps[0].UsagePage == HIDUsagePage.Button)
            {
                uint buttons = (uint)(buttoncaps[0].UsageMax - buttoncaps[0].UsageMin + 1);
                ushort[] list = new ushort[buttons];

                if (Hid.HidP_GetUsages(HIDP_REPORT_TYPE.HidP_Input, buttoncaps[0].UsagePage, 0, list, ref buttons, ppd, input.bRawData, input.dwSizeHid) != NTSTATUS.HIDP_STATUS_SUCCESS) return;

                for (int i = 0; i < buttons; i++)
                    inputs |= 1UL << (list[i] - buttoncaps[0].UsageMin);
            }

            foreach (var i in valuecaps)
            {
                if (i.UsagePage == HIDUsagePage.Generic && i.UsageMin == HIDUsage.HatSwitch)
                {
                    if (Hid.HidP_GetUsageValue(HIDP_REPORT_TYPE.HidP_Input, i.UsagePage, 0, i.UsageMin, out int hat, ppd, input.bRawData, input.dwSizeHid) != NTSTATUS.HIDP_STATUS_SUCCESS) return;

                    if (hat.InRangeII(i.LogicalMin, i.LogicalMax))
                    {
                        int size = i.LogicalMax - i.LogicalMin + 1;
                        hat -= i.LogicalMin;

                        if (size == 4)
                        {
                            // 4-way hat
                            inputs |= 1UL << (hat + 32);
                        }
                        else if (size == 8)
                        {
                            // 8-way hat
                            inputs |= hat_bm[hat] << 32;
                        }
                    }
                    break;
                }
            }

            if (last != inputs)
            {
                for (int n = 0; n < 36; n++)
                {
                    ulong i = (inputs >> n) & 1;
                    if (i != ((last >> n) & 1))
                    {
                        Recorder.RecordInput(precise, i == 1, new Input(((GamepadKeys)n).ToString(), input.Header.Device));
                    }
                }

                last = inputs;
            }
        }
        finally
        {
            Hid.HidD_FreePreparsedData(ppd);
        }
    }

    public static void Process(nint lParam)
    {
        if (!User32.GetRawInputData(lParam, out RawHID raw)) return;

        if (raw.Header.Type == RawInputType.RIM_TYPE_KEYBOARD)
        {
            if (!User32.GetRawInputData(lParam, out RawKeyboard rkb)) return;
            HandleKeyboard(rkb);
        }
        else if (raw.Header.Type == RawInputType.RIM_TYPE_HID)
        {
            HandleHID(raw);
        }
        else if (raw.Header.Type == RawInputType.RIM_TYPE_MOUSE)
        {
            if (!User32.GetRawInputData(lParam, out RawMouse rm)) return;
            HandleMouse(rm);
        }
    }

    private static RAWINPUTDEVICE[] deviceList;

    static void Register(bool enable)
    {
        int n = 0;

        if (enable)
        {
            if (captureKeyboard)
            {
                deviceList[n++].Usage = HIDUsage.Keyboard;
            }

            if (captureGamepad)
            {
                deviceList[n++].Usage = HIDUsage.Gamepad;
                deviceList[n++].Usage = HIDUsage.Joystick;
            }

            if (captureMouse)
            {
                deviceList[n++].Usage = HIDUsage.Mouse;
            }
        }
        else n = deviceList.Length;

        for (int i = 0; i < n; i++)
        {
            deviceList[i].Flags = enable ? RawInputDeviceFlags.InputSink : RawInputDeviceFlags.None;
            deviceList[i].WindowHandle = enable ? windowHandle : IntPtr.Zero;
        }

        User32.RegisterRawInputDevices(deviceList, n, Marshal.SizeOf(typeof(RAWINPUTDEVICE)));
    }

    public static void Start(nint hWnd)
    {
        if (hWnd == IntPtr.Zero)
        {
            throw new ArgumentNullException(nameof(hWnd));
        }

        windowHandle = hWnd;
        last = 0;
        Register(true);
    }

    public static void Stop() => Register(false);

    static Listener()
    {
        deviceList = new RAWINPUTDEVICE[4];

        for (int i = 0; i < deviceList.Length; i++)
        {
            deviceList[i].UsagePage = HIDUsagePage.Generic;
        }
    }
}

file static class Utility
{
    public static bool InRangeII<T>(this T val, T min, T max) where T : IComparable<T>
        => min.CompareTo(val) <= 0 && val.CompareTo(max) <= 0;
}
