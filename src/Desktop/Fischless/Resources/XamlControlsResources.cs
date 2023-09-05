using System;
using System.Windows;

namespace Fischless.Resources;

public sealed class XamlControlsResources : ResourceDictionary
{
    public XamlControlsResources()
    {
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless;component/Resources/Converters.xaml") });
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless;component/Resources/Resources.xaml") });
    }
}
