using Fischless.Globalization.Helpers;
using Fischless.Logging;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;

namespace Fischless.Globalization;

public static class MuiLanguage
{
    public static bool IsResourceReady { get; private set; } = false;
    public static string MuiLanguageName { get; private set; } = string.Empty;

    public static string DetectLanguage() => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName switch
    {
        "zh" => "zh",
        "ja" => "ja",
        "en" or _ => "en",
    };

    public static void SetupLanguage(string lang)
    {
        if (!string.IsNullOrWhiteSpace(lang))
        {
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
        }
        _ = SetLanguage();
    }

    public static bool SetLanguage() => SetLanguage(DetectLanguage());

    public static bool SetLanguage(string name = "en")
    {
        MuiLanguageName = name;

        if (!string.IsNullOrWhiteSpace(name))
        {
            CultureInfo cultureInfo = new(name);

            if (cultureInfo.NumberFormat.NumberDecimalSeparator != ".")
            {
                Log.Information($"[MuiLanguageManager] Protect number decimal seprator from diff culture '{cultureInfo.TwoLetterISOLanguageName}', so set culture to 'en'.");
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
        }

        if (Application.Current == null)
        {
            return false;
        }

        try
        {
            foreach (ResourceDictionary dictionaryOuter in Application.Current.Resources.MergedDictionaries)
            {
                if (dictionaryOuter is MuiLanguageResources reses)
                {
                    foreach (ResourceDictionary dictionaryInner in reses.MergedDictionaries)
                    {
                        if (dictionaryInner.Source != null && dictionaryInner.Source.OriginalString.EndsWith($"/Assets/Langs/{name}.xaml", StringComparison.Ordinal))
                        {
                            reses.MergedDictionaries.Remove(dictionaryInner);
                            reses.MergedDictionaries.Add(dictionaryInner);
                            IsResourceReady = true;
                            return true;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            _ = e;
        }
        return false;
    }

    public static string Mui(string key)
    {
        try
        {
            if (Application.Current == null || !IsResourceReady)
            {
                return MuiBaml(key);
            }
            if (Application.Current!.FindResource(key) is string value)
            {
                return value;
            }
        }
        catch (Exception e)
        {
            _ = e;
        }
        return null!;
    }

    public static string Mui(string key, params object[] args)
    {
        return string.Format(Mui(key)?.ToString(), args);
    }

    private static string MuiBaml(string key)
    {
        try
        {
            using Stream resourceXaml = ResourceHelper.GetStream(GetXamlUriString());
            if (BamlHelper.LoadBaml(resourceXaml) is ResourceDictionary resourceDictionary)
            {
                return (resourceDictionary[key] as string)!;
            }
        }
        catch (Exception e)
        {
            _ = e;
        }
        return null!;
    }

    private static string GetXamlUriString()
    {
        static string GetUriString(string name) => $"pack://application:,,,/Fischless.Globalization;component/Assets/Langs/{name}.xaml";

        if (ResourceHelper.HasResource(GetUriString(CultureInfo.CurrentUICulture.Name)))
        {
            return GetUriString(CultureInfo.CurrentUICulture.Name);
        }
        else
        {
            if (ResourceHelper.HasResource(GetUriString(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)))
            {
                return GetUriString(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            }
            else
            {
                if (ResourceHelper.HasResource(GetUriString(CultureInfo.CurrentUICulture.ThreeLetterISOLanguageName)))
                {
                    return GetUriString(CultureInfo.CurrentUICulture.ThreeLetterISOLanguageName);
                }
            }
        }

        Log.Debug($"[MuiLanguageService] NotFound with match mui lang name of '{CultureInfo.CurrentUICulture.Name}' or '{CultureInfo.CurrentUICulture.TwoLetterISOLanguageName}' or '{CultureInfo.CurrentUICulture.ThreeLetterISOLanguageName}'.");
        return GetUriString("en");
    }
}

public enum LanguageIndex
{
    [Description("zh")]
    Chinese,

    [Description("ja")]
    Japanese,

    [Description("en")]
    English,
}
