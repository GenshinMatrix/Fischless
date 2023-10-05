using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.System;

namespace Fischless.Helpers;

public static partial class UrlProtocolHelper
{
    public const string ProtocolRootKey = @"HKEY_CLASSES_ROOT\";
    public const string ProtocolUserKey = @"HKEY_CURRENT_USER\" + ProtocolUserSubKey;
    public const string ProtocolUserSubKey = @"Software\Classes\";
    public const string ProtocolName = "fischless";

    public static RegistryHive RegistryHive { get; set; } = RegistryHive.CurrentUser;

    [GeneratedRegex("\"(?<exe>[\\s\\S]*?)\" \"%1\"")]
    private static partial Regex VaildPathRegex();

    public static string ProtocolKeyFull => RegistryHive switch
    {
        RegistryHive.ClassesRoot => ProtocolRootKey,
        RegistryHive.CurrentUser or _ => ProtocolUserKey,
    } + ProtocolName;

    private static RegistryKey? OpenSchemeKey()
    {
        if (RegistryHive == RegistryHive.ClassesRoot)
        {
            using RegistryKey keyRoot = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
            RegistryKey? keyScheme = keyRoot.OpenSubKey(ProtocolName);

            return keyScheme;
        }
        else
        {
            using RegistryKey keyUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            using RegistryKey keyUserSub = keyUser.OpenSubKey(ProtocolUserSubKey)!;
            RegistryKey? keyScheme = keyUserSub.OpenSubKey(ProtocolName);

            return keyScheme;
        }
    }

    private static RegistryKey CreateSchemeKey()
    {
        if (RegistryHive == RegistryHive.ClassesRoot)
        {
            using RegistryKey keyRoot = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
            RegistryKey keyScheme = keyRoot.CreateSubKey(ProtocolName);

            return keyScheme;
        }
        else
        {
            using RegistryKey keyUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            using RegistryKey keyUserSub = keyUser.OpenSubKey(ProtocolUserSubKey, true)!;
            RegistryKey keyScheme = keyUserSub.CreateSubKey(ProtocolName);

            return keyScheme;
        }
    }

    public static string GetUrl(string param = null!)
    {
        return $"{ProtocolName}://{param ?? string.Empty}";
    }

    public static bool GetPath(out string path)
    {
        if (Process.GetCurrentProcess().MainModule?.FileName is string fileName)
        {
            path = $"\"{fileName}\" \"%1\"";
            return true;
        }
        path = null!;
        return false;
    }

    public static bool IsRegistered()
    {
        using RegistryKey? keyScheme = OpenSchemeKey();

        if (keyScheme != null)
        {
            using RegistryKey? keyShell = keyScheme.OpenSubKey("shell");
            using RegistryKey? keyOpen = keyShell?.OpenSubKey("open");
            using RegistryKey? keyCommand = keyOpen?.OpenSubKey("command");

            if (keyCommand?.GetValue(string.Empty) is string pathRegistered)
            {
                if (GetPath(out string path))
                {
                    if (pathRegistered == path)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static async Task<string> GetVaildPathAsync()
    {
        using RegistryKey? keyScheme = OpenSchemeKey();

        if (keyScheme != null)
        {
            using RegistryKey? keyShell = keyScheme.OpenSubKey("shell");
            using RegistryKey? keyOpen = keyShell?.OpenSubKey("open");
            using RegistryKey? keyCommand = keyOpen?.OpenSubKey("command");

            if (keyCommand?.GetValue(string.Empty) is string pathRegistered)
            {
                Regex regex = VaildPathRegex();
                Match match = regex.Match(pathRegistered);

                if (match.Success && match.Groups["exe"]?.Value is string exe)
                {
                    if (await VerifyAssembly(exe))
                    {
                        return exe;
                    }
                }
            }
        }
        return null!;
    }

    public static async Task<bool> VerifyAssembly(string path)
    {
        FileVersionInfo fvi = await Task.Run(() =>
        {
            if (File.Exists(path))
            {
                return FileVersionInfo.GetVersionInfo(path);
            }
            return null!;
        });

        if (fvi != null)
        {
            return fvi.ProductName == AppConfig.PackName
                && fvi.OriginalFilename == $"{AppConfig.PackName}.dll"
                && fvi.CompanyName == "GenshinMatrix";
        }
        return false;
    }

    public static async Task<bool> IsVaildProtocolAsync()
    {
        return await GetVaildPathAsync() != null;
    }

    public static void Register()
    {
        using RegistryKey keyScheme = CreateSchemeKey();

        keyScheme.SetValue(string.Empty, AppConfig.PackName);
        keyScheme.SetValue("URL Protocol", string.Empty);

        if (GetPath(out string path))
        {
            using RegistryKey keyShell = keyScheme.CreateSubKey("shell");
            using RegistryKey keyOpen = keyShell.CreateSubKey("open");
            using RegistryKey keyCommand = keyOpen.CreateSubKey("command");

            keyCommand.SetValue(string.Empty, path);
        }
    }

    public static void Unregister()
    {
        {
            using RegistryKey keyRoot = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
            using RegistryKey? keyScheme = keyRoot.OpenSubKey(ProtocolName, true);

            if (keyScheme != null)
            {
                keyScheme.Close();
                keyRoot.DeleteSubKeyTree(ProtocolName);
            }
        }
        {
            using RegistryKey keyUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            using RegistryKey keyUserSub = keyUser.OpenSubKey(ProtocolUserSubKey)!;
            using RegistryKey? keyScheme = keyUserSub.OpenSubKey(ProtocolName, true);

            if (keyScheme != null)
            {
                keyScheme.Close();
                keyUserSub.DeleteSubKeyTree(ProtocolName);
            }
        }
    }

    public static void Launch(string param = null!)
    {
        _ = Process.Start(new ProcessStartInfo()
        {
            UseShellExecute = true,
            FileName = GetUrl(param),
        });
    }

    public static async Task LaunchAsync(string param = null!)
    {
        await Launcher.LaunchUriAsync(new Uri(GetUrl(param)));
    }
}
