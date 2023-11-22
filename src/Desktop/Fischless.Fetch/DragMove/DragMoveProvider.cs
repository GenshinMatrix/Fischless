using Fischless.Fetch.Launch;
using Fischless.KeyboardCapture;
using Fischless.Logging;
using Fischless.MouseCapture;
using Fischless.Native;
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

                    KeyboardReader.Default.Received += OnKeyReceived;
                    KeyboardReader.Default.Start();
                }
                else
                {
                    MouseReader.Default.MouseDown -= OnMouseDown;
                    MouseReader.Default.MouseUp -= OnMouseUp;
                    MouseReader.Default.MouseMove -= OnMouseMove;
                    MouseReader.Default.Stop();

                    KeyboardReader.Default.Received -= OnKeyReceived;
                    KeyboardReader.Default.Stop();
                }
            }
        }
    }

    private static Point? latestLocation = null;
    private static bool isMouseDown = false;
    private static bool isHover = false;

    private static void OnMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            HMENU hMenu = User32.GetSystemMenu(Process.GetCurrentProcess().MainWindowHandle, false);

            if (!hMenu.IsNull)
            {
                //_ = User32.DestroyMenu(hMenu);
            }
        }
        isMouseDown = true;
    }

    private static void OnMouseUp(object? sender, MouseEventArgs e)
    {
        isMouseDown = false;

        if (isHover && e.Button == MouseButtons.Right)
        {
            if (GILauncher.TryGetProcess(out Process? p))
            {
                nint hWnd = Process.GetCurrentProcess().MainWindowHandle;
                HMENU hMenu = User32.GetSystemMenu(hWnd, false);

                if (hMenu.IsNull)
                {
                    hMenu = User32.CreatePopupMenu();
                    _ = User32.AppendMenu(hMenu, User32.MenuFlags.MF_STRING, (nint)User32.SysCommand.SC_CLOSE, "Close");
                    _ = User32.AppendMenu(hMenu, User32.MenuFlags.MF_STRING, (nint)User32.SysCommand.SC_MINIMIZE, "Minimize");
                    _ = User32.AppendMenu(hMenu, User32.MenuFlags.MF_STRING, (nint)User32.SysCommand.SC_MAXIMIZE, "Maximize");
                    _ = User32.SetMenu(hWnd, hMenu);
                }

                if (User32.GetCursorPos(out POINT pt))
                {
                    _ = User32.EnableMenuItem(hMenu, (uint)User32.SysCommand.SC_MINIMIZE, User32.MenuFlags.MF_ENABLED);
                    _ = User32.EnableMenuItem(hMenu, (uint)User32.SysCommand.SC_MAXIMIZE, User32.MenuFlags.MF_ENABLED);
                    uint command = User32.TrackPopupMenuEx(hMenu, User32.TrackPopupMenuFlags.TPM_RETURNCMD, (int)DpiHelper.CalcDPiX(pt.X), (int)DpiHelper.CalcDPiY(pt.Y), Process.GetCurrentProcess().MainWindowHandle, default);

                    if (command == (uint)User32.SysCommand.SC_CLOSE)
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex);
                        }
                    }
                    else if (command == (uint)User32.SysCommand.SC_MINIMIZE)
                    {
                        _ = User32.ShowWindow(p.MainWindowHandle, ShowWindowCommand.SW_MINIMIZE);
                    }
                    else if (command == (uint)User32.SysCommand.SC_MAXIMIZE)
                    {
                        Screen screen = Screen.FromHandle(p.MainWindowHandle);
                        _ = User32.SetWindowPos(p.MainWindowHandle, IntPtr.Zero, screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height, User32.SetWindowPosFlags.SWP_NOZORDER);
                    }
                }
            }
        }
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

        if (e.Button == MouseButtons.Right)
        {
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
                    isHover = true;
                    _ = User32.MoveWindow(p.MainWindowHandle, windowRect.X + cp.X - lp.X, windowRect.Y + cp.Y - lp.Y, windowRect.Width, windowRect.Height, false);
                }
                else
                {
                    isHover = false;
                }
            }
        }
        latestLocation = e.Location;
    }

    private static DateTime latestAlt = DateTime.MinValue;

    private static void OnKeyReceived(object? sender, KeyboardResult e)
    {
        if (e.IsAlt)
        {
            int interval = (int)(e.KeyboardItem.DateTime - latestAlt).TotalMilliseconds;

            if (interval > 100 && interval <= 500)
            {
                HWND hWnd = User32.GetForegroundWindow();
                _ = User32.GetWindowThreadProcessId(hWnd, out uint pid);
                Process process = Process.GetProcessById((int)pid);

                if (process != null && (process.ProcessName == GILauncher.ProcessNameCN || process.ProcessName == GILauncher.ProcessNameOVERSEA))
                {
                    User32.SwitchToThisWindow(User32.GetWindow(hWnd, User32.GetWindowCmd.GW_HWNDNEXT), true);
                }
            }

            latestAlt = e.KeyboardItem.DateTime;
        }
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
