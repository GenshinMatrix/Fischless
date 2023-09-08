using ModernWpf;
using System;
using System.Windows;

namespace Fischless.Designs.Themes;

public sealed class ThemeResource : ResourceDictionary
{
    public ApplicationTheme? RequestedTheme { get; set; } = ApplicationTheme.Dark;

    public ThemeResource()
    {
        Source = new Uri("pack://application:,,,/Fischless;component/Designs/Themes/Dark.xaml");
    }
}
