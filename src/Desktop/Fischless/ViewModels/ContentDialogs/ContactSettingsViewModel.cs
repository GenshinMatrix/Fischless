using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Fetch.Launch;
using Fischless.Models;
using Microsoft.Win32;
using System;
using System.IO;

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

        FileInfo gameFileInfo = new(gamePath);

        OpenFileDialog dialog = new()
        {
            Title = $"选择 {GILauncher.FileNameCN} 或 {GILauncher.FileNameOVERSEA}",
            RestoreDirectory = true,
            InitialDirectory = gameFileInfo.DirectoryName,
            FileName = gameFileInfo.Name,
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
                MessageBoxX.Warn($"选择 {GILauncher.FileNameCN} 或 {GILauncher.FileNameOVERSEA}");
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
    private bool isUseLazy = Configurations.IsUseLazy.Get();
    partial void OnIsUseLazyChanged(bool value)
    {
        Configurations.IsUseLazy.Set(value);
        ConfigurationManager.Save();
    }
}
