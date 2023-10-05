﻿using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace Fischless.Native;

public static class NativeMethods
{
    public static void Focus(nint hWnd)
    {
        if (!User32.IsZoomed(hWnd))
        {
            _ = User32.SendMessage(hWnd, User32.WindowMessage.WM_SYSCOMMAND, User32.SysCommand.SC_RESTORE, IntPtr.Zero);
        }
        _ = User32.SetForegroundWindow(hWnd);
        while (User32.IsIconic(hWnd))
        {
            continue;
        }
        _ = User32.BringWindowToTop(hWnd);
    }

    public static string MB_GetString(DialogBoxCommand wBtn)
    {
        return Marshal.PtrToStringAuto(User32Ex.MB_GetString((uint)wBtn));
    }

    public static void HideAllWindowButton(nint hWnd)
    {
        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE, User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE) & ~(int)User32.WindowStyles.WS_SYSMENU);
    }

    public static bool ApplyWindowCornerPreference(nint handle, DwmApi.DWM_WINDOW_CORNER_PREFERENCE cornerPreference)
    {
        if (handle == 0x00)
            return false;

        if (!User32.IsWindow(handle))
            return false;

        nint pvAttribute = Marshal.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(pvAttribute, (int)cornerPreference);

        _ = DwmApi.DwmSetWindowAttribute(
            handle,
            DwmApi.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE,
            pvAttribute,
            Marshal.SizeOf(typeof(int))
        );
        Marshal.FreeHGlobal(pvAttribute);

        return true;
    }
}
