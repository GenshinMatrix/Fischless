using System.Windows;

namespace Fischless.Design;

public sealed class XamlControlsResources : ResourceDictionary
{
    public XamlControlsResources()
    {
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless.Design;component/Resources/Resources.xaml") });
    }
}
