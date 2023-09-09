using System.Windows;

namespace Fischless.Designs.Themes;

public sealed class ThemeStyleResources : ResourceDictionary
{
    public ThemeStyleResources()
    {
        Source = new Uri("pack://application:,,,/Fischless.Designs;component/Themes/Generic.xaml");
    }
}
