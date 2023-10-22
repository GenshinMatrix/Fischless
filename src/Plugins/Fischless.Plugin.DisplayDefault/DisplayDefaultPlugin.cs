using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Design.Controls;
using Fischless.Plugin.Abstractions;
using ModernWpf.Controls;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Windows.System;

[assembly: FischlessPlugin]
[assembly: FischlessInternalPlugin]

namespace Fischless.Plugin.Demo;

[Export(typeof(IPlugin))]
public class DisplayDefaultPlugin : IPlugin, IPlugin2
{
    public string PluginName => "DisplayDefault";
    public string Description => "DisplayDefaultHint";
    public object Icon => IconProvider.GetFontIcon(FontSymbols.DeviceLaptopPic);
    public string Author => "GenshinMatrix";
    public Version Version => new(0, 0, 1);
    public int Index => 1;
    public bool IsShowButton => true;
    public ICommand ButtonCommand => ButtonMethod.Default.GoToCommand;
}

internal class IconProvider
{
    public static object GetFontIcon(string glyph)
    {
        return new FontIcon()
        {
            Glyph = FontSymbols.DeviceLaptopPic,
            FontFamily = (FontFamily)Application.Current.TryFindResource("SymbolThemeFontFamily"),
            FontSize = 16d,
        };
    }
}

internal partial class ButtonMethod : ObservableObject
{
    public static ButtonMethod Default { get; } = new();

    [RelayCommand]
    public async Task GoToAsync()
    {
        await Launcher.LaunchUriAsync(new Uri("ms-settings:display-advancedgraphics"));
        //await Launcher.LaunchUriAsync(new Uri("ms-settings:display-advancedgraphics-default"));
    }
}
