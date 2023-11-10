using System.Windows;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(bool), typeof(Style))]
public class BoolToStyleConverter : BoolToValueConverter<Style>
{
}
