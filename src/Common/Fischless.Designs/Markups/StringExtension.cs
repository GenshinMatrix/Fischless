using System;
using System.Windows.Markup;

namespace Fischless.Designs.Markups;

[MarkupExtensionReturnType(typeof(string))]
public sealed class StringExtension : MarkupExtension
{
    [ConstructorArgument("text")]
    public string Text { get; set; } = null;

    public StringExtension(string text)
    {
        Text = text;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Text;
    }
}
