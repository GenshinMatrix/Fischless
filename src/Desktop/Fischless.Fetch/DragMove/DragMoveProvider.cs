using Fischless.Fetch.Launch;
using Fischless.MouseCapture;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Vanara.PInvoke;

namespace Fischless.Fetch.DragMove;

public static class DragMoveProvider
{
    private static bool isEnabled = false;

    public static bool IsEnabled
    {
        get => isEnabled;
        set
        {
            if (isEnabled != value)
            {
                isEnabled = value;
                if (value)
                {
                    latestLocation = null;
                    isMouseDown = false;
                    MouseReader.Default.MouseDown -= OnMouseDown;
                    MouseReader.Default.MouseUp -= OnMouseUp;
                    MouseReader.Default.MouseMove -= OnMouseMove;
                    MouseReader.Default.MouseDown += OnMouseDown;
                    MouseReader.Default.MouseUp += OnMouseUp;
                    MouseReader.Default.MouseMove += OnMouseMove;
                    MouseReader.Default.Start();
                }
                else
                {
                    MouseReader.Default.MouseDown -= OnMouseDown;
                    MouseReader.Default.MouseUp -= OnMouseUp;
                    MouseReader.Default.MouseMove -= OnMouseMove;
                    MouseReader.Default.Stop();
                }
            }
        }
    }

    private static Point? latestLocation = null;
    private static bool isMouseDown = false;

    private static void OnMouseDown(object? sender, MouseEventArgs e)
    {
        isMouseDown = true;
    }

    private static void OnMouseUp(object? sender, MouseEventArgs e)
    {
        isMouseDown = false;
    }

    private static void OnMouseMove(object? sender, MouseEventArgs e)
    {
        if (latestLocation == e.Location)
        {
            return;
        }

        if (latestLocation == null)
        {
            latestLocation = e.Location;
            return;
        }

        if (!isMouseDown)
        {
            latestLocation = e.Location;
            return;
        }

        if (GILauncher.TryGetProcess(out Process? p))
        {
            nint hWnd = p.MainWindowHandle;

            if (User32.GetWindowRect(hWnd, out RECT windowRect)
             && User32.GetClientRect(p.MainWindowHandle, out RECT clientRect))
            {
                if ((windowRect.bottom - windowRect.top) != (clientRect.bottom - clientRect.top))
                {
                    return;
                }

                Point lp = latestLocation ?? default;
                Point cp = e.Location;

                if (windowRect.ContainsUpper(lp, 35))
                {
                    _ = User32.MoveWindow(p.MainWindowHandle, windowRect.X + cp.X - lp.X, windowRect.Y + cp.Y - lp.Y, windowRect.Width, windowRect.Height, false);
                }
            }
        }
        latestLocation = e.Location;
    }
}

file static class RectExtensions
{
    public static bool Contains(this RECT rect, POINT point)
    {
        return point.x >= rect.left && point.x <= rect.right && point.y >= rect.top && point.y <= rect.bottom;
    }

    public static bool ContainsUpper(this RECT rect, POINT point, int height)
    {
        return point.x >= rect.left && point.x <= rect.right && point.y >= rect.top && point.y <= (rect.top + height);
    }
}
