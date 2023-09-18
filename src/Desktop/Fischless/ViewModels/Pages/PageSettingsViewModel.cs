using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.Services;

namespace Fischless.ViewModels;

public partial class PageSettingsViewModel : ObservableRecipient
{
    [ObservableProperty]
    private AboutViewModel about = new();

    [ObservableProperty]
    private bool autoStart = AppConfig.GetService<IAutoStartService>()?.IsEnabled() ?? false;
    partial void OnAutoStartChanged(bool value)
    {
        AppConfig.GetService<IAutoStartService>()?.SetEnabled(value);
    }

}
