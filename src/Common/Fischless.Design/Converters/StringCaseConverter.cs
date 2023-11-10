using System.Globalization;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(string), typeof(string))]
public class StringCaseConverter : SingletonConverterBase<StringCaseConverter>
{
    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string stringValue)
        {
            var stringParameter = $"{parameter}";

            switch (stringParameter)
            {
                case "U":
                case "u":
                    return culture.TextInfo.ToUpper(stringValue);

                case "L":
                case "l":
                    return culture.TextInfo.ToLower(stringValue);

                case "T":
                case "t":
                    return culture.TextInfo.ToTitleCase(stringValue);

                default:
                    throw new ArgumentException($"Parameter '{stringParameter}' is not valid", nameof(parameter));
            }
        }

        return UnsetValue;
    }
}
