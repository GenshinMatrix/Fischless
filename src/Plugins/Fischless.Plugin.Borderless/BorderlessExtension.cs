using System.Windows.Forms;
using Vanara.PInvoke;

namespace Fischless.Plugin.Borderless;

internal static class BorderlessExtension
{
    public static void EnableWindowBorderless(this nint hWnd)
    {
        nint dwStyle = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE);

        dwStyle = dwStyle.ToInt32() & ~(int)(User32.WindowStyles.WS_BORDER | User32.WindowStyles.WS_DLGFRAME | User32.WindowStyles.WS_CAPTION | User32.WindowStyles.WS_SYSMENU | User32.WindowStyles.WS_MINIMIZEBOX | User32.WindowStyles.WS_MAXIMIZEBOX);

        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE, dwStyle);

        _ = User32.SetWindowPos(hWnd, 0, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE | User32.SetWindowPosFlags.SWP_FRAMECHANGED);
    }

    public static void DisableWindowBorderless(this nint hWnd)
    {
        nint dwStyle = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE);

        dwStyle = dwStyle.ToInt32() | (int)(User32.WindowStyles.WS_BORDER | User32.WindowStyles.WS_DLGFRAME | User32.WindowStyles.WS_CAPTION | User32.WindowStyles.WS_SYSMENU | User32.WindowStyles.WS_MINIMIZEBOX | User32.WindowStyles.WS_MAXIMIZEBOX);

        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE, dwStyle);

        _ = User32.SetWindowPos(hWnd, 0, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE | User32.SetWindowPosFlags.SWP_FRAMECHANGED);
    }

    public static void EnableWindowMaximizeBox(this nint hWnd)
    {
        nint dwStyle = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE);

        dwStyle = dwStyle.ToInt32() | (int)User32.WindowStyles.WS_MAXIMIZEBOX;

        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE, dwStyle);
    }

    public static void EnableWindowTopmost(this nint hWnd)
    {
        _ = User32.SetWindowPos(hWnd, User32.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE);
    }

    public static void DisableWindowTopmost(this nint hWnd)
    {
        _ = User32.SetWindowPos(hWnd, User32.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE);
    }

    public static void RestoreWindowPositon(this nint hWnd)
    {
        Screen screen = Screen.FromHandle(hWnd);
        _ = User32.SetWindowPos(hWnd, IntPtr.Zero, screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height, User32.SetWindowPosFlags.SWP_NOZORDER);
    }
}
