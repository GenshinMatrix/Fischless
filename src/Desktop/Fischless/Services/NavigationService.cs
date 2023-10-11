using Fischless.Design.Controls;
using Fischless.Logging;
using Fischless.Services.Attributes;
using Fischless.Views;
using Microsoft.Extensions.DependencyInjection;
using ModernWpf;
using ModernWpf.Controls;
using System;
using System.Linq;
using System.Reflection;

namespace Fischless.Services;

public sealed class NavigationService : INavigationService
{
    public static NavigationView NavigationView { get; set; } = null!;
    public static Frame Frame { get; private set; } = null!;
    public Type NavigateFrom { get; set; } = typeof(PageHome);

    public void Navigate(Type navigateTo, object? extraData = null)
    {
        if (NavigationView is null)
        {
            return;
        }

        bool result = false;

        try
        {
            if (NavigateFrom == navigateTo)
            {
                return;
            }

            if (NavigationView.SelectedItem is NavigationViewItem item)
            {
                Type navigateFrom = NavigationHelper.GetNavigateTo(item);

                if (navigateFrom != null && navigateTo == navigateFrom)
                {
                    return;
                }
            }

            SyncTabWith(navigateTo);
            Frame ??= NavigationView.FindDescendant<Frame>();
            object page = AppConfig.GetService(navigateTo);

            if (page != null)
            {
                if (navigateTo?.GetCustomAttribute<ServiceAttribute>()?.Lifetime == ServiceLifetime.Singleton)
                {
                    GC.KeepAlive(page);
                }
            }

            result = Frame?.Navigate(page, extraData) ?? false;

            if (result)
            {
                NavigateFrom = navigateTo;
            }
            Frame?.RemoveBackEntry();
        }
        catch
        {
        }
        Log.Information($"Navigate to {navigateTo}:{(result ? "succeed" : "failed")}");
    }

    public void Navigate(NavigationViewItemInvokedEventArgs e)
    {
        if (NavigationView is null)
        {
            return;
        }

        if (NavigationView.SelectedItem is NavigationViewItem selected)
        {
            Type? navigateTo = e.IsSettingsInvoked ? typeof(PageSettings) : NavigationHelper.GetNavigateTo(selected);
            object? extraData = NavigationHelper.GetExtraData(selected);
            bool result = false;

            try
            {
                if (NavigateFrom == navigateTo)
                {
                    return;
                }

                Frame frame = NavigationView.FindDescendant<Frame>();
                object page = AppConfig.GetService(navigateTo);

                if (page != null)
                {
                    if (navigateTo?.GetCustomAttribute<ServiceAttribute>()?.Lifetime == ServiceLifetime.Singleton)
                    {
                        GC.KeepAlive(page);
                    }
                }

                result = frame?.Navigate(page, extraData) ?? false;

                if (result)
                {
                    NavigateFrom = navigateTo;
                }
                frame?.RemoveBackEntry();
            }
            catch
            {
            }
            Log.Information($"Navigate to {navigateTo}:{(result ? "succeed" : "failed")}");
        }
    }

    private bool SyncTabWith(Type pageType)
    {
        if (NavigationView is null)
        {
            return false;
        }
        
        NavigationViewItem? target = NavigationView.MenuItems
            .OfType<NavigationViewItem>()
            .FirstOrDefault(menuItem => NavigationHelper.GetNavigateTo(menuItem) == pageType)
        ?? NavigationView.FooterMenuItems
            .OfType<NavigationViewItem>()
            .FirstOrDefault(menuItem => NavigationHelper.GetNavigateTo(menuItem) == pageType);

        NavigationView.SelectedItem = target;
        return true;
    }
}
