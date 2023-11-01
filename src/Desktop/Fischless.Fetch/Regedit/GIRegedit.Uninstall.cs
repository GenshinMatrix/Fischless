using Microsoft.Win32;
using System.Security.Principal;
using System.Windows.Forms;
using static Fischless.Fetch.Regedit.GIRegeditUninstallInfo;

namespace Fischless.Fetch.Regedit;

public static partial class GIRegedit
{
    public static string InstallPath
    {
        get
        {
            var installPath = InstallPathCN;

            if (string.IsNullOrEmpty(installPath))
            {
                installPath = InstallPathOVERSEA;
            }
            return installPath;
        }
    }

    public static string InstallPathOVERSEA => GetInstallPath(GIGameType.OVERSEA);

    public static string InstallPathCN => GetInstallPath(GIGameType.CN);

    public static string GetInstallPath(GIGameType type = GIGameType.CN)
    {
        try
        {
            using RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey? key = hklm.OpenSubKey(type.GetRegUninstallName());

            if (key == null)
            {
                key = hklm.OpenSubKey(type.GetRegUninstallName());

                if (key == null)
                {
                    return null!;
                }
            }

            object? installLocation = key.GetValue(SKEY_INSTALL_PATH);
            key?.Dispose();

            if (installLocation != null && !string.IsNullOrEmpty(installLocation.ToString()))
            {
                return installLocation.ToString()!;
            }
        }
        catch
        {
            throw;
        }
        return null!;
    }

    public static GIRegeditUninstallInfo GetUninstallInfo(GIGameType type)
    {
        try
        {
            GIRegeditUninstallInfo info = new();
            using RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey? key = hklm.OpenSubKey(type.GetRegUninstallName());

            if (key == null)
            {
                return null!;
            }

            info.RegistryKey = key.ToString();

            using (key)
            {
                info.DisplayIcon = key.GetValue(SKEY_DISPLAY_ICON)?.ToString();
                info.DisplayName = key.GetValue(SKEY_DISPLAY_NAME)?.ToString();
                info.DisplayVersion = key.GetValue(SKEY_DISPLAY_VERSION)?.ToString();
                info.EstimatedSize = key.GetValue(SKEY_ESTIMATED_SIZE);
                info.ExeName = key.GetValue(SKEY_EXE_NAME)?.ToString();
                info.InstallPath = key.GetValue(SKEY_INSTALL_PATH)?.ToString();
                info.NetworkType = key.GetValue(SKEY_NETWORK_TYPE)?.ToString();
                info.Publisher = key.GetValue(SKEY_PUBLISHER)?.ToString();
                info.UninstallString = key.GetValue(SKEY_UNINSTALL_STRING)?.ToString();
                info.URLInfoAbout = key.GetValue(SKEY_URL_INFO_ABOUT)?.ToString();
                info.UUID = key.GetValue(SKEY_UUID)?.ToString();
            }
            return info;
        }
        catch
        {
            throw;
        }
    }

