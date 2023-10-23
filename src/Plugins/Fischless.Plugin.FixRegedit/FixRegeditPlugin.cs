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
public class FixRegeditPlugin : IPlugin, IPlugin2
{
    public string PluginName => "FixRegedit";
    public string Description => "FixRegeditHint";
    public object Icon => IconProvider.GetFontIcon(FontSymbols.Read);
    public string Author => "GenshinMatrix";
    public Version Version => new(0, 0, 1);
    public int Index => 0;
    public bool IsShowButton => true;
    public ICommand ButtonCommand => ButtonMethod.Default.GoToCommand;
}

internal class IconProvider
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
        await Launcher.LaunchUriAsync(new Uri("ms-settings:display-advancedgraphics"));
    }
}
