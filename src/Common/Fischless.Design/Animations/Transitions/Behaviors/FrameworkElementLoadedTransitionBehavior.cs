using Microsoft.Xaml.Behaviors;
using System.Windows;
using Wpf.Ui.Animations;

namespace Fischless.Design.Animations;

public sealed class FrameworkElementLoadedTransitionBehavior : Behavior<FrameworkElement>
{
    public Transition Transition { get; set; } = Transition.FadeIn;
    public int Duration { get; set; } = 100;
    public bool IsOnce { get; set; } = true;

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += AssociatedObject_Loaded;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.Loaded -= AssociatedObject_Loaded;
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        if (IsOnce)
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
        TransitionAnimationProvider.ApplyTransition(sender, Transition, Duration);
    }
}
