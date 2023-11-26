using Fischless.Fetch.Regedit;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Fischless.Fetch.ConfigIni;

public class GIConfigIni
{
    public Dictionary<string, string> Launcher { get; } = [];

    public GIConfigIni()
    {
        Fetch();
    }

    public void Fetch(string? installPath = null)
    {
        List<KeyValuePair<string, string>> list =
            new ConfigurationBuilder()
                ?.AddIniFile(@$"{installPath ?? GIRegedit.InstallPath}\config.ini")
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

    public void Save(string? installPath = null)
    {
        StringBuilder sb = new();

        sb.AppendLine("[launcher]");
        foreach (KeyValuePair<string, string> kv in Launcher)
        {
            sb.AppendLine($"{kv.Key}={kv.Value}");
        }
        File.WriteAllText(@$"{installPath ?? GIRegedit.InstallPath}\config.ini", sb.ToString());
    }
}
