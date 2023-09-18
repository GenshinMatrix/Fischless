using Fischless.Design.Controls;
using IWshRuntimeLibrary;
using Lnk;
using Serilog;
using System;
using System.IO;
using File = System.IO.File;

namespace Fischless.Services;

public class AutoStartProgramDataService : IAutoStartService
{
    public static string? StartupFolder => Environment.GetEnvironmentVariable("windir") + @"\..\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\";

    public AutoStartProgramDataService()
    {
    }

    public string? GetAppName() => AppConfig.AppName;
    public string? GetLaunchCommand() => AppConfig.AutoStartCommand;

    public void Enable()
    {
        try
        {
            if (Directory.Exists(StartupFolder))
            {
                ShortcutCreator.CreateShortcut(StartupFolder, GetAppName(), Environment.ProcessPath!, GetLaunchCommand());
            }
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
            Notification.AddNotice("Create Startup ShortCut error", "See detail following", e.ToString());
        }
    }

    public bool IsEnabled()
    {
        try
        {
            if (Directory.Exists(StartupFolder))
            {
                string lnk = StartupFolder + GetAppName() + ".lnk";
                if (File.Exists(lnk))
                {
                    byte[] raw = File.ReadAllBytes(lnk);
                    if (raw[0] == 0x4c)
                    {
                        LnkFile lnkObj = new(raw, lnk);

                        if (lnkObj.LocalPath == Environment.ProcessPath!)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
        return false;
    }

    public void Disable()
    {
        try
        {
            string lnk = StartupFolder + GetAppName() + ".lnk";

            if (File.Exists(lnk))
            {
                File.Delete(lnk);
            }
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
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

file static class ShortcutCreator
{
    public static void CreateShortcut(string directory, string shortcutName, string targetPath, string arguments = null!, string description = null!, string iconLocation = null!)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string shortcutPath = Path.Combine(directory, $"{shortcutName}.lnk");
        WshShell shell = new();
        IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
        shortcut.TargetPath = targetPath;
        shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
        shortcut.WindowStyle = 1;
        shortcut.Arguments = arguments;
        shortcut.Description = description;
        shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;
        shortcut.Save();
    }

    public static void CreateShortcutOnDesktop(string shortcutName, string targetPath, string arguments = null!, string description = null!, string iconLocation = null!)
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        CreateShortcut(desktop, shortcutName, targetPath, arguments, description, iconLocation);
    }
}
