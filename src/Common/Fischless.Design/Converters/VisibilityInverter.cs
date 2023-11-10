using System.Windows;
using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(Visibility), typeof(string))]
public class VisibilityInverter : BoolToValueConverter<Visibility>
{
    public VisibilityInverter()
    {
        this.TrueValue = Visibility.Visible;
        this.FalseValue = Visibility.Collapsed;
        this.IsInverted = true;
    }
}
