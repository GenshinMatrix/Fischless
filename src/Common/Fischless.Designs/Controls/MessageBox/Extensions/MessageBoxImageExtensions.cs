using System;
using System.Windows;

namespace Fischless.Designs.Controls;

public static class MessageBoxImageExtensions
{
    public static string ToGlyph(this MessageBoxSymbolGlyph symbol) =>
        char.ConvertFromUtf32((int)symbol);

    public static MessageBoxSymbolGlyph ToSymbol(this MessageBoxImage image)
    {
        return image switch
        {
            MessageBoxImage.Error => MessageBoxSymbolGlyph.Error,
            MessageBoxImage.Information => MessageBoxSymbolGlyph.Info,
            MessageBoxImage.Warning => MessageBoxSymbolGlyph.Warning,
            MessageBoxImage.Question => MessageBoxSymbolGlyph.StatusCircleQuestionMark,
            MessageBoxImage.None => (MessageBoxSymbolGlyph)0x2007,
            _ => throw new NotSupportedException(),
        };
    }
}
