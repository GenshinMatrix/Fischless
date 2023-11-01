using Fischless.Design.Controls;
using Fischless.Helpers;
using Fischless.Logging;
using Lnk;
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

    public string? GetAppName() => AppConfig.PackName;

    public string? GetLaunchCommand() => AppConfig.AutoStartCommand;

    public void Enable()
    {
        try
        {
            if (Directory.Exists(StartupFolder))
            {
                LnkHelper.CreateShortcut(StartupFolder, GetAppName(), Environment.ProcessPath!, GetLaunchCommand());
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
