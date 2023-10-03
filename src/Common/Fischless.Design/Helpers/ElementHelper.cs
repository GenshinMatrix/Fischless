using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fischless.Design.Helpers;

public static class ElementHelper
{
    public static Window? FindActivedWindow()
    {
        if (Application.Current != null)
        {
            foreach (Window win in Application.Current?.Windows)
            {
                if (win.IsActive)
                {
                    return win;
                }
            }
            return Application.Current.MainWindow;
        }
        return null;
    }

    public static FrameworkElement GetTemplateChild(this FrameworkElement d, string childName)
    {
        MethodInfo getTemplateChild = typeof(FrameworkElement).GetMethod("GetTemplateChild", BindingFlags.NonPublic | BindingFlags.Instance);

        return getTemplateChild?.Invoke(d, new object[] { childName }) as FrameworkElement;
    }

    public static FrameworkElement GetVisualChild(this FrameworkElement d, int index)
    {
        MethodInfo getVisualChild = typeof(FrameworkElement).GetMethod("GetVisualChild", BindingFlags.NonPublic | BindingFlags.Instance);

        return getVisualChild?.Invoke(d, new object[] { index }) as FrameworkElement;
    }

    public static List<T> Where<T>(this UIElementCollection elements, Func<T, bool> condition) where T : UIElement
    {
        List<T> ret = new();
        _ = condition ?? throw new ArgumentNullException(nameof(condition));

        foreach (T element in elements)
        {
            if (condition(element))
            {
                ret.Add(element);
            }
        }
        return ret;
    }

    public static T Find<T>(this UIElementCollection elements, Func<T, bool> condition) where T : UIElement
    {
        _ = condition ?? throw new ArgumentNullException(nameof(condition));

        foreach (T element in elements)
        {
            if (condition(element))
            {
                return element;
            }
        }
        return null!;
    }

    public static void ForEach<T>(this UIElementCollection elements, Action<T> action) where T : UIElement
    {
        foreach (var element in elements)
        {
            if (element is T t)
                action(t);
        }
    }

    public static void ForEachSafty<T>(this UIElementCollection elements, Action<T> action) where T : UIElement
    {
        List<T> list = new();
        foreach (var element in elements)
        {
            if (element is T t)
                list.Add(t);
        }
        foreach (T t in list)
        {
            action(t);
        }
    }

    public static bool Find<T>(this UIElementCollection elements, Func<T, bool> condition, out T element) where T : UIElement
    {
        element = elements.Find(condition);
        return element != null;
    }

    public static bool Any<T>(this UIElementCollection elements, Func<T, bool> condition) where T : UIElement
    {
        T element = elements.Find(condition);
        return element != null;
    }

    public static List<T> ToList<T>(this UIElementCollection elements) where T : UIElement
    {
        List<T> l = new();

        elements.ForEach<T>(element => l.Add(element));
        return l;
    }

    public static void Sort<T>(this UIElementCollection elements) where T : UIElement
    {
        bool sorted = false;
        var list = elements.ToList<T>();
        var hashCodeListBefore = list.ConvertAll(item => item.GetHashCode());
        list.Sort();
        var hashCodeListAfter = list.ConvertAll(item => item.GetHashCode());

        for (int i = default; i < hashCodeListBefore.Count; i++)
        {
            if (hashCodeListAfter[i] != hashCodeListBefore[i])
            {
                sorted = true;
                break;
            }
        }

        if (sorted)
        {
            elements.Clear();
            foreach (var item in list)
            {
                elements.Add(item);
            }
        }
    }

    public static T GetChildrenByType<T>(this FrameworkElement d) where T : FrameworkElement
    {
        int count = VisualTreeHelper.GetChildrenCount(d);

        for (int i = default; i < count; i++)
        {
            if (VisualTreeHelper.GetChild(d, i) is not FrameworkElement c)
            {
                continue;
            }
            if (c.GetType() == typeof(T))
            {
                return (T)c;
            }
            else if (c is Decorator decorator)
            {
                if (decorator.Child.GetType() == typeof(T))
                {
                    return (T)decorator.Child;
                }
            }
            else if (c is Panel panel)
            {
                FrameworkElement cp = panel.GetChildrenByType<T>();
                if (cp?.GetType() == typeof(T))
                {
                    return (T)cp;
                }
            }
        }
        return null;
    }

    public static Point RelativeLocation(this Visual from, Visual to)
    {
        try
        {
            GeneralTransform generalTransform = from.TransformToAncestor(to);
            Point point = generalTransform.Transform(new Point(0, 0));
            return point;
        }
        catch
        {
            return new Point(0, 0);
        }
    }
}
