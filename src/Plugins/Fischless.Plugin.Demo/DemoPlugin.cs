using Fischless.Plugin.Abstractions;
using System.ComponentModel.Composition;

[assembly: FischlessPlugin]

namespace Fischless.Plugin.Demo;

[Export(typeof(IPlugin))]
public class DemoPlugin : IPlugin
{
    public string PluginName => "DemoPlugin";
    public string Description => "DemoDescription";
    public string Icon => "DemoIcon";
    public string Author => "GenshinMatrix";
    public Version Version => new();
    public int Index => default;
}
