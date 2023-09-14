using System.Windows;

namespace Fischless.Design.Themes;

public sealed class ThemeStyleResources : ResourceDictionary
{
    public ThemeStyleResources()
    {
        Source = new Uri("pack://application:,,,/Fischless.Design;component/Themes/Generic.xaml");
    }
}
