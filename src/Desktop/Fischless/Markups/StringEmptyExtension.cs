using System;
using System.Windows.Markup;

namespace Fischless.Markups;

[MarkupExtensionReturnType(typeof(string))]
public class StringEmptyExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return string.Empty;
    }
}
