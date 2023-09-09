using Microsoft.Xaml.Behaviors;
using ModernWpf.Controls;
using System.ComponentModel;

namespace Fischless.Designs.Controls;

[Description("For disable GoToBack")]
public sealed class FrameClearCommandBindingsBehavior : Behavior<Frame>
{
    protected override void OnAttached()
    {
        AssociatedObject.CommandBindings.Clear();
        base.OnAttached();
    }
}
