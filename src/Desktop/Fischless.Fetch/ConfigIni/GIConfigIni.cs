using Fischless.Fetch.Regedit;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Fischless.Fetch.ConfigIni;

public static class GIConfigIni
{
    public static string FilePath => @$"{GIRegedit.InstallPath}\config.ini";
    public static Dictionary<string, string> Launcher { get; } = new();

    static GIConfigIni()
    {
        Fetch();
    }

    public static void Fetch()
    {
        List<KeyValuePair<string, string>> list = 
            new ConfigurationBuilder()
                ?.AddIniFile(FilePath)
                ?.Build()
                ?.GetSection("launcher")
                ?.AsEnumerable().ToList()!;

        if (list is not null)
        {
            foreach (var kv in list)
            {
                if (kv.Value is not null)
                {
                    Launcher.Add(kv.Key, kv.Value);
                }
            }
        }
    }

    public static void Save()
    {
        StringBuilder sb = new();

        sb.AppendLine("[launcher]");
        foreach (KeyValuePair<string, string> kv in Launcher)
        {
            sb.AppendLine($"{kv.Key}={kv.Value}");
        }
        File.WriteAllText(FilePath, sb.ToString());
    }
}
