using Fischless.Design.Markups;
using System.Windows;
using System.Windows.Input;

namespace Fischless.Design.Themes;

/// <summary>
/// Not supported Animated Cursors or Muilti Cursors
/// </summary>
public class ThemeCursorProvider
{
    public const string ThemeCursorKey = "ThemeCursor";

    public static Uri CursorUri { get; set; } = null!;
    public static int HotSpotX { get; set; } = default;
    public static int HotSpotY { get; set; } = default;

    public static void ChangeCursor(Uri cursorUri = null!, int hotSpotX = default, int hotSpotY = default)
    {
        CursorUri = cursorUri;
        HotSpotX = hotSpotX;
        HotSpotY = hotSpotY;

        if (CursorUri != null)
        {
            Application.Current.Resources[ThemeCursorKey] = new ThemeCursorExtension()
            {
                CursorUri = CursorUri,
                HotSpotX = HotSpotX,
                HotSpotY = HotSpotY
            }.ProvideValue(null!);
        }
        else
        {
            Application.Current.Resources[ThemeCursorKey] = null!;
        }
    }
}
