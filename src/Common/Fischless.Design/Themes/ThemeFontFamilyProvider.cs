using System.Windows;
using System.Windows.Media;

namespace Fischless.Design.Themes;

public static class ThemeFontFamilyProvider
{
    public const string TextThemeFontFamilyKey = "TextThemeFontFamily";

    public static void ChangeFontFamily(ThemeTextFontFamily fontFamily = default)
    {
        if (Application.Current.Resources.Contains(TextThemeFontFamilyKey))
        {
            const string uriPrefix = "pack://application:,,,/Fischless.Design;component/Assets/Fonts/";

            if (fontFamily == ThemeTextFontFamily.HarmonyOS_Sans_SC_Regular)
            {
                Application.Current.Resources[TextThemeFontFamilyKey]
                    = new FontFamily(new Uri(uriPrefix + "HarmonyOS_Sans_SC_Regular.ttf"), "HarmonyOS Sans SC");
            }
            else if (fontFamily == ThemeTextFontFamily.MiSans_Regular)
            {
                Application.Current.Resources[TextThemeFontFamilyKey]
                    = new FontFamily(new Uri(uriPrefix + "MiSans-Regular.ttf.ttf"), "MiSans");
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}

public enum ThemeTextFontFamily
{
    Default,
    HarmonyOS_Sans_SC_Regular,
    MiSans_Regular,
}
