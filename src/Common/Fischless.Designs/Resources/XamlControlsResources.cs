using System.Windows;

namespace Fischless.Designs;

public sealed class XamlControlsResources : ResourceDictionary
{
    public XamlControlsResources()
    {
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless.Designs;component/Resources/Converters.xaml") });
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless.Designs;component/Resources/Resources.xaml") });
    }
}
