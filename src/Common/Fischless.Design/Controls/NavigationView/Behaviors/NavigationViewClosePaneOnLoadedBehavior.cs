using Microsoft.Xaml.Behaviors;
using ModernWpf.Controls;
using System.Reflection;
using System.Windows;

namespace Fischless.Design.Controls;

public sealed class NavigationViewClosePaneOnLoadedBehavior : Behavior<NavigationView>
{
    protected override void OnAttached()
    {
        AssociatedObject.LayoutUpdated += OnLayoutUpdated;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.LayoutUpdated -= OnLayoutUpdated;
        base.OnDetaching();
    }

    private void OnPaneOpening(NavigationView sender, object args)
    {
        ClosePaneFource();
        AssociatedObject.PaneOpening -= OnPaneOpening;
    }

    public void OnLayoutUpdated(object? sender, EventArgs e)
    {
        ClosePaneFource();
        AssociatedObject.LayoutUpdated -= OnLayoutUpdated;
    }

    public void OnInitialized(object? sender, EventArgs e)
    {
        ClosePaneFource();
        AssociatedObject.Initialized -= OnInitialized;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        ClosePaneFource();
        AssociatedObject.Loaded -= OnLoaded;
    }

    private void ClosePaneFource()
    {
        AssociatedObject.IsPaneOpen = false;
        UpdateDisplayModeState(true);
        ClosePane();
    }

    private void ClosePane()
    {
        MethodInfo closePane = typeof(NavigationView).GetMethod("ClosePane", BindingFlags.Instance | BindingFlags.NonPublic);

        if (closePane != null)
        {
            Action closePaneAction = (Action)Delegate.CreateDelegate(typeof(Action), AssociatedObject, closePane);
            closePaneAction.Invoke();
        }
    }

    private bool UpdateDisplayModeState(bool useTransitions = true)
    {
        MethodInfo methodInfo = typeof(NavigationView).GetMethod("UpdateDisplayModeState", BindingFlags.NonPublic | BindingFlags.Instance);

        if (methodInfo != null)
        {
            bool result = (bool)methodInfo.Invoke(AssociatedObject, [useTransitions])!;
            return result;
        }
        return false;
    }
}
