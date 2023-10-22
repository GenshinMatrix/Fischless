namespace Fischless.Plugin.Abstractions;

public interface IPlugin
{
    public string PluginName { get; }
    public string Description { get; }
    public object Icon { get; }
    public string Author { get; }
    public Version Version { get; }
    public int Index { get; }
}
