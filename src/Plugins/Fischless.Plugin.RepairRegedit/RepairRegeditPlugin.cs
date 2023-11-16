using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Design.Controls;
using Fischless.Globalization;
using Fischless.Plugin.Abstractions;
using Fischless.Plugin.RepairRegedit.Views;
using ModernWpf.Controls;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

[assembly: FischlessPlugin]
[assembly: FischlessInternalPlugin]

namespace Fischless.Plugin.RepairRegedit;

[Export(typeof(IPlugin))]
public class RepairRegeditPlugin : IPlugin, IPlugin2
{
    public string PluginName => MuiLanguage.Mui("RepairRegedit");
    public string Description => MuiLanguage.Mui("RepairRegeditHint");
    public object Icon => IconProvider.GetFontIcon(FontSymbols.Rename);
    public string Author => "GenshinMatrix";
    public Version Version => new(0, 0, 1);
    public int Index => 0;
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
        await new RepairRegeditContentDialog().ShowAsync();
    }
}
