using System;
using System.IO;

namespace Fischless.Helpers;

internal static class SpecialPathHelper
{
    private readonly static string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public static string TempPath { get; } = Path.GetTempPath();

    public static string GetFolder(string optionFolder = null!)
    {
        return Path.Combine(_localApplicationData, optionFolder ?? AppConfig.PackName);
    }

    public static string GetPath(string? baseName = null)
    {
        string configPath = Path.Combine(GetFolder(), baseName ?? string.Empty);

        if (!Directory.Exists(new FileInfo(configPath).DirectoryName))
        {
            Directory.CreateDirectory(new FileInfo(configPath).DirectoryName!);
        }
        return configPath;
    }

    public static string GetTempPath(string baseName)
    {
        return Path.Combine(TempPath + AppConfig.PackName, baseName);
    }
}
