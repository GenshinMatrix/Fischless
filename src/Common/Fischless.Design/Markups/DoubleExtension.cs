using System.Windows.Markup;

namespace Fischless.Design.Markups;

[MarkupExtensionReturnType(typeof(double))]
public sealed class DoubleExtension : MarkupExtension
{
    public double Value { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}
