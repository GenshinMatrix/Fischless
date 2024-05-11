namespace Fischless.GamepadCapture.RawInput;

/// <summary>
/// Enumeration contanining the command types to issue.
/// </summary>
public enum RawInputCommand
{
    /// <summary>
    /// Get input data.
    /// </summary>
    Input = 0x10000003,

    /// <summary>
    /// Get header data.
    /// </summary>
    Header = 0x10000005,

    PreparsedData = 0x20000005,
    DeviceName = 0x20000007,
    DeviceInfo = 0x2000000b
}
