using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Vanara.PInvoke;

namespace Fischless.Design.Markups;

[MarkupExtensionReturnType(typeof(Cursor))]
public class CursorExtension : MarkupExtension
{
    public Uri CursorUri { get; set; } = null!;
    public int HotSpotX { get; set; } = default;
    public int HotSpotY { get; set; } = default;

    public CursorExtension()
    {
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return CreateCursorFromPng(CursorUri, HotSpotX, HotSpotY);
    }

    internal static Cursor CreateCursorFromPng(Uri cursorUri, int hotSpotX, int hotSpotY)
    {
        if (cursorUri == null)
        {
            return null;
        }

        var pngBitmap = new BitmapImage(cursorUri);
        var cursorWidth = pngBitmap.PixelWidth;
        var cursorHeight = pngBitmap.PixelHeight;

        var pngRenderTarget = new RenderTargetBitmap(cursorWidth, cursorHeight, 96d, 96d, PixelFormats.Pbgra32);
        pngRenderTarget.Render(new Image { Source = pngBitmap });

        var pixelsBuffer = new byte[cursorWidth * cursorHeight * 4];
        pngRenderTarget.CopyPixels(pixelsBuffer, cursorWidth * 4, 0);

        var hBitmap = BitmapSourceToHBitmap(pngBitmap);

        var cursorInfo = new User32.ICONINFO
        {
            fIcon = false,
            xHotspot = hotSpotX,
            yHotspot = hotSpotY,
            hbmMask = hBitmap,
            hbmColor = hBitmap,
        };

        User32.SafeHICON cursorPtr = User32.CreateIconIndirect(cursorInfo);
        return CursorInteropHelper.Create(cursorPtr);
    }

    internal static nint BitmapSourceToHBitmap(BitmapSource bitmapSource)
    {
        int width = bitmapSource.PixelWidth;
        int height = bitmapSource.PixelHeight;
        var format = bitmapSource.Format;

        Gdi32.SafeHBITMAP safeHBitmap;
        Gdi32.BITMAPINFO pbmi = new()
        {
            bmiHeader = new Gdi32.BITMAPINFOHEADER()
            {
                biSize = (uint)Marshal.SizeOf<Gdi32.BITMAPINFOHEADER>(),
                biWidth = width,
                biHeight = -height,
                biPlanes = 1,
                biBitCount = (ushort)format.BitsPerPixel,
                biCompression = Gdi32.BitmapCompressionMode.BI_RGB,
            }
        };

        using var safeHdc = Gdi32.CreateCompatibleDC(HDC.NULL);

        if (safeHdc.IsInvalid)
        {
            throw new InvalidOperationException("Failed to create a compatible device context.");
        }

        safeHBitmap = Gdi32.CreateDIBSection(safeHdc, pbmi, Gdi32.DIBColorMode.DIB_RGB_COLORS, out nint pBits, Gdi32.HSECTION.NULL, 0);

        if (safeHBitmap.IsInvalid)
        {
            throw new InvalidOperationException("Failed to create DIB section.");
        }

        bitmapSource.CopyPixels(Int32Rect.Empty, pBits, height * width * format.BitsPerPixel / 8, width * format.BitsPerPixel / 8);

        nint hBitmap = safeHBitmap.ReleaseOwnership();
        return hBitmap;
    }
}
