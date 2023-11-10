using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(int), typeof(bool))]
public class IntegerToBoolConverter : ValueToBoolConverter<int>
{
}
