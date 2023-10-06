using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fischless.Design.Controls;

public class CachedImage : Image
{
    static CachedImage()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CachedImage), new FrameworkPropertyMetadata(typeof(CachedImage)));
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
