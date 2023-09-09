using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Fischless.Designs.Controls;

public sealed class WindowFullScreenRestorePathBehavior : Behavior<FrameworkElement>
{
    protected override void OnAttached()
    {
        AssociatedObject.Loaded += Loaded;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Loaded -= Loaded;
        base.OnDetaching();
    }

    private void Loaded(object sender, EventArgs e)
    {
        if (sender is UIElement maximizeButtonContent && Window.GetWindow(maximizeButtonContent) is WindowX window)
        {
            window.SizeChanged += (s, e) =>
            {
                if (maximizeButtonContent is TextBlock path)
                {
                    if (window.IsFullScreen)
                    {
                        path.Text = "\xe73f";
                    }
                    else
                    {
                        path.Text = "\xe740";
                    }
                }
            };
        }
    }
}
