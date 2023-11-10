using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(bool), typeof(object))]
public class BoolToObjectConverter : BoolToValueConverter<object>
{
}
