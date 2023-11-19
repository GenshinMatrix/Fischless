using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Configuration;
using Fischless.Globalization;
using Fischless.Helpers;
using Fischless.Models;
using Fischless.Services;
using Fischless.Services.Attributes;
using Fischless.Threading;
using Fischless.Views;
using Microsoft.Extensions.DependencyInjection;
using ModernWpf.Controls;
using System;
using System.Threading.Tasks;
using Windows.System;

namespace Fischless.ViewModels;

[Service(ServiceLifetime.Singleton)]
public partial class MainViewModel : ObservableRecipient
{
    public string Language
    {
        get => string.IsNullOrWhiteSpace(Configurations.Language.Get()) ? MuiLanguage.MuiLanguageName : Configurations.Language.Get();
        set
        {
            if (string.IsNullOrEmpty(value)) return;
            MuiLanguage.SetLanguage(value);
            Configurations.Language.Set(value);
            ConfigurationManager.Save();
        }
    }

    [RelayCommand]
    public void ViewSettings()
    {
        AppConfig.GetService<INavigationService>()?.Navigate(typeof(PageSettings));
    }

    [RelayCommand]
    public async Task OpenUrlInteractiveMapAsync()
    {
        if (MuiLanguageName?.StartsWith("zh") ?? false)
        {
            await Launcher.LaunchUriAsync(new Uri("https://webstatic.mihoyo.com/ys/app/interactive-map"));
        }
        else if (MuiLanguageName?.StartsWith("ja") ?? false)
        {
            await Launcher.LaunchUriAsync(new Uri("https://act.hoyolab.com/ys/app/interactive-map/index.html?lang=ja"));
        }
        else
        {
            await Launcher.LaunchUriAsync(new Uri("https://act.hoyolab.com/ys/app/interactive-map/index.html?lang=en"));
        }
    }

    [RelayCommand]
    public async Task OpenUrlPlayBoxAsync()
    {
        await Launcher.LaunchUriAsync(new Uri("https://www.aplaybox.com/u/680828836"));
    }

    [RelayCommand]
    public async Task OpenUrlGameBananaAsync()
    {
        await Launcher.LaunchUriAsync(new Uri("https://gamebanana.com/games/8552"));
    }

    [RelayCommand]
    public static async Task OpenSpecialFolderAsync()
    {
        _ = await Launcher.LaunchUriAsync(new Uri($"file://{SpecialPathHelper.GetPath()}/"));
    }

    [RelayCommand]
    public static async Task OpenLogsFolderAsync()
    {
        _ = await Launcher.LaunchUriAsync(new Uri($"file://{AppConfig.LogFolder}/"));
    }

    [RelayCommand]
    public static async Task OpenUserManualAsync()
    {
        _ = await Launcher.LaunchUriAsync(new Uri($"{AppConfig.Website}/wiki"));
    }

    [RelayCommand]
    public static async Task ViewAboutAsync()
    {
        await new AboutContentDialog().ShowAsync();
    }

    [RelayCommand]
    public void ShowEventViewer()
    {
        FluentProcess.Create()
            .FileName("cmd.exe")
            .Arguments("/c eventvwr.msc")
            .CreateNoWindow()
            .Start()
            .Forget();
    }

    [RelayCommand]
    public void ShowVersionViewer()
    {
        FluentProcess.Create()
            .FileName("rundll32.exe")
            .Arguments("SHELL32.DLL,ShellAbout")
            .WorkingDirectory(Environment.CurrentDirectory)
            .Start()
            .Forget();
    }

    public void OnNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs e)
    {
        AppConfig.GetService<INavigationService>()?.Navigate(e);
    }

    public void OnNavViewPaneOpening(NavigationView sender, object args)
    {
    }

    public void OnNavViewPaneOpened(NavigationView sender, object args)
    {
    }
}
