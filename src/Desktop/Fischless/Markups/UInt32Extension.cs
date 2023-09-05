using System;
using System.Windows.Markup;

namespace Fischless.Markups;

[MarkupExtensionReturnType(typeof(uint))]
public sealed class UInt32Extension : MarkupExtension
{
    public uint Value { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}
