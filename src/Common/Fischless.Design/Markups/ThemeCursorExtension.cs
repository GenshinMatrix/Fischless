using Fischless.Design.Themes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Fischless.Design.Markups;

[MarkupExtensionReturnType(typeof(Cursor))]
public class ThemeCursorExtension : CursorExtension
{
    public ThemeCursorExtension()
    {
        CursorUri = ThemeCursorProvider.CursorUri;
        HotSpotX = ThemeCursorProvider.HotSpotX;
        HotSpotY = ThemeCursorProvider.HotSpotY;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (ThemeCursorProvider.CursorUri != null)
        {
            if (Application.Current.Resources.Contains(ThemeCursorProvider.ThemeCursorKey))
            {
                if (Application.Current.Resources[ThemeCursorProvider.ThemeCursorKey] != null)
                {
                    return Application.Current.Resources[ThemeCursorProvider.ThemeCursorKey];
                }
            }
            return base.ProvideValue(serviceProvider) as Cursor;
        }
        return null!;
    }
}
