using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(double), typeof(bool))]
public class DoubleToBoolConverter : ValueToBoolConverter<double>
{
}
