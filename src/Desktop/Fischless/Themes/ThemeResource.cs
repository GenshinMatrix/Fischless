using ModernWpf;
using System;
using System.Windows;

namespace Fischless.Themes;

public sealed class ThemeResource : ResourceDictionary
{
    public ApplicationTheme? RequestedTheme { get; set; } = ApplicationTheme.Dark;

    public ThemeResource()
    {
        Source = new Uri("pack://application:,,,/Fischless;component/Themes/Dark.xaml");
    }
}
