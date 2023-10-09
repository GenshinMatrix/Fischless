namespace Fischless.Fetch.Lazy;

internal class LazySpecialPathProvider
{
    public static string TempPath { get; } = Path.GetTempPath();

    public static string GetPath(string baseName)
    {
        return GetPathInternal(baseName);
    }

    internal static string GetPathInternal(string baseName)
    {
        string appUserPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string configPath = Path.Combine(Path.Combine(appUserPath, "genshin-lazy"), baseName);

        if (!Directory.Exists(new FileInfo(configPath).DirectoryName))
        {
            Directory.CreateDirectory(new FileInfo(configPath).DirectoryName!);
        }
        return configPath;
    }
}
