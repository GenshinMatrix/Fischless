using Fischless.Fetch.Launch;
using Fischless.Fetch.Settings;
using Fischless.KeyboardCapture;
using Fischless.Logging;
using Fischless.MouseCapture;
using System.Drawing;
using System.Windows.Forms;
using Vanara.PInvoke;
using Process = System.Diagnostics.Process;

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

                    timer?.Dispose();
                    timer = new(OnTimerCallback, null, 0, 3000);
                }
                else
                {
                    MouseReader.Default.MouseDown -= OnMouseDown;
                    MouseReader.Default.MouseUp -= OnMouseUp;
                    MouseReader.Default.MouseMove -= OnMouseMove;
                    MouseReader.Default.Stop();

                    KeyboardReader.Default.Received -= OnKeyReceived;
                    KeyboardReader.Default.Stop();

                    timer?.Dispose();
                    timer = null!;
                }
            }
        }
    }

    private static System.Threading.Timer timer = null!;
    private static nint hWnd = IntPtr.Zero;
    private static Point? latestLocation = null;
    private static bool isMouseDown = false;
    private static bool isHover = false;

    private static void OnTimerCallback(object? state)
    {
        _ = state;

        if (!IsEnabled)
        {
            timer?.Dispose();
            timer = null!;
            return;
        }

        if (GILauncher.TryGetProcess(out Process? p))
        {
            hWnd = p.MainWindowHandle;
        }
        else
        {
            hWnd = IntPtr.Zero;
        }
    }

    private static void OnMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (hWnd != IntPtr.Zero && User32.IsWindow(hWnd))
            {
                if (User32.GetForegroundWindow() != hWnd)
                {
                    return;
                }

                if (User32.GetWindowRect(hWnd, out RECT windowRect)
                 && User32.GetClientRect(hWnd, out RECT clientRect))
                {
                    if ((windowRect.bottom - windowRect.top) != (clientRect.bottom - clientRect.top))
                    {
                        return;
                    }

                    Point lp = latestLocation ?? default;
                    Point cp = e.Location;

                    if (Math.Abs(cp.X - lp.X) < 100d || Math.Abs(cp.Y - lp.Y) < 100d)
                    {
                        if (windowRect.ContainsUpper(lp, 25))
                        {
                            isHover = true;
                            isMouseDown = true;
                        }
                        else
                        {
                            isHover = false;
                        }
                    }
                }
            }
        }
    }

    private static void OnMouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isMouseDown = false;
        }

        if (e.Button == MouseButtons.Right
         && e.Location == latestLocation
         && isHover)
        {
            if (hWnd != IntPtr.Zero && User32.IsWindow(hWnd))
            {
                if (User32.GetForegroundWindow() != hWnd)
                {
                    return;
                }

                if (User32.GetWindowRect(hWnd, out RECT windowRect)
                 && User32.GetClientRect(hWnd, out RECT clientRect))
                {
                    if ((windowRect.bottom - windowRect.top) != (clientRect.bottom - clientRect.top))
                    {
                        return;
                    }
                }

                nint hWndCurr = Process.GetCurrentProcess().MainWindowHandle;
                HMENU hMenu = User32.GetSystemMenu(hWndCurr, false);

                if (hMenu.IsNull)
                {
                    hMenu = User32.CreatePopupMenu();
                    _ = User32.AppendMenu(hMenu, User32.MenuFlags.MF_STRING, (nint)User32.SysCommand.SC_RESTORE, "Restore");
                    _ = User32.AppendMenu(hMenu, User32.MenuFlags.MF_STRING, (nint)User32.SysCommand.SC_MOVE, "Move");
                    _ = User32.AppendMenu(hMenu, User32.MenuFlags.MF_STRING, (nint)User32.SysCommand.SC_MINIMIZE, "Minimize");
                    _ = User32.AppendMenu(hMenu, User32.MenuFlags.MF_STRING, (nint)User32.SysCommand.SC_MAXIMIZE, "Maximize");
                    _ = User32.AppendMenu(hMenu, User32.MenuFlags.MF_STRING, (nint)User32.SysCommand.SC_CLOSE, "Close");
                    _ = User32.SetMenu(hWndCurr, hMenu);
                }

                if (User32.GetCursorPos(out POINT pt))
                {
                    _ = User32.EnableMenuItem(hMenu, (uint)User32.SysCommand.SC_RESTORE, User32.MenuFlags.MF_ENABLED);
                    _ = User32.EnableMenuItem(hMenu, (uint)User32.SysCommand.SC_MOVE, User32.MenuFlags.MF_ENABLED);
                    _ = User32.EnableMenuItem(hMenu, (uint)User32.SysCommand.SC_MINIMIZE, User32.MenuFlags.MF_ENABLED);
                    _ = User32.EnableMenuItem(hMenu, (uint)User32.SysCommand.SC_MAXIMIZE, User32.MenuFlags.MF_ENABLED);
                    uint command = User32.TrackPopupMenuEx(hMenu, User32.TrackPopupMenuFlags.TPM_RETURNCMD, pt.X, pt.Y, Process.GetCurrentProcess().MainWindowHandle, default);

                    if (command == (uint)User32.SysCommand.SC_MOVE)
                    {
                        if (User32.IsWindow(hWnd) && User32.GetClientRect(hWnd, out RECT lpRect))
                        {
                            Screen screen = Screen.FromHandle(hWnd);
                            _ = User32.SetWindowPos(hWnd, IntPtr.Zero, screen.Bounds.X, screen.Bounds.Y, lpRect.Width, lpRect.Height, User32.SetWindowPosFlags.SWP_NOZORDER);
                        }
                    }
                    else if (command == (uint)User32.SysCommand.SC_RESTORE)
                    {
                        if (User32.IsWindow(hWnd))
                        {
                            SettingsContainer sc = new();

                            sc.FromReg();

                            ResolutionSettings? res = sc.Resolution;

                            if (res != null)
                            {
                                _ = User32.SetWindowPos(hWnd, IntPtr.Zero, windowRect.X, windowRect.Y, res.Width, res.Height, User32.SetWindowPosFlags.SWP_NOZORDER);
                            }
                        }
                    }
                    else if (command == (uint)User32.SysCommand.SC_MINIMIZE)
                    {
                        if (User32.IsWindow(hWnd))
                        {
                            _ = User32.ShowWindow(hWnd, ShowWindowCommand.SW_MINIMIZE);
                        }
                    }
                    else if (command == (uint)User32.SysCommand.SC_MAXIMIZE)
                    {
                        if (User32.IsWindow(hWnd))
                        {
                            Screen screen = Screen.FromHandle(hWnd);
                            _ = User32.SetWindowPos(hWnd, IntPtr.Zero, screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height, User32.SetWindowPosFlags.SWP_NOZORDER);
                        }
                    }
                    else if (command == (uint)User32.SysCommand.SC_CLOSE)
                    {
                        if (User32.IsWindow(hWnd))
                        {
                            try
                            {
                                uint tid = User32.GetWindowThreadProcessId(hWnd, out uint pid);

                                if (tid != 0)
                                {
                                    using Kernel32.SafeHPROCESS hProcess = Kernel32.OpenProcess(new ACCESS_MASK(Kernel32.ProcessAccess.PROCESS_TERMINATE), false, pid);
                                    _ = Kernel32.TerminateProcess(hProcess, default);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex);
                            }
                        }
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

        if (hWnd != IntPtr.Zero && User32.IsWindow(hWnd))
        {
            if (User32.GetForegroundWindow() != hWnd)
            {
                return;
            }

            if (User32.GetWindowRect(hWnd, out RECT windowRect)
             && User32.GetClientRect(hWnd, out RECT clientRect))
            {
                if ((windowRect.bottom - windowRect.top) != (clientRect.bottom - clientRect.top))
                {
                    return;
                }

                Point lp = latestLocation ?? default;
                Point cp = e.Location;

                if (Math.Abs(cp.X - lp.X) < 100d || Math.Abs(cp.Y - lp.Y) < 100d)
                {
                    if (windowRect.ContainsUpper(lp, 25))
                    {
                        isHover = true;
                        _ = User32.MoveWindow(hWnd, windowRect.X + cp.X - lp.X, windowRect.Y + cp.Y - lp.Y, windowRect.Width, windowRect.Height, false);
                    }
                    else
                    {
                        isHover = false;
                    }
                }
            }
        }
        latestLocation = e.Location;
    }

    private static DateTime latestAlt = DateTime.MinValue;

    private static void OnKeyReceived(object? sender, KeyboardResult e)
    {
        if (e.IsCtrl)
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
                    Log.Debug("SwitchToNextWindow");
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
