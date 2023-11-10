using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(string), typeof(Color))]
[SuppressMessage("WpfAnalyzers.IValueConverter", "WPF0072:")]
public sealed class StringToColorConverter : SingletonConverterBase<StringToColorConverter>
{
    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string inputString)
        {
            return ColorConverter.ConvertFromString(inputString);
        }
        return value;
    }

    protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        return string.Empty;
    }
}
