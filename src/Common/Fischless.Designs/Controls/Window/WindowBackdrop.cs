using Fischless.Native;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Xml.Linq;
using Vanara.PInvoke;

namespace Fischless.Designs.Controls;

public static class WindowBackdrop
{
    public static bool IsSupported(WindowBackdropType backdropType)
    {
        return backdropType switch
        {
            WindowBackdropType.Auto => OsVersionHelper.IsWindows11_22523,
            WindowBackdropType.Tabbed => OsVersionHelper.IsWindows11_22523,
            WindowBackdropType.Mica => OsVersionHelper.IsWindows11_OrGreater,
            WindowBackdropType.Acrylic => OsVersionHelper.IsWindows7_OrGreater,
            WindowBackdropType.None => true,
            _ => false
        };
    }

    private static bool ApplyWindowDarkMode(nint handle)
    {
        if (handle == 0x00)
            return false;

        if (!User32.IsWindow(handle))
            return false;

        nint pvAttribute = Marshal.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(pvAttribute, 0x1); // Enable
        var dwAttribute = DwmApi.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;

        if (!OsVersionHelper.IsWindows11_22523_OrGreater)
            dwAttribute = (DwmApi.DWMWINDOWATTRIBUTE)DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_OLD;

        _ = DwmApi.DwmSetWindowAttribute(
            handle,
            dwAttribute,
            pvAttribute,
            Marshal.SizeOf(typeof(int))
        );
        Marshal.FreeHGlobal(pvAttribute);

        return true;
    }

    private static bool RemoveWindowDarkMode(nint handle)
    {
        if (handle == 0x00)
            return false;

        if (!User32.IsWindow(handle))
            return false;

        nint pvAttribute = Marshal.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(pvAttribute, 0x0); // Disable
        var dwAttribute = DwmApi.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;

        if (!OsVersionHelper.IsWindows11_22523_OrGreater)
            dwAttribute = (DwmApi.DWMWINDOWATTRIBUTE)DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_OLD;

        _ = DwmApi.DwmSetWindowAttribute(
            handle,
            dwAttribute,
            pvAttribute,
            Marshal.SizeOf(typeof(int))
        );
        Marshal.FreeHGlobal(pvAttribute);

        return true;
    }

    private static bool RemoveWindowCaption(nint hWnd)
    {
        if (hWnd == 0x00)
            return false;

        if (!User32.IsWindow(hWnd))
            return false;

        var wtaOptions = new UxTheme.WTA_OPTIONS()
        {
            Flags = UxTheme.WTNCA.WTNCA_NODRAWCAPTION,
            Mask = (uint)UxTheme.ThemeDialogTextureFlags.ETDT_VALIDBITS,
        };

        UxTheme.SetWindowThemeAttribute(
            hWnd,
            UxTheme.WINDOWTHEMEATTRIBUTETYPE.WTA_NONCLIENT,
            wtaOptions,
            (uint)Marshal.SizeOf(typeof(UxTheme.WTA_OPTIONS))
        );

        return true;
    }

    public static bool ApplyBackdrop(nint hWnd, WindowBackdropType backdropType)
    {
        if (hWnd == 0x00)
            return false;

        if (!User32.IsWindow(hWnd))
            return false;

        if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark)
        {
            ApplyWindowDarkMode(hWnd);
        }
        else
        {
            RemoveWindowDarkMode(hWnd);
        }

        RemoveWindowCaption(hWnd);

        // 22H1
        if (!OsVersionHelper.IsWindows11_22523_OrGreater)
        {
            if (backdropType == WindowBackdropType.Mica || backdropType == WindowBackdropType.Auto)
                return ApplyLegacyMicaBackdrop(hWnd);

            if (backdropType == WindowBackdropType.Acrylic)
                return ApplyLegacyAcrylicBackdrop(hWnd);

            return false;
        }

        switch (backdropType)
        {
            case WindowBackdropType.Auto:
                return ApplyDwmwWindowAttrubute(hWnd, DwmApi.DWM_SYSTEMBACKDROP_TYPE.DWMSBT_AUTO);

            case WindowBackdropType.Mica:
                return ApplyDwmwWindowAttrubute(hWnd, DwmApi.DWM_SYSTEMBACKDROP_TYPE.DWMSBT_MAINWINDOW);

            case WindowBackdropType.Acrylic:
                return ApplyDwmwWindowAttrubute(hWnd, DwmApi.DWM_SYSTEMBACKDROP_TYPE.DWMSBT_TRANSIENTWINDOW);

            case WindowBackdropType.Tabbed:
                return ApplyDwmwWindowAttrubute(hWnd, DwmApi.DWM_SYSTEMBACKDROP_TYPE.DWMSBT_TABBEDWINDOW);
        }

