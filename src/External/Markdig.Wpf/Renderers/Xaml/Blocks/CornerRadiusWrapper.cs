using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Markdig.Renderers.Xaml.Blocks;

public static class RoundedBlockWrapper
{
    public static Block ToRounded(this Block block, double cornerRadius)
    {
        var border = new Border
        {
            Background = "#2D2D2D".ToColor().ToBrush(),
            BorderBrush = "#424242".ToColor().ToBrush(),
            BorderThickness = new Thickness(1d),
            CornerRadius = new CornerRadius(cornerRadius),
            Padding = new Thickness(6d, 8d, 6d, 8d),
        };

        var richTextBox = new RichTextBox
        {
            BorderThickness = new Thickness(0d),
            Padding = new Thickness(0d),
            Style = null!,
            BorderBrush = Brushes.Transparent,
            Background = Brushes.Transparent,
            IsReadOnly = true,
            ContextMenu = null!,
            CaretBrush = Brushes.White,
        };

        richTextBox.Document.Blocks.Clear();
        richTextBox.Document.Blocks.Add(block);
        border.Child = richTextBox;

        return new BlockUIContainer(border);
    }

    public static Inline ToRounded(this Inline inline, double cornerRadius)
    {
        var border = new Border
        {
            Background = "#212121".ToColor().ToBrush(),
            BorderBrush = "#424242".ToColor().ToBrush(),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(cornerRadius),
            Padding = new Thickness(3d, 1d, 3d, 1d),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Margin = new Thickness(0d, 3d, 0d, -6d),
        };
        border.Child = new TextBlock(inline)
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };
        return new InlineUIContainer(border);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string ToTextString(this Span span)
    {
        StringBuilder sb = new();
        foreach (Inline inline in span.Inlines)
        {
            if (inline is Run run)
            {
                sb.Append(run.Text);
            }
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Color ToColor(this string color)
    {
        return (Color)ColorConverter.ConvertFromString(color);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SolidColorBrush ToBrush(this Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze();
        return brush;
    }
}

//public class RoundedParagraph : Block
//{
//    private double cornerRadius = 10;

//    public double CornerRadius
//    {
//        get { return cornerRadius; }
//        set { cornerRadius = value; }
//    }

//    public RoundedParagraph()
//    {
//    }

//    protected override void OnRender(DrawingContext dc)
//    {
//        var backgroundBrush = new SolidColorBrush(Colors.LightGray);
//        var borderBrush = new SolidColorBrush(Colors.Gray);
//        var borderThickness = new Thickness(1);

//        var rect = new Rect(new Point(0, 0), new Size(RenderSize.Width, RenderSize.Height));
//        var borderRect = new Rect(new Point(borderThickness.Left, borderThickness.Top), new Size(RenderSize.Width - borderThickness.Left - borderThickness.Right, RenderSize.Height - borderThickness.Top - borderThickness.Bottom));

//        var cornerRadius = CornerRadius;

//        dc.DrawRoundedRectangle(backgroundBrush, new Pen(borderBrush, borderThickness.Left), borderRect, cornerRadius, cornerRadius);
//        base.OnRender(dc);
//    }
//}
