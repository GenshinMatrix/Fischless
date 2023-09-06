using Fischless.Controls;
using Fischless.Plugin.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Composition;

namespace Fischless.Plugin.Demo;

[Export(typeof(IPluginPage))]
[PluginPage(ServiceLifetime.Transient)]
public partial class DemoPluginPage : WindowX, IPluginPage
{
    public DemoPluginPage()
    {
        InitializeComponent();
    }
}