        return ApplyDwmwWindowAttrubute(hWnd, DwmApi.DWM_SYSTEMBACKDROP_TYPE.DWMSBT_NONE);
    }

    private static bool RemoveBackdrop(Window window)
    {
        if (window == null)
            return false;

        nint windowHandle = new WindowInteropHelper(window).Handle;

        return RemoveBackdrop(windowHandle);
    }

    private static bool RemoveBackdrop(nint hWnd)
    {
        if (hWnd == 0x00)
            return false;

        RestoreContentBackground(hWnd);

        if (hWnd == 0x00)
            return false;

        if (!User32.IsWindow(hWnd))
            return false;

        nint pvAttribute = Marshal.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(pvAttribute, 0x0); // Disable

        _ = DwmApi.DwmSetWindowAttribute(
            hWnd,
            (DwmApi.DWMWINDOWATTRIBUTE)DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT,
            pvAttribute,
            Marshal.SizeOf(typeof(int))
        );
        Marshal.FreeHGlobal(pvAttribute);

        nint backdropPvAttribute = Marshal.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(backdropPvAttribute, (int)DwmApi.DWM_SYSTEMBACKDROP_TYPE.DWMSBT_NONE);

        _ = DwmApi.DwmSetWindowAttribute(
            hWnd,
            (DwmApi.DWMWINDOWATTRIBUTE)DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE,
            backdropPvAttribute,
            Marshal.SizeOf(typeof(int))
        );
        Marshal.FreeHGlobal(backdropPvAttribute);

        return true;
    }

    public static bool ApplyBackdrop(Window window, WindowBackdropType backdropType)
    {
        if (window is null)
            return false;

        if (window.IsLoaded)
        {
            nint windowHandle = new WindowInteropHelper(window).Handle;

            if (windowHandle == 0x00)
                return false;

            return ApplyBackdrop(windowHandle, backdropType);
        }

        window.Loaded += (sender, _) =>
        {
            nint windowHandle =
                new WindowInteropHelper(sender as Window ?? null)?.Handle
                ?? IntPtr.Zero;

            if (windowHandle == 0x00)
                return;

            ApplyBackdrop(windowHandle, backdropType);
        };

        return true;
    }

    public static bool RemoveBackground(Window window)
    {
        if (window == null)
            return false;

        // Remove background from visual root
        window.Background = Brushes.Transparent;

        nint windowHandle = new WindowInteropHelper(window).Handle;

        if (windowHandle == 0x00)
            return false;

        var windowSource = HwndSource.FromHwnd(windowHandle);

        // Remove background from client area
        if (windowSource?.Handle.ToInt32() != 0x00 && windowSource?.CompositionTarget != null)
            windowSource.CompositionTarget.BackgroundColor = Colors.Transparent;

        return true;
    }

    private static bool ApplyDwmwWindowAttrubute(nint hWnd, DwmApi.DWM_SYSTEMBACKDROP_TYPE dwmSbt)
    {
        if (hWnd == 0x00)
            return false;

        if (!User32.IsWindow(hWnd))
            return false;

        nint backdropPvAttribute = Marshal.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(backdropPvAttribute, (int)dwmSbt);

        var dwmApiResult = DwmApi.DwmSetWindowAttribute(
            hWnd,
            (DwmApi.DWMWINDOWATTRIBUTE)DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE,
            backdropPvAttribute,
            Marshal.SizeOf(typeof(int))
        );
        Marshal.FreeHGlobal(backdropPvAttribute);

        return dwmApiResult == HRESULT.S_OK;
    }

    private static bool ApplyLegacyMicaBackdrop(nint hWnd)
    {
        nint backdropPvAttribute = Marshal.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(backdropPvAttribute, 1); // Enable

        var dwmApiResult = DwmApi.DwmSetWindowAttribute(
            hWnd,
            (DwmApi.DWMWINDOWATTRIBUTE)DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT,
            backdropPvAttribute,
            Marshal.SizeOf(typeof(int))
        );
        Marshal.FreeHGlobal(backdropPvAttribute);

        return dwmApiResult == HRESULT.S_OK;
    }

    private static bool ApplyLegacyAcrylicBackdrop(nint hWnd)
    {
        throw new NotImplementedException();
    }

    private static bool RestoreContentBackground(nint hWnd)
    {
        if (hWnd == 0x00)
            return false;

        if (!User32.IsWindow(hWnd))
            return false;

        var windowSource = HwndSource.FromHwnd(hWnd);

        // Restore client area background
        if (windowSource?.Handle.ToInt32() != 0x00 && windowSource?.CompositionTarget != null)
            windowSource.CompositionTarget.BackgroundColor = SystemColors.WindowColor;

        if (windowSource?.RootVisual is System.Windows.Window window)
        {
            var backgroundBrush = window.Resources["ApplicationBackgroundBrush"];

            // Manual fallback
            if (backgroundBrush is not SolidColorBrush)
                backgroundBrush = GetFallbackBackgroundBrush();

            window.Background = (SolidColorBrush)backgroundBrush;
        }

        return true;
    }

    private static Brush GetFallbackBackgroundBrush()
    {
        return ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark
            ? new SolidColorBrush(Color.FromArgb(0xFF, 0x20, 0x20, 0x20))
            : new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0xFA, 0xFA));
    }
}
