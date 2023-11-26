using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Design.Themes;
using Fischless.Fetch.Muter;
using Fischless.Helpers;
using Fischless.Logging;
using Fischless.Models;
using Fischless.Models.Message;
using Fischless.Services;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Windows.System;

namespace Fischless.ViewModels;

public partial class PageSettingsViewModel : ObservableRecipient, IDisposable
{
    [ObservableProperty]
    private AboutViewModel about = new();

    [ObservableProperty]
    private bool ensureElevated = Configurations.EnsureElevated.Get();

    partial void OnEnsureElevatedChanged(bool value)
    {
        Configurations.EnsureElevated.Set(value);
        ConfigurationManager.Save();
    }

    [RelayCommand]
    public static void RestartAsElevated()
    {
        RuntimeHelper.EnsureElevated();
    }

    [ObservableProperty]
    private bool autoStart = AppConfig.GetService<IAutoStartService>()?.IsEnabled() ?? false;

    partial void OnAutoStartChanged(bool value)
    {
        AppConfig.GetService<IAutoStartService>()?.SetEnabled(value);
    }

    [ObservableProperty]
    private bool desktopShortcut = ShortcutHelper.HasShortcutOnDesktop(AppConfig.PackName);

    partial void OnDesktopShortcutChanged(bool value)
    {
        if (value)
        {
            try
            {
                ShortcutHelper.CreateShortcutOnDesktop(AppConfig.PackName, Environment.ProcessPath!);
                Notification.AddNotice(Mui("CreateDesktopShortcut"), Mui("OperationSuccessfully"));
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                Notification.AddNotice(Mui("CreationFailed"), Mui("Detail"), e.ToString());
            }
        }
        else
        {
            ShortcutHelper.RemoveShortcutOnDesktop(AppConfig.PackName);
        }
    }

    [ObservableProperty]
    private bool closeToTray = Configurations.CloseToTray.Get();

    partial void OnCloseToTrayChanged(bool value)
    {
        Configurations.CloseToTray.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool autoMute = Configurations.AutoMute.Get();

    partial void OnAutoMuteChanged(bool value)
    {
        MuteManager.AutoMute = value;
        Configurations.AutoMute.Set(value);
        ConfigurationManager.Save();
        WeakReferenceMessenger.Default.Send(new AutoMuteChangedMessage());
    }

    [SuppressMessage("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "MVVMTK0034:")]
    private void OnAutoMuteChangedReceived()
    {
        autoMute = Configurations.AutoMute;
        OnPropertyChanged(nameof(AutoMute));
    }

    [ObservableProperty]
    private bool isUseThemeCursor = Configurations.IsUseThemeCursor.Get();

    partial void OnIsUseThemeCursorChanged(bool value)
    {
        Configurations.IsUseThemeCursor.Set(value);
        ConfigurationManager.Save();

        if (Configurations.IsUseThemeCursor)
        {
            ThemeCursorProvider.ChangeCursor(new Uri("pack://application:,,,/Fischless;component/Assets/Images/UI_Img_Cursor_PC.png"));
        }
        else
        {
            ThemeCursorProvider.ChangeCursor(null!);
        }
    }

    [ObservableProperty]
    private int themeTextFontFamily = Configurations.ThemeTextFontFamily.Get();

    partial void OnThemeTextFontFamilyChanged(int value)
    {
        Configurations.ThemeTextFontFamily.Set(value);
        ConfigurationManager.Save();

        ThemeFontFamilyProvider.ChangeFontFamily((ThemeTextFontFamily)value);
    }

    [ObservableProperty]
    private bool isUseSmallerSize = Configurations.IsUseSmallerSize.Get();

    partial void OnIsUseSmallerSizeChanged(bool value)
    {
        Configurations.IsUseSmallerSize.Set(value);
        ConfigurationManager.Save();
    }

    public PageSettingsViewModel()
    {
        WeakReferenceMessenger.Default.Register<AutoMuteChangedMessage>(this, (_, _) => OnAutoMuteChangedReceived());
    }

    public void Dispose()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    [RelayCommand]
    public static async Task LaunchWindowsSettingsAppsVolume()
    {
        _ = await Launcher.LaunchUriAsync(new Uri("ms-settings:apps-volume"));
    }

    [RelayCommand]
    public static async Task OpenSpecialFolder()
    {
        _ = await Launcher.LaunchUriAsync(new Uri($"file://{SpecialPathHelper.GetPath()}/"));
    }

    [RelayCommand]
    public static async Task OpenLogsFolder()
    {
        _ = await Launcher.LaunchUriAsync(new Uri($"file://{AppConfig.LogFolder}/"));
    }
}
