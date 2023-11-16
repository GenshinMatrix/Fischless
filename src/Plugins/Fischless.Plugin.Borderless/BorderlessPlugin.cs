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
using Vanara.PInvoke;

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

    public static void MakeWindowBorderless(nint hWnd)
    {
        nint dwStyle = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE);

        dwStyle = dwStyle.ToInt32() & ~(int)(User32.WindowStyles.WS_BORDER | User32.WindowStyles.WS_DLGFRAME | User32.WindowStyles.WS_CAPTION | User32.WindowStyles.WS_SYSMENU | User32.WindowStyles.WS_MINIMIZEBOX | User32.WindowStyles.WS_MAXIMIZEBOX);

        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE, dwStyle);

        _ = User32.SetWindowPos(hWnd, 0, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE | User32.SetWindowPosFlags.SWP_FRAMECHANGED);
    }

    public static void EnableWindowBorder(nint hWnd)
    {
        nint dwStyle = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE);

        dwStyle = dwStyle.ToInt32() | (int)(User32.WindowStyles.WS_BORDER | User32.WindowStyles.WS_DLGFRAME | User32.WindowStyles.WS_CAPTION | User32.WindowStyles.WS_SYSMENU | User32.WindowStyles.WS_MINIMIZEBOX | User32.WindowStyles.WS_MAXIMIZEBOX);

        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE, dwStyle);

        _ = User32.SetWindowPos(hWnd, 0, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE | User32.SetWindowPosFlags.SWP_FRAMECHANGED);
    }

    public static void EnableWindowMaximizeBox(nint hWnd)
    {
        nint dwStyle = User32.GetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE);

        dwStyle = dwStyle.ToInt32() | (int)User32.WindowStyles.WS_MAXIMIZEBOX;

        _ = User32.SetWindowLong(hWnd, User32.WindowLongFlags.GWL_STYLE, dwStyle);
    }

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
                    Command = MakeWindowBorderlessCommand,
                };
                MenuItem menuItem2 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu2"),
                    Command = EnableWindowBorderCommand,
                };
                MenuItem menuItem3 = new()
                {
                    Header = MuiLanguage.Mui("BorderlessMenu3"),
                    Command = EnableWindowMaximizeBoxCommand,
                };
                LeftContextMenuBehavior behavior = new();
                contextMenu.Items.Add(menuItem1);
                contextMenu.Items.Add(menuItem2);
                contextMenu.Items.Add(menuItem3);
                button.ContextMenu = contextMenu;
                Interaction.GetBehaviors(button).Add(behavior);
                contextMenu.IsOpen = true;
            }
        }
    }

    [RelayCommand]
    public async Task MakeWindowBorderlessAsync()
    {
        if (!await GILauncher.TryGetProcessAsync(async t =>
        {
            if (!RuntimeHelper.IsElevated)
            {
                if (MessageBoxX.Question("需要管理员权限，是否以管理员权限重启？") == MessageBoxResult.Yes)
                {
                    RuntimeHelper.RestartAsElevated();
                }
                return;
            }

            await Task.CompletedTask;

            nint hWnd = t.MainWindowHandle;
            MakeWindowBorderless(hWnd);
        }))
        {
            // NO GAME PLAYING
        }
    }

    [RelayCommand]
    public async Task EnableWindowBorderAsync()
    {
        if (!await GILauncher.TryGetProcessAsync(async t =>
        {
            if (!RuntimeHelper.IsElevated)
            {
                if (MessageBoxX.Question("需要管理员权限，是否以管理员权限重启？") == MessageBoxResult.Yes)
                {
                    RuntimeHelper.RestartAsElevated();
                }
                return;
            }

            await Task.CompletedTask;

            nint hWnd = t.MainWindowHandle;
            EnableWindowBorder(hWnd);
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
                if (MessageBoxX.Question("需要管理员权限，是否以管理员权限重启？") == MessageBoxResult.Yes)
                {
                    RuntimeHelper.RestartAsElevated();
                }
                return;
            }

            await Task.CompletedTask;

            nint hWnd = t.MainWindowHandle;
            EnableWindowMaximizeBox(hWnd);
        }))
        {
            // NO GAME PLAYING
        }
    }
}
