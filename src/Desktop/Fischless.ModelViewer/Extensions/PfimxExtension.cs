using Pfim;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PfimImageFormat = Pfim.ImageFormat;
using PixelFormat = System.Windows.Media.PixelFormat;

namespace Fischless.ModelViewer.Extensions;

internal static class PfimxExtension
{
    public static bool IsPfimx(string ext) =>
        (ext?.Equals(".dds", StringComparison.OrdinalIgnoreCase) ?? false)
     || (ext?.Equals(".tga", StringComparison.OrdinalIgnoreCase) ?? false);

    public static ImageSource ToBitmapImage(Stream stream)
    {
        IImage image = Pfimage.FromStream(stream);
        return image.ToBitmapImage();
    }

    public static ImageSource ToBitmapImage(string path)
    {
        IImage image = Pfimage.FromFile(path);
        return image.ToBitmapImage();
    }

    public static ImageSource ToBitmapImage(this IImage image)
    {
        PixelFormat pixelFormat = image.Format switch
        {
            PfimImageFormat.Rgb24 => PixelFormats.Bgr24,
            PfimImageFormat.Rgba32 => PixelFormats.Bgra32,
            PfimImageFormat.Rgb8 => PixelFormats.Gray8,
            PfimImageFormat.R5g5b5a1 => PixelFormats.Bgr555,
            PfimImageFormat.R5g5b5 => PixelFormats.Bgr555,
            PfimImageFormat.R5g6b5 => PixelFormats.Bgr565,
            _ => throw new Exception($"Unable to convert {image.Format} to PixelFormat"),
        };
        GCHandle pinnedArray = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
        nint addr = pinnedArray.AddrOfPinnedObject();
        BitmapSource bsource = BitmapSource.Create(image.Width, image.Height, 96.0, 96.0, pixelFormat, null, addr, image.DataLen, image.Stride);
        //pinnedArray.Free();
        return bsource;
    }
}
