using Fischless.Native;
using Microsoft.Win32;

namespace Fischless.Controls;

public class ApplicationThemeManager
{
    public static ApplicationTheme GetAppTheme()
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        object? registryValueObject = key?.GetValue("AppsUseLightTheme");

        if (registryValueObject == null)
        {
            return ApplicationTheme.Light;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0 ? ApplicationTheme.Light : ApplicationTheme.Dark;
    }
}
