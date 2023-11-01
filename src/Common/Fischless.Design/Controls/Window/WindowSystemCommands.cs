using Fischless.Native;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Interop;
using POINT = Vanara.PInvoke.POINT;
using User32 = Vanara.PInvoke.User32;

namespace Fischless.Design.Controls;

internal static class WindowSystemCommands
{
    [SecuritySafeCritical]
    public static void ShowSystemMenu(Window window, Point? screenLocation = null!)
    {
        if (screenLocation == null)
        {
            if (User32.GetCursorPos(out POINT pt))
            {
                SystemCommands.ShowSystemMenu(window, new Point(DpiHelper.CalcDPiX(pt.X), DpiHelper.CalcDPiY(pt.Y)));
            }
        }
        else
        {
            SystemCommands.ShowSystemMenu(window, new Point(DpiHelper.CalcDPiX(screenLocation.Value.X), DpiHelper.CalcDPiY(screenLocation.Value.Y)));
        }
    }

    [SecuritySafeCritical]
    public static void CloseWindow(Window window)
    {
        SystemCommands.CloseWindow(window);
    }

    [SecuritySafeCritical]
    public static void MaximizeWindow(Window window)
    {
        SystemCommands.MaximizeWindow(window);
    }

    [SecuritySafeCritical]
    public static void MinimizeWindow(Window window)
    {
        SystemCommands.MinimizeWindow(window);
    }

    [SecuritySafeCritical]
    public static void RestoreWindow(Window window)
    {
        if (window is WindowX windowX)
        {
            windowX.IsFullScreen = false;
        }
        SystemCommands.RestoreWindow(window);
        window.WindowStyle = WindowStyle.SingleBorderWindow;
    }

    [SecuritySafeCritical]
    public static void FastRestoreWindow(Window window, bool force = false)
    {
        if (window is WindowX windowX)
        {
            if (windowX.IsFullScreen)
            {
                if (!force)
                {
                    return;
                }
            }
            windowX.IsFullScreen = false;
        }
        _ = User32.PostMessage(new WindowInteropHelper(window).Handle, (int)User32.WindowMessage.WM_NCLBUTTONDOWN, (nint)User32.HitTestValues.HTCAPTION, IntPtr.Zero);
        window.WindowStyle = WindowStyle.SingleBorderWindow;
    }

    [SecuritySafeCritical]
    public static void MaximizeOrRestoreWindow(Window window)
    {
        if (window.WindowState == WindowState.Maximized)
        {
            RestoreWindow(window);
        }
        else
        {
            MaximizeWindow(window);
        }
    }

    public static async void FullScreenOrRestoreWindow(WindowX window)
    {
        bool isFullScreen = window.IsFullScreen;
        if (window.WindowState != WindowState.Maximized)
        {
            isFullScreen = false;
        }
        window.IsFullScreen = !isFullScreen;
        if (!isFullScreen)
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
                window.WindowStyle = WindowStyle.SingleBorderWindow;

                nint animPtr = 0x00;

                try
                {
                    User32.ANIMATIONINFO anim = default;
                    anim.cbSize = (uint)Marshal.SizeOf(anim);
                    anim.iMinAnimate = false;
                    animPtr = Marshal.AllocHGlobal((int)anim.cbSize);
                    Marshal.StructureToPtr(anim, animPtr, false);

                    if (User32.SystemParametersInfo(User32.SPI.SPI_GETANIMATION, (uint)Marshal.SizeOf(anim), animPtr, 0))
                    {
                        anim = Marshal.PtrToStructure<User32.ANIMATIONINFO>(animPtr);

                        if (anim.iMinAnimate)
                        {
                            await Task.Delay(300);
                        }
                    }
                    else
                    {
                        await Task.Delay(300);
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(animPtr);
                }
            }
            window.WindowStyle = WindowStyle.None;
            window.WindowState = WindowState.Maximized;
        }
        else
        {
            window.WindowState = WindowState.Normal;
            window.WindowStyle = WindowStyle.SingleBorderWindow;
        }
    }
}
