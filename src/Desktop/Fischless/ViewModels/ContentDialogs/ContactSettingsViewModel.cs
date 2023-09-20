using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.Configuration;
using Fischless.Models;

namespace Fischless.ViewModels;

public partial class ContactSettingsViewModel : ObservableObject
{
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
