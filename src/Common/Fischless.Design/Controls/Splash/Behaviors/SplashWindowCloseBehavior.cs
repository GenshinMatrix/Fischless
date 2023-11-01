using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace Fischless.Design.Controls;

public sealed class SplashWindowCloseBehavior : Behavior<Window>
{
    protected override void OnAttached()
    {
        AssociatedObject.Loaded += OnLoaded;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Loaded -= OnLoaded;
        base.OnDetaching();
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        Splash.CloseAsync(AssociatedObject);
        AssociatedObject.Loaded -= OnLoaded;
    }
}
