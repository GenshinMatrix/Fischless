using Fischless.Design.Markups;
using Microsoft.Xaml.Behaviors;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Fischless.Design.Controls;

public sealed class ButtonFlyoutCursorBehavior : Behavior<Button>
{
    public static readonly DependencyProperty CursorProperty =
        DependencyProperty.Register(nameof(Cursor), typeof(Cursor), typeof(ButtonFlyoutCursorBehavior), new PropertyMetadata(null!));

    public Cursor Cursor
    {
        get => (Cursor)GetValue(CursorProperty);
        set => SetValue(CursorProperty, value);
    }

    public Uri CursorUri { get; set; } = null!;
    public int HotSpotX { get; set; } = default;
    public int HotSpotY { get; set; } = default;

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
        FlyoutBase fly = FlyoutService.GetFlyout(AssociatedObject);

        if (fly != null)
        {
            fly.Opened += OnOpened;
        }
    }

    private void OnOpened(object? sender, object e)
    {
        FlyoutBase fly = FlyoutService.GetFlyout(AssociatedObject);

        if (fly != null)
        {
            fly.Opened -= OnOpened;
            if (fly.GetType().GetProperty("InternalPopup", BindingFlags.NonPublic | BindingFlags.Instance) is PropertyInfo internalPopupPropertyInfo)
            {
                if (internalPopupPropertyInfo.GetValue(fly) is Popup popup)
                {
                    Cursor ??= new CursorExtension()
                    {
                        CursorUri = CursorUri,
                        HotSpotX = HotSpotX,
                        HotSpotY = HotSpotY,
                    }.ProvideValue(null!) as Cursor;
                    popup.Cursor = Cursor;
                    if (popup.Child is FrameworkElement element)
                    {
                        element.Cursor = Cursor;
                    }
                }
            }
        }
    }
}
