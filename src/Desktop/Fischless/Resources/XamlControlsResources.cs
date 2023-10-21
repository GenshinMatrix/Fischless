using System;
using System.Windows;

namespace Fischless;

public sealed class XamlControlsResources : ResourceDictionary
{
    public XamlControlsResources()
    {
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless;component/Resources/Resources.xaml") });
    }
}
