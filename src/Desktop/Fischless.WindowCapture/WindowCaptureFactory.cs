namespace Fischless.WindowCapture;

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
