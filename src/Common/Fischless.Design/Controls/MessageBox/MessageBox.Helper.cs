﻿using ModernWpf.Controls;
using System.Windows;
using System.Windows.Media;

namespace Fischless.Design.Controls;

public partial class MessageBoxX
{
    public static MessageBoxResult Show(string messageBoxText) =>
        Show(messageBoxText, null!);

    public static MessageBoxResult Show(string messageBoxText, string caption) =>
        Show(GetActiveWindow(), messageBoxText, caption);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button) =>
        Show(GetActiveWindow(), messageBoxText, caption, button);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) =>
        Show(messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxSymbolGlyph icon) =>
        Show(messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, string icon) =>
        Show(messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, IconSource icon) =>
        Show(messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult) =>
        Show(GetActiveWindow(), messageBoxText, caption, button, icon, defaultResult);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxSymbolGlyph icon, MessageBoxResult defaultResult) =>
        Show(GetActiveWindow(), messageBoxText, caption, button, icon, defaultResult);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, string icon, MessageBoxResult defaultResult) =>
        Show(GetActiveWindow(), messageBoxText, caption, button, icon, defaultResult);

    public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, IconSource icon, MessageBoxResult defaultResult) =>
        Show(GetActiveWindow(), messageBoxText, caption, button, icon, defaultResult);

    public static MessageBoxResult Show(Window owner, string messageBoxText) =>
        Show(owner, messageBoxText, null);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption) =>
        Show(owner, messageBoxText, caption, MessageBoxButton.OK);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        Show(owner, messageBoxText, caption, button, (IconSource)null);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) =>
        Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxSymbolGlyph icon) =>
        Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, string icon) =>
        Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, IconSource icon) =>
        Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult) =>
        Show(owner, messageBoxText, caption, button, icon.ToSymbol(), defaultResult);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxSymbolGlyph icon, MessageBoxResult defaultResult) =>
        Show(owner, messageBoxText, caption, button, icon.ToGlyph(), defaultResult);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, string icon, MessageBoxResult defaultResult) =>
        Show(owner, messageBoxText, caption, button, new FontIconSource { Glyph = icon, FontSize = 30, FontFamily = Application.Current.FindResource("SymbolThemeFontFamily") as FontFamily }, defaultResult);

    public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, IconSource icon, MessageBoxResult defaultResult)
    {
        owner ??= GetActiveWindow();

        MessageBoxX window = new()
        {
            Owner = owner,
            IconSource = icon,
            MessageBoxIcon = icon.ToMessageBoxIcon(),
            Result = defaultResult,
            Content = messageBoxText,
            MessageBoxButtons = button,
            Caption = caption ?? string.Empty,
            Title = caption ?? string.Empty,
            ResizeMode = ResizeMode.NoResize,
            WindowStyle = WindowStyle.SingleBorderWindow,
            WindowStartupLocation = owner is null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner
        };

        return window.ShowDialog();
    }

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText) =>
        ShowAsync(messageBoxText, null);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText) =>
        ShowAsync(owner, messageBoxText, null);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption) =>
        ShowAsync(owner, messageBoxText, caption, MessageBoxButton.OK);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) =>
        ShowAsync(messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button, MessageBoxSymbolGlyph icon) =>
        ShowAsync(messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button, string icon) =>
        ShowAsync(messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button, IconSource icon) =>
        ShowAsync(messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(owner, messageBoxText, caption, button, (IconSource)null);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button, icon, defaultResult);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button, MessageBoxSymbolGlyph icon, MessageBoxResult defaultResult) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button, icon, defaultResult);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button, string icon, MessageBoxResult defaultResult) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button, icon, defaultResult);

    public static Task<MessageBoxResult> ShowAsync(string messageBoxText, string caption, MessageBoxButton button, IconSource icon, MessageBoxResult defaultResult) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button, icon, defaultResult);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) =>
        ShowAsync(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxSymbolGlyph icon) =>
        ShowAsync(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, string icon) =>
        ShowAsync(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, IconSource icon) =>
        ShowAsync(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult) =>
        ShowAsync(owner, messageBoxText, caption, button, icon.ToSymbol(), defaultResult);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxSymbolGlyph icon, MessageBoxResult defaultResult) =>
        ShowAsync(owner, messageBoxText, caption, button, icon.ToGlyph(), defaultResult);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, string icon, MessageBoxResult defaultResult) =>
        ShowAsync(owner, messageBoxText, caption, button, new FontIconSource { Glyph = icon, FontSize = 30, FontFamily = Application.Current.FindResource("SymbolThemeFontFamily") as FontFamily }, defaultResult);

    public static Task<MessageBoxResult> ShowAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, IconSource icon, MessageBoxResult defaultResult)
    {
        TaskCompletionSource<MessageBoxResult> taskSource = new(TaskCreationOptions.RunContinuationsAsynchronously);

        Application.Current.Dispatcher.Invoke(() =>
        {
            MessageBoxResult result = Show(owner, messageBoxText, caption, button, icon, defaultResult);
            taskSource.TrySetResult(result);
        });

        return taskSource.Task;
    }

    private static Window GetActiveWindow()
    {
        return Application.Current.Windows.Cast<Window>()
            .FirstOrDefault(window => window.IsActive && window.ShowActivated);
    }
}
