using System;
using System.Windows.Interop;
using Vanara.PInvoke;
using Application = System.Windows.Application;

namespace Fischless.Native;

public static class DpiHelper
{
    public static float ScaleX => GetScale().X;
    public static float ScaleY => GetScale().Y;

    private static DpiScaleF GetScale()
    {
        if (OsVersionHelper.IsWindows81_OrGreater && Application.Current?.MainWindow != null)
        {
            HWND hwnd = new WindowInteropHelper(Application.Current?.MainWindow).Handle;
            HMONITOR hMonitor = User32.MonitorFromWindow(hwnd, User32.MonitorFlags.MONITOR_DEFAULTTONEAREST);
            SHCore.GetDpiForMonitor(hMonitor, SHCore.MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out uint dpiX, out uint dpiY);
            return new DpiScaleF(dpiX / 96f, dpiY / 96f);
        }

        HDC hdc = User32.GetDC(HWND.NULL);
        float scaleX = Gdi32.GetDeviceCaps(hdc, Gdi32.DeviceCap.LOGPIXELSX);
        float scaleY = Gdi32.GetDeviceCaps(hdc, Gdi32.DeviceCap.LOGPIXELSY);
        _ = User32.ReleaseDC(0, hdc);
        return new(scaleX / 96f, scaleY / 96f);
    }

    public static double CalcDPI(double src) => Math.Ceiling(src * ScaleX);
    public static float CalcDPI(float src) => (float)Math.Ceiling(src * ScaleX);
    public static double CalcDPIX(double src) => Math.Ceiling(src * ScaleX);
    public static float CalcDPIX(float src) => (float)Math.Ceiling(src * ScaleX);
    public static double CalcDPIY(double src) => Math.Ceiling(src * ScaleY);
    public static float CalcDPIY(float src) => (float)Math.Ceiling(src * ScaleY);

    public static double CalcDPi(double src) => Math.Ceiling(src * (1d / ScaleX));
    public static float CalcDPi(float src) => (float)Math.Ceiling(src * (1f / ScaleX));
    public static double CalcDPiX(double src) => Math.Ceiling(src * (1d / ScaleX));
    public static float CalcDPiX(float src) => (float)Math.Ceiling(src * (1f / ScaleX));
    public static double CalcDPiY(double src) => Math.Ceiling(src * (1d / ScaleY));
    public static float CalcDPiY(float src) => (float)Math.Ceiling(src * (1f / ScaleY));
}

internal readonly struct DpiScaleF
{
    private readonly float x;
    public float X => x;

    private readonly float y;
    public float Y => y;

    public DpiScaleF(float x = 1f, float y = 1f)
    {
        this.x = x;
        this.y = y;
    }

    public DpiScaleF Reserve()
    {
        return new DpiScaleF(1f / x, 1f / y);
    }

    public override string ToString() => $"{X},{Y}";
}
