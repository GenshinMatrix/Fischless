using System.Globalization;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(object), typeof(string))]
public sealed class ToStringConverter : SingletonConverterBase<ToStringConverter>
{
    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString();
    }
}
