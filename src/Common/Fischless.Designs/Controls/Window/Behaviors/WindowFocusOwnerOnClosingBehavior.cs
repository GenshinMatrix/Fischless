using Fischless.Native;
using Microsoft.Xaml.Behaviors;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using Vanara.PInvoke;

namespace Fischless.Designs.Controls;

public sealed class WindowFocusOwnerOnClosingBehavior : Behavior<Window>
{
    protected override void OnAttached()
    {
        AssociatedObject.Closing += OnWindowClosing;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Closing -= OnWindowClosing;
        base.OnDetaching();
    }

    private void OnWindowClosing(object? sender, CancelEventArgs e)
    {
        if (!e.Cancel)
        {
            try
            {
                if (AssociatedObject.Owner != null)
                {
                    _ = User32.SetForegroundWindow(new WindowInteropHelper(AssociatedObject.Owner).Handle);
                    AssociatedObject.Owner.Activate();
                    AssociatedObject.Owner.Focus();
                    _ = User32.BringWindowToTop(new WindowInteropHelper(AssociatedObject.Owner).Handle);
                    AssociatedObject.Owner = null!;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Don't attache this {nameof(WindowClearOwnerOnClosingBehavior)} for {nameof(Window)} called from {nameof(Window.ShowDialog)}.", ex);
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            }
        }
    }
}
