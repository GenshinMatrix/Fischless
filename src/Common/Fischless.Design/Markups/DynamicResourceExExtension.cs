using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Fischless.Design.Markups;

[ObservableObject]
[MarkupExtensionReturnType(typeof(object))]
public partial class DynamicResourceExExtension : MarkupExtension
{
    [ObservableProperty]
    [SuppressMessage("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "MVVMTK0034:")]
#pragma warning disable CS0658
    [prop: ConstructorArgument(nameof(resourceKey))]
#pragma warning restore CS0658
    public Binding resourceKey = null!;

    public DynamicResourceExExtension()
    {
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (ResourceKey?.Path?.Path is string propName
         && serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget
         && provideValueTarget.TargetObject is DependencyObject targetObject
         && provideValueTarget.TargetProperty is DependencyProperty targetProperty
         && targetObject is FrameworkElement frameworkElement
         && frameworkElement.DataContext != null
         && frameworkElement.DataContext.GetType().GetProperty(propName) is PropertyInfo propInfo)
        {
            object? resourceKey = propInfo.GetValue(frameworkElement.DataContext);

            if (resourceKey != null)
            {
                frameworkElement.SetResourceReference(targetProperty, resourceKey);
            }
            return null!;
        }
        return DependencyProperty.UnsetValue;
    }
}
