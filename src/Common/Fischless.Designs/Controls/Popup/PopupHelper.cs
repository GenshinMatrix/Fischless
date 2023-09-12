using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Fischless.Designs.Controls;

public static class PopupHelper
{
    public static void ApplyCursorFromRelativeSource(this Popup self, Type? ancestorType = null)
    {
        if (self.Child is FrameworkElement element)
        {
            Binding cursorBinding = new("Cursor")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor)
                {
                    AncestorType = ancestorType ?? typeof(Window),
                }
            };
            element.SetBinding(FrameworkElement.CursorProperty, cursorBinding);
        }
    }

    public static void ApplyCursorFromApplication(this Popup self)
    {
        if (self.Child is FrameworkElement element)
        {
            element.Cursor = Application.Current.MainWindow.Cursor;
        }
    }
}