    public static void SetUninstallInfo(GIRegeditUninstallInfo info)
    {
        if (!RuntimeHelper.IsElevated)
        {
#if false
            string tempPath = Path.GetTempPath();
            string tempFile = Path.Combine(tempPath, "Fischless-RepairRegedit.reg");

            try
            {
                File.WriteAllText(tempFile, info.GetRegFileText(), Encoding.Unicode);

                FluentProcess.Create()
                    .FileName("regedit.exe")
                    .WorkingDirectory(tempPath)
                    .Arguments($"/s \"{tempFile}\"")
                    .Verb("runas")
                    .UseShellExecute(false)
                    .CreateNoWindow()
                    .Start()
                    .WaitForExit();
            }
            finally
            {
                File.Delete(tempFile);
            }
#elif false
            string key = info.RegistryKey.Replace(GIRegeditKeys.HKEY_LOCAL_MACHINE, "HKLM:");
            string script = $@"""
            if (-not (Test-Path -Path '{key}')) {{
                New-Item -Path '{key}' -Force;
            }}
            Set-ItemProperty -Path '{key}' -Name '{SKEY_DISPLAY_ICON}' -Value '{info.DisplayIcon}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_DISPLAY_NAME}' -Value '{info.DisplayName}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_DISPLAY_VERSION}' -Value '{info.DisplayVersion}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_ESTIMATED_SIZE}' -Value {info.EstimatedSize} -Type DWORD -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_EXE_NAME}' -Value '{info.ExeName}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_INSTALL_PATH}' -Value '{info.InstallPath}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_NETWORK_TYPE}' -Value '{info.NetworkType}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_PUBLISHER}' -Value '{info.Publisher}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_UNINSTALL_STRING}' -Value '{info.UninstallString}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_URL_INFO_ABOUT}' -Value '{info.URLInfoAbout}' -Force;
            Set-ItemProperty -Path '{key}' -Name '{SKEY_UUID}' -Value '{info.UUID}' -Force;
            """;
            Process.Start(new ProcessStartInfo()
            {
                FileName = "powershell",
                Arguments = script,
                Verb = "runas",
                CreateNoWindow = true,
                WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            })?.WaitForExit();

#endif
            return;
        }

        try
        {
            using RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            using RegistryKey? key = hklm.CreateSubKey(info.RegistryKey.Substring(hklm.ToString().Length + 1), true);

            key.SetValue(SKEY_DISPLAY_ICON, info.DisplayIcon, RegistryValueKind.String);
            key.SetValue(SKEY_DISPLAY_NAME, info.DisplayName, RegistryValueKind.String);
            key.SetValue(SKEY_DISPLAY_VERSION, info.DisplayVersion, RegistryValueKind.String);
            key.SetValue(SKEY_ESTIMATED_SIZE, info.EstimatedSize, RegistryValueKind.DWord);
            key.SetValue(SKEY_EXE_NAME, info.ExeName, RegistryValueKind.String);
            key.SetValue(SKEY_INSTALL_PATH, info.InstallPath, RegistryValueKind.String);
            key.SetValue(SKEY_NETWORK_TYPE, info.NetworkType, RegistryValueKind.String);
            key.SetValue(SKEY_PUBLISHER, info.Publisher, RegistryValueKind.String);
            key.SetValue(SKEY_UNINSTALL_STRING, info.UninstallString, RegistryValueKind.String);
            key.SetValue(SKEY_URL_INFO_ABOUT, info.URLInfoAbout, RegistryValueKind.String);
            key.SetValue(SKEY_UUID, info.UUID, RegistryValueKind.String);
        }
        catch
        {
            throw;
        }
    }

    public static string GetRegUninstallName(this GIGameType type)
    {
        return @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + type.ParseGameType();
    }

    public static string GetRegUninstallKey(this GIGameType type)
    {
        return $@"{GIRegeditKeys.HKEY_LOCAL_MACHINE}\{type.GetRegUninstallName()}";
    }

    public static string GetRegFileText(this GIRegeditUninstallInfo info)
    {
        return
            $"""
            Windows Registry Editor Version 5.00

            [{info.RegistryKey}]
            "UUID"="{info.UUID}"
            "DisplayIcon"="{info.DisplayIcon.Replace("\\", "\\\\")}"
            "DisplayName"="{info.DisplayName}"
            "DisplayVersion"="{info.DisplayVersion}"
            "Publisher"="{info.Publisher}"
            "UninstallString"="{info.UninstallString.Replace("\\", "\\\\")}"
            "InstallPath"="{info.InstallPath.Replace("\\", "\\\\")}"
            "ExeName"="{info.ExeName}"
            "URLInfoAbout"="{info.URLInfoAbout}"
            "EstimatedSize"=dword:{info.EstimatedSize}
            "NetworkType"="{info.NetworkType}"

            """;
    }

    public static GIRegeditUninstallInfo Repair(this GIRegeditUninstallInfo info)
    {
        string lang = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;

        info.InstallPath ??= @"C:\Program Files\Genshin Impact";
        info.DisplayIcon = Path.Combine(info.InstallPath, "launcher.exe");
        info.DisplayName ??= (lang == "zh" ? "原神" : "Genshin Impact");
        info.DisplayVersion ??= new Version().ToString();
        info.EstimatedSize ??= default(int);
        info.ExeName ??= "launcher.exe";
        info.NetworkType ??= "QT";
        info.Publisher ??= "miHoYo Co.,Ltd";
        info.UninstallString = Path.Combine(info.InstallPath, "uninstall.exe");
        info.URLInfoAbout ??= (lang == "zh" ? "https://ys.mihoyo.com/main/" : "https://genshin.hoyoverse.com/");
        info.UUID ??= ('0' * 32).ToString();

        return info;
    }
}

file static class RuntimeHelper
{
    public static bool IsElevated { get; } = GetElevated();

    private static bool GetElevated()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}
