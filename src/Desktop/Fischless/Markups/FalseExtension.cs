using System;
using System.Windows.Markup;

namespace Fischless.Markups;

[MarkupExtensionReturnType(typeof(bool))]
public sealed class FalseExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return false;
    }
}
