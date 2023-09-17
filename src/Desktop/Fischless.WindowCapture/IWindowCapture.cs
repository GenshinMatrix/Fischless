using System.Drawing;

namespace Fischless.WindowCapture;

public interface IWindowCapture
{
    public bool IsCapturing { get; }

    public void Start(nint hWnd);

    public Bitmap? Capture();

    public void Stop();
}
