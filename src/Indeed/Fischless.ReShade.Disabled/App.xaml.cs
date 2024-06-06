using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Fischless.ReShade.Disable;

public partial class App : Application
{
    static App()
    {
        Start();
        Environment.Exit(default);
    }

    private static void Start()
    {
        try
        {
            // TODO
            if (Environment.GetCommandLineArgs().Contains("-r"))
            {
                static void GetDirectoriesRecursive(string path)
                {
                    Disable(path);

                    string[] subdirectories = Directory.GetDirectories(path);

                    foreach (string subdirectory in subdirectories)
                    {
                        GetDirectoriesRecursive(subdirectory);
                    }
                }

                string[] subdirectories = Directory.GetDirectories(Path.GetFullPath("."));

                foreach (string subdirectory in subdirectories)
                {
                    GetDirectoriesRecursive(subdirectory);
                }
            }
            else
            {
                string[] directories = Directory.GetDirectories(Path.GetFullPath("."), "*", SearchOption.TopDirectoryOnly);

                foreach (string dir in directories)
                {
                    Disable(dir);
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    public static void Enable(string folderPath)
    {
        DirectoryInfo directoryInfo = new(folderPath);

        if (directoryInfo.Exists)
        {
            string newFolderName = directoryInfo.Name.RemoveDisabledPrefix();
            string newFolderPath = Path.Combine(directoryInfo.Parent.FullName, newFolderName);

            if (newFolderPath.Equals(directoryInfo.FullName, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            directoryInfo.MoveTo(newFolderPath);
        }
    }

    public static void Disable(string folderPath)
    {
        DirectoryInfo directoryInfo = new(folderPath);

        if (directoryInfo.Exists)
        {
            if (!directoryInfo.Name.IsDisabledFolderPath())
            {
                string newFolderName = $"{ReShadeSentimentalString.DISABLED}{directoryInfo.Name}";
                string newFolderPath = Path.Combine(directoryInfo.Parent.FullName, newFolderName);

                if (newFolderPath.Equals(directoryInfo.FullName, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (Directory.Exists(newFolderPath))
                {
                    Debug.WriteLine($"[ReShadeAvatar] Can not disable '{newFolderPath}', cuz target folder already exists.");
                    return;
                }
                directoryInfo.MoveTo(newFolderPath);
            }
        }
    }
}

public sealed class ReShadeSentimentalString
{
    public const string DISABLED = "DISABLED";

    public const string EXT_INI = ".ini";

    public const string MERGED_INI = "merged.ini";
    public const string MERGED_COMMENT = "; Merged Mod:";

    public const string TextureOverridePattern = @"\[TextureOverride(.+?)\]";

    public const string LoaderD3dxIniGameExePrefix = "target = ";
}

public static class ReShadeExtension
{
    public static bool IsEnabledFolderPath(this string folderPath)
    {
        return !IsDisabledFolderPath(folderPath);
    }

    public static bool IsDisabledFolderPath(this string folderPath)
    {
        return new DirectoryInfo(folderPath).Name?.StartsWith(ReShadeSentimentalString.DISABLED, StringComparison.OrdinalIgnoreCase) ?? false;
    }

    public static string RemoveDisabledPrefix(this string fileName)
    {
        while (fileName.StartsWith(ReShadeSentimentalString.DISABLED, StringComparison.OrdinalIgnoreCase))
        {
            fileName = fileName[ReShadeSentimentalString.DISABLED.Length..];
        }
        return fileName;
    }

    public static string[] SplitTextureOverride(this string textureOverride)
    {
        return textureOverride.Split(';', ',', '|');
    }
}
