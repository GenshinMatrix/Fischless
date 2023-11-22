using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Design.Controls;
using Fischless.Globalization;
using Fischless.Plugin.Abstractions;
using ModernWpf.Controls;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Windows.System;

[assembly: FischlessPlugin]
[assembly: FischlessInternalPlugin]

namespace Fischless.Plugin.LaunchHyperion;

[Export(typeof(IPlugin))]
public class DisplayDefaultPlugin : IPlugin, IPlugin2
{
    public string PluginName => MuiLanguage.Mui("LaunchHyperion");
    public string Description => MuiLanguage.Mui("LaunchHyperionHint");
    public object Icon => IconProvider.GetFontIcon(FontSymbols.AspectRatio);
    public string Author => "GenshinMatrix";
    public Version Version => new(0, 0, 1);
    public int Index => 4;
    public bool IsShowButton => true;
    public ICommand ButtonCommand => ButtonMethod.Default.GoToCommand;
}

file class IconProvider
{
    public static object GetFontIcon(string glyph)
    {
        return new FontIcon()
        {
            Glyph = glyph,
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
        // Chinese server only: https://www.miyoushe.com/ys/
        await Launcher.LaunchUriAsync(new Uri("wsa://com.mihoyo.hyperion"));
    }
}
