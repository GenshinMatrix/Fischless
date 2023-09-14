using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Design.Controls;
using Fischless.Helper;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Windows.System;

namespace Fischless.ViewModels;

public partial class AboutViewModel : ObservableObject
{
    [ObservableProperty]
    private string version = $"v{Assembly.GetCallingAssembly().GetAssemblyVersion()}";

    [RelayCommand]
    public async Task CopyVersionAsync()
    {
        try
        {
            Clipboard.SetText($"Fischless {this.Version}");
            Toast.Success(Mui("CopySuccessfully"), ToastLocation.BottomCenter);
        }
        catch
        {
            Toast.Success(Mui("CopyFailed"), ToastLocation.BottomCenter);
        }
        await Task.Delay(250);
    }

    [RelayCommand]
    public async Task CheckUpdateVersionAsync()
    {
        await Launcher.LaunchUriAsync(new Uri("https://github.com/emako/Fischless/releases"));
    }

    [RelayCommand]
    public async Task ShowPrivacyPolicyAsync()
    {
        await Launcher.LaunchUriAsync(new Uri("https://github.com/emako/Fischless/blob/main/PRIVACYPOLICY.md"));
    }

    [RelayCommand]
    public async Task ShowLicense()
    {
        await Launcher.LaunchUriAsync(new Uri("https://github.com/emako/Fischless/blob/main/LICENSE"));
    }
}
