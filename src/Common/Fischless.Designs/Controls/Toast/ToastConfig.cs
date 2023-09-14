using System.Windows;
using System.Windows.Media;

namespace Fischless.Designs.Controls;

public class ToastConfig
{
    public const int FastTime = 1500;
    public const int NormalTime = 2000;
    public const int SlowTime = 3000;

    public double Height { get; set; } = 35d;
    public int Time { get; set; } = NormalTime;
    public MessageBoxIcon MessageBoxIcon { get; set; } = MessageBoxIcon.None;
    public ToastLocation Location { get; set; } = ToastLocation.TopCenter;
    public Brush Foreground { get; set; } = Brushes.White;
    public FontStyle FontStyle { get; set; } = SystemFonts.MessageFontStyle;
    public FontStretch FontStretch { get; set; } = FontStretches.Normal;
    public double FontSize { get; set; } = SystemFonts.MessageFontSize;
    public FontWeight FontWeight { get; set; } = SystemFonts.MenuFontWeight;
    public double IconSize { get; set; } = 16d;
    public CornerRadius CornerRadius { get; set; } = new CornerRadius(3d);
    public Brush BorderBrush { get; set; } = (Brush)new BrushConverter().ConvertFromString("#1B1B1B");
    public Thickness BorderThickness { get; set; } = new Thickness(1.5d);
    public Brush Background { get; set; } = (Brush)new BrushConverter().ConvertFromString("#2B2B2B");
    public HorizontalAlignment HorizontalContentAlignment { get; set; } = HorizontalAlignment.Left;
    public VerticalAlignment VerticalContentAlignment { get; set; } = VerticalAlignment.Center;
    public Thickness OffsetMargin { get; set; } = new Thickness(15d);

    public ToastConfig()
    {
    }

    public ToastConfig(MessageBoxIcon icon, ToastLocation location, Thickness offsetMargin, int time) : this()
    {
        MessageBoxIcon = icon;
        Location = location;
        if (offsetMargin != default)
        {
            OffsetMargin = offsetMargin;
        }
        Time = time;
    }
}

public enum ToastLocation
{
    Center,
    Left,
    Right,
    TopLeft,
    TopCenter,
    TopRight,
    BottomLeft,
    BottomCenter,
    BottomRight,
}
