using ModernWpf.Controls;
using System;

namespace Fischless.Services;

public interface INavigationService
{
    public void Navigate(Type navigateTo, object? extraData = null);
    public void Navigate(NavigationViewItemInvokedEventArgs e);
}
