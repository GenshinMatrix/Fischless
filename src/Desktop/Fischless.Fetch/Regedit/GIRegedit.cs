using Fischless.Threading;
using Microsoft.Win32;
using Serilog;
using System.Diagnostics;
using System.Text;

namespace Fischless.Fetch.Regedit;

public static class GIRegedit
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

    public static string InstallPathCN => GetInstallPath(GameType.CN);

    public static string ProdCN
    {
        get => GetStringFromRegedit(GIRegeditKeys.PROD_CN, GameType.CN);
        set => SetStringToRegedit(GIRegeditKeys.PROD_CN, value, GameType.CN);
    }

    public static string InstallPathOVERSEA => GetInstallPath(GameType.OVERSEA);

    public static string ProdOVERSEA
    {
        get => GetStringFromRegedit(GIRegeditKeys.PROD_OVERSEA, GameType.OVERSEA);
        set => SetStringToRegedit(GIRegeditKeys.PROD_OVERSEA, value, GameType.OVERSEA);
    }

    [Obsolete]
    public static string DataCN
    {
        get => GetStringFromRegedit(GIRegeditKeys.DATA, GameType.CN);
        set => SetStringToRegedit(GIRegeditKeys.DATA, value, GameType.CN);
    }

    [Obsolete]
    public static string DataOVERSEA
    {
        get => GetStringFromRegedit(GIRegeditKeys.DATA, GameType.OVERSEA);
        set => SetStringToRegedit(GIRegeditKeys.DATA, value, GameType.OVERSEA);
    }

    internal static string GetInstallPath(GameType type = GameType.CN)
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

            object installLocation = key.GetValue("InstallPath")!;
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

    internal static string GetStringFromRegedit(string key, GameType type = GameType.CN)
    {
#if DISPSREG
        object? value = Registry.GetValue(type.GetRegKeyName(), key, string.Empty);

        if (value is byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        return null!;
#else
        try
        {
            using MemoryStream stream = new();
            FluentProcess.Create()
                .FileName("powershell")
                .WorkingDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
                .Arguments(@$"Get-ItemPropertyValue -Path 'HKCU:\Software\miHoYo\{type.ParseGameType()}' -Name '{type.GetRegKey()}';")
                .UseShellExecute(false)
                .CreateNoWindow()
                .RedirectStandardOutput()
                .Start()
                .BeginOutputRead(stream)
                .WaitForExit();
            stream.Position = 0;
            string lines = Encoding.UTF8.GetString(stream.ToArray());
            StringBuilder sb = new();

            foreach (string line in lines.Replace("\r", string.Empty).Split('\n'))
            {
                if (byte.TryParse(line, out byte b))
                {
                    sb.Append((char)b);
                }
            }
            Log.Debug(sb.ToString());
            return sb.ToString();
        }
        catch (Exception e)
        {
            Log.Warning(e.ToString());
        }
        return null!;
#endif
    }

    internal static void SetStringToRegedit(string key, string value, GameType type = GameType.CN)
    {
#if DISPSREG
        Registry.SetValue(GetRegKeyName(type), key, Encoding.UTF8.GetBytes(value));
#else
        try
        {
            string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            string script = $"""
            $value = [Convert]::FromBase64String('{base64}');
            Set-ItemProperty -Path 'HKCU:\Software\miHoYo\{type.ParseGameType()}' -Name '{type.GetRegKey()}' -Value $value -Force;
            """;
            Process.Start(new ProcessStartInfo()
            {
                FileName = "powershell",
                Arguments = script,
                CreateNoWindow = true,
                WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            })?.WaitForExit();
        }
        catch (Exception e)
        {
            Log.Warning(e.ToString());
        }
#endif
    }

    internal static string GetRegKey(this GameType type)
    {
        return type switch
        {
            GameType.OVERSEA => GIRegeditKeys.PROD_OVERSEA,
            GameType.CNCloud => GIRegeditKeys.PROD_CNCloud,
            GameType.CN or _ => GIRegeditKeys.PROD_CN,
        };
    }

    internal static string GetRegKeyName(this GameType type)
    {
        return @"HKEY_CURRENT_USER\SOFTWARE\miHoYo\" + ParseGameType(type);
    }

    internal static string GetRegUninstallName(this GameType type)
    {
        return @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + ParseGameType(type);
    }

    internal static string ParseGameType(this GameType type)
    {
        return type switch
        {
            GameType.OVERSEA => GIRegeditKeys.OVERSEA,
            GameType.CNCloud => GIRegeditKeys.CNCloud,
            GameType.CN or _ => GIRegeditKeys.CN,
        };
    }
}

internal enum GameType
{
    CN,
    OVERSEA,
    CNCloud,
}
