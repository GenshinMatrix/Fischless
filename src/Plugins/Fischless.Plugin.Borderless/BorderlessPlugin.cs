using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Fetch.DragMove;
using Fischless.Fetch.Launch;
using Fischless.Globalization;
using Fischless.Helpers;
using Fischless.Plugin.Abstractions;
using Microsoft.Xaml.Behaviors;
using ModernWpf.Controls;
using System.ComponentModel.Composition;
using System.Reflection;
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
    static BorderlessPlugin()
    {
        if (Configurations.IsUseBorderlessPlugin.Get())
        {
            DragMoveProvider.IsEnabled = true;
        }
    }

    public string PluginName => MuiLanguage.Mui("Borderless");
    public string Description => MuiLanguage.Mui("BorderlessHint");
    public object Icon => IconProvider.GetFontIcon(FontSymbols.QuickNote);
    public string Author => "GenshinMatrix";
    public Version Version => new(0, 0, 2);
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

                MenuItem menuItem0 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu0"),
                    IsCheckable = true,
                    IsChecked = Configurations.IsUseBorderlessPlugin.Get(),
                };
                menuItem0.Checked += (object sender, RoutedEventArgs e) =>
                {
                    DragMoveProvider.IsEnabled = true;
                    Configurations.IsUseBorderlessPlugin.Set(DragMoveProvider.IsEnabled);
                    ConfigurationManager.Save();
                };
                menuItem0.Unchecked += (object sender, RoutedEventArgs e) =>
                {
                    DragMoveProvider.IsEnabled = false;
                    Configurations.IsUseBorderlessPlugin.Set(DragMoveProvider.IsEnabled);
                    ConfigurationManager.Save();
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
                MenuItem menuItem6 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu6"),
                    Command = RestoreWindowPositonCommand,
                };
                MenuItem menuItem7 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu7"),
                    Command = EnableWindowDragMoveCommand,
                };
                MenuItem menuItem8 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu8"),
                    Command = DisableWindowDragMoveCommand,
                };
                LeftContextMenuBehavior behavior = new();
                contextMenu.Items.Add(menuItem0);
                contextMenu.Items.Add(new Separator());
                contextMenu.Items.Add(menuItem1);
                contextMenu.Items.Add(menuItem2);
                contextMenu.Items.Add(menuItem3);
                contextMenu.Items.Add(menuItem4);
                contextMenu.Items.Add(menuItem5);
                contextMenu.Items.Add(menuItem6);
                contextMenu.Items.Add(menuItem7);
                contextMenu.Items.Add(menuItem8);
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

    [RelayCommand]
    public async Task RestoreWindowPositonAsync()
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
            hWnd.RestoreWindowPositon();
        }))
        {
            // NO GAME PLAYING
        }
    }

    [RelayCommand]
    public void EnableWindowDragMove()
    {
        DragMoveProvider.IsEnabled = true;
    }

    [RelayCommand]
    public void DisableWindowDragMove()
    {
        DragMoveProvider.IsEnabled = false;
    }
}

[Obfuscation]
file static class Configurations
{
    public static ConfigurationDefinition<bool> IsUseBorderlessPlugin { get; } = new(nameof(IsUseBorderlessPlugin), false);
}
