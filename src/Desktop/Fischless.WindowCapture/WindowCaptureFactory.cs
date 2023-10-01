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
            CaptureMode.BitBlt => new BitBlt.BitBltCapture(),
            CaptureMode.WindowsGraphicsCapture => new Graphics.GraphicsCapture(),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null),
        };
    }
}
