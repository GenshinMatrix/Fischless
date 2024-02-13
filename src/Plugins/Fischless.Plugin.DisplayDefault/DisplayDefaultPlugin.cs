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

namespace Fischless.Plugin.DisplayDefault;

[Export(typeof(IPlugin))]
public class DisplayDefaultPlugin : IPlugin, IPlugin2
{
    public string PluginName => MuiLanguage.Mui("DisplayDefault");
    public string Description => MuiLanguage.Mui("DisplayDefaultHint");
    public object Icon => IconProvider.GetFontIcon(FontSymbols.DeviceLaptopPic);
    public string Author => "GenshinMatrix";
    public Version Version => new(0, 0, 1);
    public int Index => 3;
    public bool IsShowButton => true;
    public ICommand ButtonCommand => ButtonMethod.Instance.Value.GoToCommand;
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
    public static Lazy<ButtonMethod> Instance { get; } = new();

    [RelayCommand]
    public async Task GoToAsync()
    {
        // ms-settings:display
        // ms-settings:display-advancedgraphics
        // ms-settings:display-advancedgraphics-default
        await Launcher.LaunchUriAsync(new Uri("ms-settings:display-advancedgraphics"));
    }
}
