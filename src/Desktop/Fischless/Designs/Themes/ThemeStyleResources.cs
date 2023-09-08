using System;
using System.Windows;

namespace Fischless.Designs.Themes;

public sealed class ThemeStyleResources : ResourceDictionary
{
    public ThemeStyleResources()
    {
        Source = new Uri("pack://application:,,,/Fischless;component/Designs/Themes/Generic.xaml");
    }
}
