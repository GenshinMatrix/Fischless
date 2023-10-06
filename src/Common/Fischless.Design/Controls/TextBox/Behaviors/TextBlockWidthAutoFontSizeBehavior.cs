using Microsoft.Xaml.Behaviors;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fischless.Design.Controls;

public class TextBlockWidthAutoFontSizeBehavior : Behavior<TextBlock>
{
    private const double MinFontSize = 8d;
    private const double MaxFontSize = 12d;
    private const double FontSizeStep = 0.2d;

    public double Min { get; set; } = MinFontSize;
    public double Max { get; set; } = MaxFontSize;

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.SizeChanged += OnSizeChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.SizeChanged -= OnSizeChanged;
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        AutoAdjustFontSize();
    }

    private void AutoAdjustFontSize()
    {
        if (AssociatedObject == null || string.IsNullOrEmpty(AssociatedObject.Text))
        {
            return;
        }

        if (double.IsNaN(AssociatedObject.MaxWidth) || double.IsInfinity(AssociatedObject.MaxWidth))
        {
            Debug.WriteLine("[TextBlockWidthAutoFontSizeBehavior] MaxWidth is NaN.");
            return;
        }

        double fontSize = Max;
        double desiredWidth = AssociatedObject.MaxWidth - AssociatedObject.Padding.Left - AssociatedObject.Padding.Right;
        double pixelsPerDip = VisualTreeHelper.GetDpi(AssociatedObject).PixelsPerDip;
        bool shouldFix = false;

        while (GetTextBlockWidth(AssociatedObject.Text, fontSize, pixelsPerDip) > desiredWidth && fontSize > Min)
        {
            fontSize -= FontSizeStep;
        }

        while (GetTextBlockWidth(AssociatedObject.Text, fontSize, pixelsPerDip) <= desiredWidth && fontSize < Max)
        {
            fontSize += FontSizeStep;
            shouldFix = true;
        }

        if (shouldFix)
        {
            fontSize -= FontSizeStep;
        }

        AssociatedObject.FontSize = Math.Min(fontSize, Max);
    }

    private double GetTextBlockWidth(string text, double fontSize, double pixelsPerDip)
    {
        Typeface typeface = new(AssociatedObject.FontFamily, AssociatedObject.FontStyle, AssociatedObject.FontWeight, AssociatedObject.FontStretch);
        FormattedText formattedText = new(
            text,
            CultureInfo.CurrentCulture,
            AssociatedObject.FlowDirection,
            typeface,
            fontSize,
            Brushes.Black,
            pixelsPerDip);

        return formattedText.Width;
    }
}
