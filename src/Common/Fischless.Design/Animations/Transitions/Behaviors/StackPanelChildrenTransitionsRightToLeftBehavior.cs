using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Fischless.Design.Animations;

public sealed class StackPanelChildrenTransitionsRightToLeftBehavior : Behavior<StackPanel>
{
    public bool IsOnce { get; set; } = false;
    public double BeginTimeDelay { get; set; } = 0d;

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
        if (AssociatedObject != null)
        {
            int i = default;
            foreach (UIElement child in AssociatedObject.Children)
            {
                if (child is FrameworkElement element)
                {
                    Storyboard sb = new();
                    DoubleAnimation fromHorizontalOffsetAnimaiton = new()
                    {
                        To = 0d,
                        BeginTime = TimeSpan.FromSeconds(i / 40d + BeginTimeDelay),
                        Duration = new Duration(TimeSpan.FromSeconds(0.3d)),
                        From = 15d,
                        EasingFunction = new SineEase() { EasingMode = EasingMode.EaseIn },
                    };
                    DoubleAnimation fadeInAnimation = new()
                    {
                        To = 1d,
                        BeginTime = TimeSpan.FromSeconds(i / 40d + BeginTimeDelay),
                        Duration = new Duration(TimeSpan.FromSeconds(0.4d)),
                        From = 0d,
                        EasingFunction = new SineEase() { EasingMode = EasingMode.EaseIn },
                    };
                    i++;

                    child.Opacity = 0d;
                    child.RenderTransform = new TranslateTransform() { X = 15d };
                    Storyboard.SetTarget(fromHorizontalOffsetAnimaiton, element);
                    Storyboard.SetTarget(fadeInAnimation, element);
                    Storyboard.SetTargetProperty(fromHorizontalOffsetAnimaiton, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                    Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));
                    sb.Children.Add(fromHorizontalOffsetAnimaiton);
                    sb.Children.Add(fadeInAnimation);
                    sb.Begin();
                }
            }
        }
    }
}
