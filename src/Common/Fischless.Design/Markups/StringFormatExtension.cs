using System.Windows.Markup;

namespace Fischless.Design.Markups;

[MarkupExtensionReturnType(typeof(string))]
public sealed class StringFormatExtension : MarkupExtension
{
    [ConstructorArgument("format")]
    public string Format { get; set; } = null!;

    [ConstructorArgument("args")]
    public string[] Args { get; set; } = null!;

    [ConstructorArgument("arg0")]
    public string Arg0 { get; set; } = null!;

    [ConstructorArgument("arg1")]
    public string Arg1 { get; set; } = null!;

    [ConstructorArgument("arg2")]
    public string Arg2 { get; set; } = null!;

    public StringFormatExtension()
    {
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        Args ??= new string[]
        {
            Arg0,
            Arg1,
            Arg2,
        };
        return string.Format(Format ?? string.Empty, Args);
    }
}
