using System.Globalization;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(object), typeof(Enumerable))]
public class ValueToEnumerableConverter : SingletonConverterBase<ValueToEnumerableConverter>
{
    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            return new object[1] { value };
        }

        return UnsetValue;
    }
}
