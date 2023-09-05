using System;
using System.Windows;

namespace Fischless.Themes;

public sealed class ThemeStyleResources : ResourceDictionary
{
    public ThemeStyleResources()
    {
        Source = new Uri("pack://application:,,,/Fischless;component/Themes/Generic.xaml");
    }
}
