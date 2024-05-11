using System.Runtime.InteropServices;

namespace Fischless.GamepadCapture.RawInput;

/// <summary>
/// Value type for raw input devices.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct RAWINPUTDEVICE
{
    /// <summary>
    /// Top level collection Usage page for the raw input device.
    /// </summary>
    public HIDUsagePage UsagePage;

    /// <summary>
    /// Top level collection Usage for the raw input device.
    /// </summary>
    public HIDUsage Usage;

    /// <summary>
    /// Mode flag that specifies how to interpret the information provided by UsagePage and Usage.
    /// </summary>
    public RawInputDeviceFlags Flags;

    /// <summary>
    /// Handle to the target device. If NULL, it follows the keyboard focus.
    /// </summary>
    public nint WindowHandle;
}
