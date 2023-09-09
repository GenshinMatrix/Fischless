using System.Windows.Markup;

namespace Fischless.Designs.Markups;

[MarkupExtensionReturnType(typeof(double))]
public sealed class DoubleExtension : MarkupExtension
{
    public double Value { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}
