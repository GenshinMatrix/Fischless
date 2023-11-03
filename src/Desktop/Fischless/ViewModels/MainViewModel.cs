using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Globalization;
using Fischless.Helpers;
using Fischless.Models;
using Fischless.Services;
using Fischless.Services.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ModernWpf.Controls;
using System.Windows;

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
