using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fischless.Design.Controls;

public class CachedImage : Image
{
    public static Dictionary<string, ImageSource> Caches { get; private set; } = new();

    static CachedImage()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CachedImage), new FrameworkPropertyMetadata(typeof(CachedImage)));
    }

    public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
        nameof(Path),
        typeof(string),
        typeof(CachedImage),
        new PropertyMetadata(string.Empty, OnPathChanged)
    );

    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    private static void OnPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CachedImage self && e.NewValue is string path)
        {
            try
            {
                if (Caches.ContainsKey(path))
                {
                    self.Source = Caches[path];
                    return;
                }
                if (Caches.ContainsKey(self.Path))
                {
                    self.Source = Caches[self.Path];
                    return;
                }

                Uri uri = new(self.Path);

                if (uri.Scheme == "file" && File.Exists(uri.LocalPath))
                {
                    using FileStream fileStream = new(uri.LocalPath, FileMode.Open, FileAccess.Read);
                    ImageSource bi = fileStream.ToBitmapImage();
                    self.Source = bi;
                    Caches.Add(self.Path, bi);
                    return;
                }
                else
                {
                    BitmapImage bi = new BitmapImage(new Uri(path));
                    self.Source = bi;
                    Caches.Add(path, bi);
                }
            }
            catch
            {
                self.Source = null!;
            }
        }
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CachedImage), new(new CornerRadius(0d), OnCornerRadiusChanged));

    private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Image image)
        {
            if (image.OpacityMask == null)
            {
                Border border = new()
                {
                    Background = Brushes.White,
                };
                VisualBrush brush = new()
                {
                    Visual = border,
                };
                image.OpacityMask = brush;
                image.SizeChanged += (_, _) =>
                {
                    border.Width = image.ActualWidth;
                    border.Height = image.ActualHeight;
                };
            }
            if (image.OpacityMask is VisualBrush visualBrush)
            {
                if (visualBrush.Visual is Border border)
                {
                    border.CornerRadius = (CornerRadius)e.NewValue;
                }
            }
        }
    }
}

file static class BitmapExtension
{
    public static ImageSource ToBitmapImage(this Stream stream)
    {
        BitmapImage bitmapImage = new();

        stream.Position = 0;
        bitmapImage.BeginInit();
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.StreamSource = stream;
        bitmapImage.EndInit();
        bitmapImage.Freeze();
        return bitmapImage;
    }
}
