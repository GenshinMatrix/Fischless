﻿using ModernWpf.Controls;

namespace Fischless.Design.Controls;

public static class MessageBoxIconExtensions
{
    public static MessageBoxIcon ToMessageBoxIcon(this IconSource icon)
    {
        MessageBoxIcon boxIcon = MessageBoxIcon.None;

        if (icon is FontIconSource ficon)
        {
            if (ficon.Glyph == MessageBoxSymbolGlyph.Error.ToGlyph())
            {
                boxIcon = MessageBoxIcon.Error;
            }
            else if (ficon.Glyph == MessageBoxSymbolGlyph.Info.ToGlyph())
            {
                boxIcon = MessageBoxIcon.Info;
            }
            else if (ficon.Glyph == MessageBoxSymbolGlyph.Warning.ToGlyph())
            {
                boxIcon = MessageBoxIcon.Warning;
            }
            else if (ficon.Glyph == MessageBoxSymbolGlyph.Question.ToGlyph())
            {
                boxIcon = MessageBoxIcon.Question;
            }
            else if (ficon.Glyph == MessageBoxSymbolGlyph.None.ToGlyph())
            {
                boxIcon = MessageBoxIcon.None;
            }
        }
        return boxIcon;
    }
}
