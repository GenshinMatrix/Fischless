using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fischless.Design.Controls;

public sealed class ImageHelper
{
    [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
    public static CornerRadius GetCornerRadius(DependencyObject obj)
    {
        return (CornerRadius)obj.GetValue(CornerRadiusProperty);
    }

    public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
    {
        obj.SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ImageHelper), new PropertyMetadata(new CornerRadius(0d), CornerRadiusChanged));

    private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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
            if (image.OpacityMask is VisualBrush vb)
            {
                if (vb.Visual is Border bd)
                {
                    bd.CornerRadius = (CornerRadius)e.NewValue;
                }
            }
        }
    }
}
