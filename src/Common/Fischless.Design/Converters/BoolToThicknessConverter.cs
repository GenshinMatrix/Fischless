using System.Windows;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(bool), typeof(Thickness))]
public class BoolToThicknessConverter : BoolToValueConverter<Thickness>
{
}
