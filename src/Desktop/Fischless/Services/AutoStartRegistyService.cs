using Microsoft.Win32;
using System;

namespace Fischless.Services;

public class AutoStartRegistyService : IAutoStartService
{
    private const string RunLocation = @"Software\Microsoft\Windows\CurrentVersion\Run";

    private readonly string keyName;
    private readonly string launchCommand;

    public AutoStartRegistyService()
    {
        keyName = GetAppName();
        launchCommand = $"\"{Environment.ProcessPath!}\" {GetLaunchCommand()}";
    }

    public string? GetAppName() => AppConfig.PackName;

    public string? GetLaunchCommand() => AppConfig.AutoStartCommand;

    public void Enable()
    {
        using RegistryKey key = Registry.CurrentUser.CreateSubKey(RunLocation);
        key?.SetValue(keyName, launchCommand);
    }

    public bool IsEnabled()
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(RunLocation);

        if (key == null)
        {
            return false;
        }

        string? value = (string?)key.GetValue(keyName);

        if (value == null)
        {
            return false;
        }

        return value == launchCommand;
    }

    public void Disable()
    {
        using RegistryKey key = Registry.CurrentUser.CreateSubKey(RunLocation);

        if (key == null)
        {
            return;
        }

        if (key.GetValue(keyName) != null)
        {
            key.DeleteValue(keyName);
        }
    }

    public void SetEnabled(bool enable)
    {
        if (enable)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }
}
