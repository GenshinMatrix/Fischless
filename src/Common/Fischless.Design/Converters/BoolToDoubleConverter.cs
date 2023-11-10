using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(bool), typeof(double))]
public class BoolToDoubleConverter : BoolToValueConverter<double>
{
}
