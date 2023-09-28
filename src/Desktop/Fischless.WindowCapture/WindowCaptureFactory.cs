namespace Fischless.WindowCapture;

public enum CaptureMode
{
    BitBlt,
    WindowsGraphicsCapture,
}

public static class CaptureModeExtensions
{
    public static CaptureMode ToCaptureMode(this string modeName)
    {
        return (CaptureMode) Enum.Parse(typeof(CaptureMode), modeName);
    }
}

public class WindowCaptureFactory
{
    public static string[] ModeNames()
    {
        return Enum.GetNames(typeof(CaptureMode));
    }

    public static IWindowCapture Create(CaptureMode mode)
    {
        return mode switch
        {
            CaptureMode.BitBlt => new BitBltCapture(),
            CaptureMode.WindowsGraphicsCapture => new GraphicsCapture(),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null),
        };
    }
}
