using System.Windows.Data;

namespace Fischless.Design.Converters;

[ValueConversion(typeof(bool), typeof(bool))]
public class BoolInverter : BoolNegationConverter
{
}
