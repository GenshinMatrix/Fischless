using ModernWpf;
using System.Windows;

namespace Fischless.Design.Themes;

public sealed class ThemeResource : ResourceDictionary
{
    public ApplicationTheme? RequestedTheme { get; set; } = ApplicationTheme.Dark;

    public ThemeResource()
    {
        Source = new Uri("pack://application:,,,/Fischless.Design;component/Themes/Dark.xaml");
    }
}
