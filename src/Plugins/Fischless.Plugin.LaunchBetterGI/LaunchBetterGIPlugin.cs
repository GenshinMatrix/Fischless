using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Plugin.Abstractions;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Windows.System;

[assembly: FischlessPlugin]

namespace Fischless.Plugin.LaunchBetterGI;

[Export(typeof(IPlugin))]
public class LaunchBetterGIPlugin : IPlugin, IPlugin2
{
    public string PluginName => "BetterGI · 更好的原神";
    public string Description => "需要版本大于 v0.16.0";
    public object Icon => "🍨";
    public string Author => "蜜汁老芭";
    public Version Version => new(0, 0, 1);
    public int Index => 20;
    public bool IsShowButton => true;
    public ICommand ButtonCommand => ButtonMethod.Default.GoToCommand;
}

internal partial class ButtonMethod : ObservableObject
{
    public static ButtonMethod Default { get; } = new();

    [RelayCommand]
    public async Task GoToAsync()
    {
        await Launcher.LaunchUriAsync(new Uri("BetterGI://"));
    }
}
