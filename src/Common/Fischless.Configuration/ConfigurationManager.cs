namespace Fischless.Configuration;

public class ConfigurationManager
{
    public static event Action? Reloaded = null!;

    public static string FilePath { get; private set; } = null!;
    public static ConfigurationCache Cache { get; private set; } = Init();

    public static void Setup(string filePath)
    {
        FilePath = filePath;
        Cache = Init();
        Save();
    }

    public static ConfigurationCache Init()
    {
        ConfigurationCache instance = null!;

        if (File.Exists(FilePath))
        {
            instance = Load();
        }

        instance ??= new();
        return instance;
    }

    public static void Reinit()
    {
        Cache = Init();
        Reloaded?.Invoke();
    }

    public static ConfigurationCache Load()
    {
        return LoadFrom(FilePath);
    }

    public static ConfigurationCache LoadFrom(string filename)
    {
        try
        {
            return ConfigurationSerializer.DeserializeFile<ConfigurationCache>(filename) ?? new();
        }
        catch (Exception e)
        {
            _ = e;
            return new();
        }
    }

    public static bool Save()
    {
        return SaveAs(FilePath);
    }

    public static bool SaveAs(string filename, bool overwrite = true)
    {
        if (!overwrite && File.Exists(filename))
        {
            return true;
        }
        return ConfigurationSerializer.SerializeFile(FilePath, Cache);
    }

    public static bool SaveAs(string filename, object obj, bool overwrite = true)
    {
        if (!overwrite && File.Exists(filename))
        {
            return true;
        }
        return ConfigurationSerializer.SerializeFile(filename, obj);
    }
}
