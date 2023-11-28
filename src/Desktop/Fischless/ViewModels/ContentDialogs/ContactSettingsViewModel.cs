using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Fetch.Launch;
using Fischless.Fetch.Lazy;
using Fischless.Logging;
using Fischless.Models;
using Fischless.Views;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Fischless.ViewModels;

public partial class ContactSettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isUseGamePath = Configurations.IsUseGamePath.Get();

    partial void OnIsUseGamePathChanged(bool value)
    {
        Configurations.IsUseGamePath.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private string gamePath = Configurations.GamePath.Get();

    partial void OnGamePathChanged(string value)
    {
        Configurations.GamePath.Set(value);
        ConfigurationManager.Save();
    }

    [RelayCommand]
    public void SelectGamePath()
    {
        string gamePath = GamePath;

        if (string.IsNullOrWhiteSpace(gamePath))
        {
            _ = GILauncher.TryGetGamePath(out gamePath);
        }

        string? restoreDirName = null;
        string? restoreName = null;

        if (!string.IsNullOrWhiteSpace(gamePath))
        {
            FileInfo gameFileInfo = new(gamePath);

            restoreDirName = gameFileInfo.DirectoryName;
            restoreName = gameFileInfo.Name;
        }

        OpenFileDialog dialog = new()
        {
            Title = Mui("LaunchGameSelectGamePathHint", GILauncher.FileNameCN, GILauncher.FileNameOVERSEA),
            RestoreDirectory = true,
            InitialDirectory = restoreDirName,
            FileName = restoreName,
            DefaultExt = "*.exe",
            Filter = "*.exe|*.exe",
        };

        if (dialog.ShowDialog() ?? false)
        {
            FileInfo fileInfo = new(dialog.FileName);

            if (fileInfo.Name.Equals(GILauncher.FileNameCN, StringComparison.OrdinalIgnoreCase)
             || fileInfo.Name.Equals(GILauncher.FileNameOVERSEA, StringComparison.OrdinalIgnoreCase))
            {
                GamePath = dialog.FileName;
            }
            else
            {
                MessageBoxX.Warn(Mui("LaunchGameSelectGamePathHint", GILauncher.FileNameCN, GILauncher.FileNameOVERSEA));
            }
        }
    }

    [ObservableProperty]
    private bool isUseFps = Configurations.IsUseFps.Get();

    partial void OnIsUseFpsChanged(bool value)
    {
        Configurations.IsUseFps.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private uint fps = Configurations.Fps.Get();

    partial void OnFpsChanged(uint value)
    {
        Configurations.Fps.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool isUseResolution = Configurations.IsUseResolution.Get();

    partial void OnIsUseResolutionChanged(bool value)
    {
        Configurations.IsUseResolution.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private uint resolutionWidth = Configurations.ResolutionWidth.Get();

    partial void OnResolutionWidthChanged(uint value)
    {
        Configurations.ResolutionWidth.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private uint resolutionHeight = Configurations.ResolutionHeight.Get();

    partial void OnResolutionHeightChanged(uint value)
    {
        Configurations.ResolutionHeight.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool isUseFullScreen = Configurations.IsUseFullScreen.Get();

    partial void OnIsUseFullScreenChanged(bool value)
    {
        Configurations.IsUseFullScreen.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool isUseBorderless = Configurations.IsUseBorderless.Get();

    partial void OnIsUseBorderlessChanged(bool value)
    {
        Configurations.IsUseBorderless.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool isUseGameRunningHint = Configurations.IsUseGameRunningHint.Get();

    partial void OnIsUseGameRunningHintChanged(bool value)
    {
        Configurations.IsUseGameRunningHint.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool isUseLazy = Configurations.IsUseLazy.Get();

    partial void OnIsUseLazyChanged(bool value)
    {
        Configurations.IsUseLazy.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool isInstallLazy = LazyProtocol.AnyRegistered();

    [RelayCommand]
    public static void SetLazyToken()
    {
        _ = new SetLazyTokenDialog()
        {
            Owner = Application.Current.MainWindow,
        }.ShowDialog();
    }

    [RelayCommand]
    public static async Task ShowLazyServerAsync()
    {
        try
        {
            if (await LazyRepository.SetupToken())
            {
                string file = await LazyRepository.GetFile();

                if (!string.IsNullOrEmpty(file))
                {
                    _ = new ShowLazyDialog(file)
                    {
                        Owner = Application.Current.MainWindow,
                    }.ShowDialog();
                }
                else
                {
                    Notification.AddNotice(Mui("LazySearchRecordFailedTitle"), Mui("LazySearchRecordFailedHint"));
                }
            }
            else
            {
                Notification.AddNotice(Mui("LazySetupNoTokenTitle"), Mui("LazySetupNoTokenHint"));
            }
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
    }

    [ObservableProperty]
    private bool isUseReShade = Configurations.IsUseReShade.Get();

    partial void OnIsUseReShadeChanged(bool value)
    {
        Configurations.IsUseReShade.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool isUseReShadeSlient = Configurations.IsUseReShadeSlient.Get();

    partial void OnIsUseReShadeSlientChanged(bool value)
    {
        Configurations.IsUseReShadeSlient.Set(value);
        ConfigurationManager.Save();
    }

    [ObservableProperty]
    private bool isInstalleShade = !string.IsNullOrWhiteSpace(Configurations.ReShadePath.Get());
}
