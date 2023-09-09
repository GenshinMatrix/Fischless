using ModernWpf;
using System.Windows;

namespace Fischless.Designs.Themes;

public sealed class ThemeResource : ResourceDictionary
{
    public ApplicationTheme? RequestedTheme { get; set; } = ApplicationTheme.Dark;

    public ThemeResource()
    {
        Source = new Uri("pack://application:,,,/Fischless.Designs;component/Themes/Dark.xaml");
    }
}
