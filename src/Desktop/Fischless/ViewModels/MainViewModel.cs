using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.Services;
using Fischless.Services.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ModernWpf.Controls;

namespace Fischless.ViewModels;

[Service(ServiceLifetime.Singleton)]
public partial class MainViewModel : ObservableRecipient
{
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
