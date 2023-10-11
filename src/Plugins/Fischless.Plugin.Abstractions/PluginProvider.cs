using Fischless.Logging;
using System.ComponentModel.Composition.Hosting;

namespace Fischless.Plugin.Abstractions;

public static class PluginProvider
{
    public const string PluginsPath = "Plugins";
    public static IEnumerable<PluginBuilder> Plugins { get; private set; } = Array.Empty<PluginBuilder>();

    public static IEnumerable<PluginBuilder> Reload()
    {
        if (!Reload(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginsPath)))
        {
            _ = Reload(AppDomain.CurrentDomain.BaseDirectory);
        }
        return Plugins;
    }

    private static bool Reload(string dir)
    {
        DirectoryInfo dirInfo = new(dir);
        if (dirInfo.Exists)
        {
            try
            {
                using DirectoryCatalog catalog = new(dirInfo.FullName, "*Plugin*.dll");
                using CompositionContainer container = new(catalog);

                Plugins = container.GetExportedValues<IPlugin>()
                    .OrderBy(x => x.Index)
                    .Select(x => new PluginBuilder(x))
                    .ToList();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
        return false;
    }
}
