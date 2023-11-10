using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(object), typeof(bool))]
public class IsInRangeConverter : MinMaxValueToBoolConverter
{
}
