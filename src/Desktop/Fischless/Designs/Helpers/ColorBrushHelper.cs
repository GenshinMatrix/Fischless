using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Fischless.Designs.Helpers;

public static class ColorBrushHelper
{
    public static Brush GetBrush(string color)
    {
        BrushConverter converter = new();
        return (converter.ConvertFromString(color) as Brush)!;
    }

    public static Color GetColor(string color)
    {
        return (Color)ColorConverter.ConvertFromString(color);
    }

    public static Brush GetLinearGradientBrush(string color1, string color2, int ori = default)
    {
        LinearGradientBrush brush = ori switch
        {
            2 => new()
            {
                StartPoint = new(0, 0),
                EndPoint = new(0, 1),
            },
            1 or _ => new()
            {
                StartPoint = new(0, 0),
                EndPoint = new(1, 1),
            },
        };
        brush.GradientStops.Add(new GradientStop() { Offset = 0, Color = GetColor(color1) });
        brush.GradientStops.Add(new GradientStop() { Offset = 1, Color = GetColor(color2) });
        return brush;
    }

    public static Brush GetRadialGradientBrush(string color1, string color2)
    {
        RadialGradientBrush brush = new()
        {
            GradientOrigin = new(),
        };
        brush.GradientStops.Add(new GradientStop() { Offset = 0, Color = GetColor(color1) });
        brush.GradientStops.Add(new GradientStop() { Offset = 1, Color = GetColor(color2) });
        return brush;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SolidColorBrush ToBrush(this Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze();
        return brush;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SolidColorBrush ToBrush(this Color? color)
    {
        if (color == null)
            return new SolidColorBrush(Colors.Transparent);
        return new SolidColorBrush((Color)color);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ToColor(this string color)
    {
        return (Color)ColorConverter.ConvertFromString(color);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ToColor(this SolidColorBrush brush)
    {
        if (brush == null)
            return Colors.Transparent;
        return brush.Color;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ToColor(this Brush brush)
    {
        if (brush == null)
            return Colors.Transparent;
        if (brush is SolidColorBrush)
            return (brush as SolidColorBrush).Color;
        else if (brush is LinearGradientBrush)
            return (brush as LinearGradientBrush).GradientStops[0].Color;
        else if (brush is RadialGradientBrush)
            return (brush as RadialGradientBrush).GradientStops[0].Color;
        else
            return Colors.Transparent;
    }
}
