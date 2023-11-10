using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(bool), typeof(string))]
public class BoolToStringConverter : BoolToValueConverter<string>
{
}
