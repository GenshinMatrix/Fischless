using Fischless.Fetch.AutoTrack;
using Fischless.Fetch.Launch;
using Fischless.WindowCapture;
using System.Diagnostics;
using System.Drawing;
using Vanara.PInvoke;

namespace Fischless.Fetch.Responsive;

public static class ResponsiveTester
{
    public static void Benchmark()
    {
        IWindowCapture capture = WindowCaptureFactory.Create(CaptureMode.BitBlt);

        if (GILauncher.TryGetProcess(out Process? process))
        {
            capture.Start(process.MainWindowHandle);
            using Bitmap? bitmap = capture.Capture();

            if (bitmap != null)
            {
                ResponsiveRecord rec = ResponsiveProvider.GetResponsiveRecord(process.MainWindowHandle);
                using Graphics g = Graphics.FromImage(bitmap);
                using Pen p = new(Color.Red);

                RectangleF[] rectfs = new RECT[]
                {
                    rec.rect_paimon_maybe,
                    rec.rect_minimap_cailb_maybe,
                    rec.rect_minimap_maybe,
                    rec.rect_uid_maybe,
                    rec.rect_uid,
                    rec.rect_left_give_items_maybe,
                    rec.rect_right_pick_items_maybe,
                }
               .Select(r => new RectangleF(r.Left, r.Top, r.Right, r.Bottom)).ToArray();

                g.DrawRectangles(p, rectfs);

                try
                {
                    bitmap.Save("ResponsiveTester.jpg");
                }
                catch
                {
                    ///
                }
            }
        }
    }

    public static Bitmap GetTestResponsiveBitmap(nint hWnd)
    {
        return null!;
    }
}
