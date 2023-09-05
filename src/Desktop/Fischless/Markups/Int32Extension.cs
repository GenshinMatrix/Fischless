using System;
using System.Windows.Markup;

namespace Fischless.Markups;

[MarkupExtensionReturnType(typeof(int))]
public sealed class Int32Extension : MarkupExtension
{
    public int Value { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}
