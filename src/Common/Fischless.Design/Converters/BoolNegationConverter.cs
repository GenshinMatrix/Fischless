using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(bool), typeof(bool))]
public class BoolNegationConverter : BoolToValueConverter<bool>
{
    public BoolNegationConverter()
    {
        this.TrueValue = true;
        this.FalseValue = false;
        this.IsInverted = true;
    }
}
