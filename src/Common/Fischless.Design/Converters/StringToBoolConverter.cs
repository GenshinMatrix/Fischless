using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(string), typeof(bool))]
public class StringToBoolConverter : ValueToBoolConverter<string>
{
}
