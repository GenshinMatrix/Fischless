using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fischless.Designs.Controls;

#pragma warning disable IDE0017

public sealed class ListViewMouseWheelBubblingBehavior : Behavior<ListView>
{
    protected override void OnAttached()
    {
        AssociatedObject.PreviewMouseWheel += OnPreviewMouseWheel;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PreviewMouseWheel -= OnPreviewMouseWheel;
        base.OnDetaching();
    }

    private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (!e.Handled)
        {
            e.Handled = true;

            MouseWheelEventArgs eventArg = new(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((Control)sender).Parent as UIElement;
            parent.RaiseEvent(eventArg);
        }
    }
}
