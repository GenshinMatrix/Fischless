using Fischless.Logging;
using Fischless.Threading;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;

namespace Fischless.Fetch.Regedit;

public  static partial class GIRegedit
{
    public static string ProdCN
    {
        get => GetStringFromRegedit(GIRegeditKeys.PROD_CN, GIGameType.CN);
        set => SetStringToRegedit(GIRegeditKeys.PROD_CN, value, GIGameType.CN);
    }

    public static string ProdOVERSEA
    {
        get => GetStringFromRegedit(GIRegeditKeys.PROD_OVERSEA, GIGameType.OVERSEA);
        set => SetStringToRegedit(GIRegeditKeys.PROD_OVERSEA, value, GIGameType.OVERSEA);
    }

    [Obsolete("Unusable")]
    public static string DataCN
    {
        get => GetStringFromRegedit(GIRegeditKeys.DATA, GIGameType.CN);
        set => SetStringToRegedit(GIRegeditKeys.DATA, value, GIGameType.CN);
    }

    [Obsolete("Unusable")]
    public static string DataOVERSEA
    {
        get => GetStringFromRegedit(GIRegeditKeys.DATA, GIGameType.OVERSEA);
        set => SetStringToRegedit(GIRegeditKeys.DATA, value, GIGameType.OVERSEA);
    }

    public static string GetStringFromRegedit(string key, GIGameType type = GIGameType.CN)
    {
        if (RuntimeHelper.IsElevated)
        {
            return GetStringFromRegeditDirect(key, type);
        }

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
    }

    public static string GetStringFromRegeditDirect(string key, GIGameType type = GIGameType.CN)
    {
        object? value = Registry.GetValue(type.GetRegKeyName(), key, string.Empty);

        if (value is byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        return null!;
    }

    public static void SetStringToRegedit(string key, string value, GIGameType type = GIGameType.CN)
    {
        if (RuntimeHelper.IsElevated)
        {
            SetStringToRegeditDirect(key, value, type);
            return;
        }

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
    }

    public static void SetStringToRegeditDirect(string key, string value, GIGameType type = GIGameType.CN)
    {
        Registry.SetValue(GetRegKeyName(type), key, Encoding.UTF8.GetBytes(value));
    }

    public static string GetRegKey(this GIGameType type)
    {
        return type switch
        {
            GIGameType.OVERSEA => GIRegeditKeys.PROD_OVERSEA,
            GIGameType.CNCloud => GIRegeditKeys.PROD_CNCloud,
            GIGameType.CN or _ => GIRegeditKeys.PROD_CN,
        };
    }

    public static string GetRegKeyName(this GIGameType type)
    {
        return @$"{GIRegeditKeys.HKEY_CURRENT_USER}\SOFTWARE\miHoYo\" + ParseGameType(type);
    }

    public static string ParseGameType(this GIGameType type)
    {
        return type switch
        {
            GIGameType.OVERSEA => GIRegeditKeys.OVERSEA,
            GIGameType.CNCloud => GIRegeditKeys.CNCloud,
            GIGameType.CN or _ => GIRegeditKeys.CN,
        };
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
