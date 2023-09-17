using Fischless.WindowCapture.GraphicsCapture.Helpers;
using System.Drawing;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;

namespace Fischless.WindowCapture.GraphicsCapture;

public class GraphicsCapture : IWindowCapture
{
    private nint _hWnd;

    private Direct3D11CaptureFramePool _captureFramePool;
    private GraphicsCaptureItem _captureItem;
    private GraphicsCaptureSession _captureSession;

    public bool IsCapturing { get; private set; }

    public void Start(nint hWnd)
    {
        _hWnd = hWnd;
        IsCapturing = true;

        _captureItem = CaptureHelper.CreateItemForWindow(_hWnd);

        if (_captureItem == null)
        {
            throw new InvalidOperationException("Failed to create capture item.");
        }

        _captureItem.Closed += CaptureItemOnClosed;

        var device = Direct3D11Helper.CreateDevice();

        _captureFramePool = Direct3D11CaptureFramePool.Create(device, DirectXPixelFormat.B8G8R8A8UIntNormalized, 2,
            _captureItem.Size);
        _captureSession = _captureFramePool.CreateCaptureSession(_captureItem);
        _captureSession.StartCapture();
        IsCapturing = true;
    }

    public Bitmap? Capture()
    {
        using var frame = _captureFramePool?.TryGetNextFrame();
        return frame?.ToBitmap();
    }

    public void Stop()
    {
        _captureSession?.Dispose();
        _captureFramePool?.Dispose();
        _captureSession = null;
        _captureFramePool = null;
        _captureItem = null;

        _hWnd = IntPtr.Zero;
        IsCapturing = false;
    }

    private void CaptureItemOnClosed(GraphicsCaptureItem sender, object args)
    {
        Stop();
    }
}
