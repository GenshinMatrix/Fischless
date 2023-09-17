using System.Drawing;
using Vanara.PInvoke;

namespace Fischless.WindowCapture.StretchBlt;

public class StretchBltCapture : IWindowCapture
{
    private nint _hWnd;
    public bool IsCapturing { get; private set; }

    public void Start(nint hWnd)
    {
        _hWnd = hWnd;
        IsCapturing = true;
    }

    public Bitmap? Capture()
    {
        try
        {
            User32.GetWindowRect(_hWnd, out var windowRect);
            var x = 0;
            var y = 0;
            var width = windowRect.right - windowRect.left;
            var height = windowRect.bottom - windowRect.top;

            Bitmap copied = new(width, height);
            using Graphics g = Graphics.FromImage(copied);
            nint hdcDest = g.GetHdc();
            Gdi32.SafeHDC hdcSrc = User32.GetDC(_hWnd == IntPtr.Zero ? User32.GetDesktopWindow() : _hWnd);
            _ = Gdi32.StretchBlt(hdcDest, 0, 0, width, height, hdcSrc, x, y, width, height, Gdi32.RasterOperationMode.SRCCOPY);
            g.ReleaseHdc();
            _ = Gdi32.DeleteDC(hdcDest);
            _ = Gdi32.DeleteDC(hdcSrc);
            return copied;
        }
        catch
        {
        }
        return null!;
    }

    public void Stop()
    {
        _hWnd = IntPtr.Zero;
        IsCapturing = false;
    }
}
