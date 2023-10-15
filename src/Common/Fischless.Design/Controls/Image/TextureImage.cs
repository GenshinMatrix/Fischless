using Pfim;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PfimImageFormat = Pfim.ImageFormat;
using PixelFormat = System.Windows.Media.PixelFormat;

namespace Fischless.Design.Controls;

public class TextureImage : CachedImage
{
    public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
        nameof(Path),
        typeof(string),
        typeof(TextureImage),
        new PropertyMetadata(string.Empty, OnPathChanged)
    );

    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    static TextureImage()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextureImage), new FrameworkPropertyMetadata(typeof(TextureImage)));
    }

    private static void OnPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextureImage customImage && e.NewValue is string path)
        {
            try
            {
                FileInfo fileInfo = new(customImage.Path);

                if ((fileInfo.Extension?.EndsWith(".dds", StringComparison.OrdinalIgnoreCase) ?? false)
                 || (fileInfo.Extension?.EndsWith(".tga", StringComparison.OrdinalIgnoreCase) ?? false))
                {
                    if (path.StartsWith("file:///"))
                    {
                        path = path.Replace("file:///", string.Empty);
                    }
                    customImage.Source = PfimxUtils.ToBitmapImage(path);
                }
                else
                {
                    customImage.Source = new BitmapImage(new Uri(path));
                }
            }
            catch
            {
                customImage.Source = null!;
            }
        }
    }
}

internal static class PfimxUtils
{
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
        var pinnedArray = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
        var addr = pinnedArray.AddrOfPinnedObject();
        var bsource = BitmapSource.Create(image.Width, image.Height, 96.0, 96.0, pixelFormat, null, addr, image.DataLen, image.Stride);

        return bsource;
    }
}