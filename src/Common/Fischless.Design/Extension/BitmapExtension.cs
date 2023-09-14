using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Vanara.PInvoke;

namespace Fischless.Design.Extension;

public static class BitmapExtension
{
    public static ImageSource ToImageSource(this Bitmap bitmap)
    {
        nint hBitmap = bitmap.GetHbitmap();
        ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

        _ = Gdi32.DeleteObject(hBitmap);
        return imageSource;
    }

    public static BitmapSource ToBitmapSource(this Bitmap bitmap)
    {
        try
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        catch
        {
        }
        return null!;
    }
}
