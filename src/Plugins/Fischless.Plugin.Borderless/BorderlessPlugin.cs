using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Design.Controls;
using Fischless.Fetch.Launch;
using Fischless.Globalization;
using Fischless.Helpers;
using Fischless.Plugin.Abstractions;
using Microsoft.Xaml.Behaviors;
using ModernWpf.Controls;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

[assembly: FischlessPlugin]
[assembly: FischlessInternalPlugin]

namespace Fischless.Plugin.Borderless;

[Export(typeof(IPlugin))]
public class BorderlessPlugin : IPlugin, IPlugin2
{
    public string PluginName => MuiLanguage.Mui("Borderless");
    public string Description => MuiLanguage.Mui("BorderlessHint");
    public object Icon => IconProvider.GetFontIcon(FontSymbols.DeviceLaptopPic);
    public string Author => "GenshinMatrix";
    public Version Version => new(0, 0, 1);
    public int Index => 1;
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
            FontFamily = (System.Windows.Media.FontFamily)Application.Current.TryFindResource("SymbolThemeFontFamily"),
            FontSize = 16d,
        };
    }
}

internal partial class ButtonMethod : ObservableObject
{
    public static ButtonMethod Default { get; } = new();

    [RelayCommand]
    public void GoTo(object param)
    {
        if (param is Button button)
        {
            if (button.ContextMenu == null)
            {
                ContextMenu contextMenu = new()
                {
                    PlacementTarget = button,
                    PlacementRectangle = new Rect(new Point(), new Size(button.ActualWidth, button.ActualHeight)),
                    Placement = PlacementMode.Bottom,
                    StaysOpen = true,
                };
                MenuItem menuItem1 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu1"),
                    Command = EnableWindowBorderlessCommand,
                };
                MenuItem menuItem2 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu2"),
                    Command = DisableWindowBorderlessCommand,
                };
                MenuItem menuItem3 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu3"),
                    Command = EnableWindowMaximizeBoxCommand,
                };
                MenuItem menuItem4 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu4"),
                    Command = EnableWindowTopmostCommand,
                };
                MenuItem menuItem5 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu5"),
                    Command = DisableWindowTopmostCommand,
                };
                LeftContextMenuBehavior behavior = new();
                contextMenu.Items.Add(menuItem1);
                contextMenu.Items.Add(menuItem2);
                contextMenu.Items.Add(menuItem3);
                contextMenu.Items.Add(menuItem4);
                contextMenu.Items.Add(menuItem5);
                button.ContextMenu = contextMenu;
                Interaction.GetBehaviors(button).Add(behavior);
                contextMenu.IsOpen = true;
            }
        }
    }

    [RelayCommand]
    public async Task EnableWindowBorderlessAsync()
    {
        if (!await GILauncher.TryGetProcessAsync(async t =>
        {
            if (!RuntimeHelper.IsElevated)
            {
                if (MessageBoxX.Question(MuiLanguage.Mui("UACRequestRestartHint")) == MessageBoxResult.Yes)
                {
                    RuntimeHelper.RestartAsElevated();
                }
                return;
            }

            await Task.CompletedTask;

            nint hWnd = t.MainWindowHandle;
            hWnd.EnableWindowBorderless();
        }))
        {
            // NO GAME PLAYING
        }
    }

    [RelayCommand]
    public async Task DisableWindowBorderlessAsync()
    {
        if (!await GILauncher.TryGetProcessAsync(async t =>
        {
            if (!RuntimeHelper.IsElevated)
            {
                if (MessageBoxX.Question(MuiLanguage.Mui("UACRequestRestartHint")) == MessageBoxResult.Yes)
                {
                    RuntimeHelper.RestartAsElevated();
                }
                return;
            }

            await Task.CompletedTask;

            nint hWnd = t.MainWindowHandle;
            hWnd.DisableWindowBorderless();
        }))
        {
            // NO GAME PLAYING
        }
    }

    [RelayCommand]
    public async Task EnableWindowMaximizeBoxAsync()
    {
        if (!await GILauncher.TryGetProcessAsync(async t =>
        {
            if (!RuntimeHelper.IsElevated)
            {
                if (MessageBoxX.Question(MuiLanguage.Mui("UACRequestRestartHint")) == MessageBoxResult.Yes)
                {
                    RuntimeHelper.RestartAsElevated();
                }
                return;
            }

            await Task.CompletedTask;

            nint hWnd = t.MainWindowHandle;
            hWnd.EnableWindowMaximizeBox();
        }))
        {
            // NO GAME PLAYING
        }
    }

    [RelayCommand]
    public async Task EnableWindowTopmostAsync()
    {
        if (!await GILauncher.TryGetProcessAsync(async t =>
        {
            if (!RuntimeHelper.IsElevated)
            {
                if (MessageBoxX.Question(MuiLanguage.Mui("UACRequestRestartHint")) == MessageBoxResult.Yes)
                {
                    RuntimeHelper.RestartAsElevated();
                }
                return;
            }

            await Task.CompletedTask;

            nint hWnd = t.MainWindowHandle;
            hWnd.EnableWindowTopmost();
        }))
        {
            // NO GAME PLAYING
        }
    }

    [RelayCommand]
    public async Task DisableWindowTopmostAsync()
    {
        if (!await GILauncher.TryGetProcessAsync(async t =>
        {
            if (!RuntimeHelper.IsElevated)
            {
                if (MessageBoxX.Question(MuiLanguage.Mui("UACRequestRestartHint")) == MessageBoxResult.Yes)
                {
                    RuntimeHelper.RestartAsElevated();
                }
                return;
            }

            await Task.CompletedTask;

            nint hWnd = t.MainWindowHandle;
            hWnd.DisableWindowTopmost();
        }))
        {
            // NO GAME PLAYING
        }
    }
}
