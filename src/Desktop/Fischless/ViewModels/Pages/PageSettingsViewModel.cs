using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Helpers;
using Fischless.Models;
using Fischless.Services;
using Serilog;
using System;

namespace Fischless.ViewModels;

public partial class PageSettingsViewModel : ObservableRecipient
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
    public void RestartAsElevated()
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
    private bool desktopShortcut = LnkHelper.HasShortcutOnDesktop(AppConfig.AppName);
    partial void OnDesktopShortcutChanged(bool value)
    {
        if (value)
        {
            try
            {
                LnkHelper.CreateShortcutOnDesktop(AppConfig.AppName, Environment.ProcessPath!);
                Notification.AddNotice("创建桌面快捷方式", "操作成功");
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                Notification.AddNotice("Create ShortCut error", "See detail following", e.ToString());
            }
        }
        else
        {
            LnkHelper.RemoveShortcutOnDesktop(AppConfig.AppName);
        }
    }
}
