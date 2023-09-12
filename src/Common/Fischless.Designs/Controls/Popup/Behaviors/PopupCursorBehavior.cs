using Fischless.Designs.Markups;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Fischless.Designs.Controls;

public sealed class FlyoutCursorBehavior : Behavior<Popup>
{
    public Cursor Cursor { get; set; } = null!;
    public Uri CursorUri { get; set; } = null!;
    public int HotSpotX { get; set; } = default;
    public int HotSpotY { get; set; } = default;

    protected override void OnAttached()
    {
        AssociatedObject.Opened += OnOpened;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Opened -= OnOpened;
        base.OnDetaching();
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        AssociatedObject.Opened -= OnOpened;
        Cursor ??= new CursorExtension()
        {
            CursorUri = CursorUri,
            HotSpotX = HotSpotX,
            HotSpotY = HotSpotY,
        }.ProvideValue(null!) as Cursor;
        AssociatedObject.Cursor = Cursor;
        if (AssociatedObject.Child is FrameworkElement element)
        {
            element.Cursor = Cursor;
        }
    }
}
