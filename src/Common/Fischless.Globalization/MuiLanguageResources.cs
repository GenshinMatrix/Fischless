using System.Windows;

namespace Fischless.Globalization;

public sealed class MuiLanguageResources : ResourceDictionary
{
    public MuiLanguageResources()
    {
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless.Globalization;component/Assets/Langs/zh.xaml") });
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless.Globalization;component/Assets/Langs/ja.xaml") });
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless.Globalization;component/Assets/Langs/en.xaml") });
    }
}
