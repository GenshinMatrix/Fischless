using Vanara.PInvoke;

namespace Fischless.Fetch.AutoTrack;

/// <summary>
/// Impl from https://github.com/GengGode/cvAutoTrack/blob/master/cvAutoTrack/src/genshin/genshin.handle.cpp
/// </summary>
public static class ResponsiveProvider
{
    public static ResponsiveRecord GetResponsiveRecord(nint hWnd)
    {
        ResponsiveRecord record = default;

        if ((User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE) & (int)User32.WindowStyles.WS_CAPTION) != 0)
        {
            record.is_exist_title_bar = true;
        }
        else
        {
            record.is_exist_title_bar = false;
        }

        _ = User32.GetWindowRect(hWnd, out record.rect);
        _ = User32.GetClientRect(hWnd, out record.rect_client);

        HMONITOR hMonitor = User32.MonitorFromWindow(hWnd, User32.MonitorFlags.MONITOR_DEFAULTTONEAREST);
        _ = SHCore.GetDpiForMonitor(hMonitor, SHCore.MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out uint dpiX, out _);
        record.scale = dpiX / 96d;

        int x = record.rect_client.right - record.rect_client.left;
        int y = record.rect_client.bottom - record.rect_client.top;

        double f, fx, fy;

        if (Math.Abs((double)x / y - 16d / 9d) < 0.001d)
        {
            record.size_frame = new SIZE(1920, 1080);
        }
        else if ((double)x / y > 16d / 9d)
        {
            f = y / 1080d;
            fx = x / f;
            record.size_frame = new SIZE((int)fx, 1080);
        }
        else
        {
            f = x / 1920d;
            fy = y / f;
            record.size_frame = new SIZE(1920, (int)fy);
        }

        x = record.size_frame.Width;
        y = record.size_frame.Height;

        int paimonMayAreaLeft = 0;
        int paimonMayAreaTop = 0;
        int paimonMayAreaWidth = (int)(x * 0.1d);
        int paimonMayAreaHeight = (int)(y * 0.1d);
        record.rect_paimon_maybe = new RECT(paimonMayAreaLeft, paimonMayAreaTop, paimonMayAreaWidth, paimonMayAreaHeight);

        int miniMapCailbMayAreaLeft = (int)(x * 0.08d);
        int miniMapCailbMayAreaTop = 0;
        int miniMapCailbMayAreaWidth = (int)(x * 0.1d);
        int miniMapCailbMayAreaHeight = (int)(y * 0.1d);
        record.rect_minimap_cailb_maybe = new RECT(miniMapCailbMayAreaLeft, miniMapCailbMayAreaTop, miniMapCailbMayAreaWidth, miniMapCailbMayAreaHeight);

        int miniMapMayAreaLeft = 0;
        int miniMapMayAreaTop = 0;
        int miniMapMayAreaWidth = (int)(x * 0.18d);
        int miniMapMayAreaHeight = (int)(y * 0.22d);
        record.rect_minimap_maybe = new RECT(miniMapMayAreaLeft, miniMapMayAreaTop, miniMapMayAreaWidth, miniMapMayAreaHeight);

        int uidMayAreaLeft = (int)(x * 0.88d);
        int uidMayAreaTop = (int)(y * 0.97d);
        int uidMayAreaWidth = x - uidMayAreaLeft;
        int uidMayAreaHeight = y - uidMayAreaTop;
        record.rect_uid_maybe = new RECT(uidMayAreaLeft, uidMayAreaTop, uidMayAreaWidth, uidMayAreaHeight);

        int uidRectX = (int)Math.Ceiling(x - x * (1d - 0.865d));
        int uidRectY = (int)Math.Ceiling(y - 1080d * (1d - 0.9755d));
        int uidRectW = (int)Math.Ceiling(1920d * 0.11d);
        int uidRectH = (int)Math.Ceiling(1920d * 0.0938d * 0.11d);
        record.rect_uid = new RECT(uidRectX, uidRectY, uidRectW, uidRectH);

        int leftGetItemsMayAreaLeft = (int)(x * 0.57d);
        int leftGetItemsMayAreaTop = (int)(y * 0.25d);
        int leftGetItemsMayAreaWidth = (int)(x * 0.225d);
        int leftGetItemsMayAreaHeight = (int)(y * 0.5d);
        record.rect_left_give_items_maybe = new RECT(leftGetItemsMayAreaLeft, leftGetItemsMayAreaTop, leftGetItemsMayAreaWidth, leftGetItemsMayAreaHeight);

        int rightGetItemsMayAreaLeft = (int)(x * 0.05d);
        int rightGetItemsMayAreaTop = (int)(y * 0.46d);
        int rightGetItemsMayAreaWidth = (int)(x * 0.16d);
        int rightGetItemsMayAreaHeight = (int)(y * 0.48d);
        record.rect_right_pick_items_maybe = new RECT(rightGetItemsMayAreaLeft, rightGetItemsMayAreaTop, rightGetItemsMayAreaWidth, rightGetItemsMayAreaHeight);

        return record;
    }
}

public ref struct ResponsiveRecord
{
    public bool is_exist;
    public bool is_exist_title_bar;
    public RECT rect;
    public RECT rect_client;
    public double scale;
    public SIZE size_frame;
    public RECT rect_paimon_maybe;
    public RECT rect_minimap_cailb_maybe;
    public RECT rect_minimap_maybe;
    public RECT rect_uid_maybe;
    public RECT rect_uid;
    public RECT rect_left_give_items_maybe;
    public RECT rect_right_pick_items_maybe;
    public bool is_force_used_no_alpha;
}
