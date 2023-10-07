using SharpDX.Direct3D11;
using System.Diagnostics;
using Vanara.PInvoke;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;

namespace Fischless.WindowCapture.Graphics;

public class GraphicsCapture : IWindowCapture
{
    private nint _hWnd;

    private Direct3D11CaptureFramePool _captureFramePool = null!;
    private GraphicsCaptureItem _captureItem = null!;
    private GraphicsCaptureSession _captureSession = null!;
    private ResourceRegion? _region = null;

    public bool IsCapturing { get; private set; }
    public bool IsClientEnabled { get; set; } = false;

    public void Dispose()
    {
        Stop();
    }

    public void Start(nint hWnd)
    {
        _hWnd = hWnd;

        _region = GetGameScreenRegion(hWnd);

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
        _captureSession.IsCursorCaptureEnabled = false;
        _captureSession.IsBorderRequired = false;
        _captureSession.StartCapture();
        IsCapturing = true;
    }

    public Bitmap? Capture()
    {
        if (_hWnd == IntPtr.Zero)
        {
            return null;
        }

        try
        {
            using var frame = _captureFramePool?.TryGetNextFrame();

            if (IsClientEnabled)
            {
                return frame?.ToBitmap();
            }
            else
            {
                return frame?.ToBitmap(_region);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
        return null;
    }

    public Bitmap? Capture(int x, int y, int width, int height)
    {
        if (_hWnd == IntPtr.Zero)
        {
            return null;
        }

        try
        {
            using var frame = _captureFramePool?.TryGetNextFrame();

            if (frame == null)
            {
                return null!;
            }

            ResourceRegion regionBase = _region ?? default;
            ResourceRegion region = new()
            {
                Left = regionBase.Left + x,
                Top = regionBase.Top + y,
                Right = regionBase.Left + x + width,
                Bottom = regionBase.Top + x + height,
                Front = regionBase.Front,
                Back = regionBase.Back,
            };

            return frame.ToBitmap(region);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
        return null;
    }

    public void Stop()
    {
        _captureSession?.Dispose();
        _captureFramePool?.Dispose();
        _captureSession = null!;
        _captureFramePool = null!;
        _captureItem = null!;
        _region = default;

        _hWnd = IntPtr.Zero;
        IsCapturing = false;
    }

    private void CaptureItemOnClosed(GraphicsCaptureItem sender, object args)
    {
        Stop();
    }

    private ResourceRegion? GetGameScreenRegion(nint hWnd)
    {
        if (CaptionHelper.IsFullScreenMode(hWnd))
        {
            return null!;
        }

        ResourceRegion region = new();
        DwmApi.DwmGetWindowAttribute<RECT>(hWnd, DwmApi.DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out var windowRect);
        User32.GetClientRect(_hWnd, out RECT clientRect);

        region.Left = 0;
        region.Top = windowRect.Height - clientRect.Height;
        region.Right = clientRect.Width;
        region.Bottom = windowRect.Height;
        region.Front = 0;
        region.Back = 1;

        return region;
    }
}
