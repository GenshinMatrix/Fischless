using System;
using System.Windows;

namespace Fischless.Resources;

public sealed class MuiLanguageResources : ResourceDictionary
{
    public MuiLanguageResources()
    {
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless;component/Assets/Langs/zh.xaml") });
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless;component/Assets/Langs/ja.xaml") });
        MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Fischless;component/Assets/Langs/en.xaml") });
    }
}
