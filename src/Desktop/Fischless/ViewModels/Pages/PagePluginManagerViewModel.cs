using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.Mapper;
using Fischless.Mvvm;
using Fischless.Plugin.Abstractions;
using System;
using System.Linq;
using System.Windows.Input;

namespace Fischless.ViewModels;

public partial class PagePluginManagerViewModel : ObservableRecipient
{
    [ObservableProperty]
    private ObservableCollectionEx<PluginViewModel> plugins = new();

    public PagePluginManagerViewModel()
    {
        Plugins.Reset(PluginProvider.Reload()
            .Select(p => new PluginViewModel(p.Plugin)));
    }
}

public partial class PluginViewModel : ObservableRecipient
{
    [ObservableProperty]
    private string pluginName;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private object icon;

    [ObservableProperty]
    private string author;

    [ObservableProperty]
    private Version version;

    [ObservableProperty]
    private int index;

    [ObservableProperty]
    private bool isNaviabled;

    [ObservableProperty]
    private bool isShowButton = false;

    [ObservableProperty]
    private ICommand buttonCommand;

    [ObservableProperty]
    private bool isInternalPlugin = false;

    public PluginViewModel(object plugin)
    {
        if (plugin is IPlugin plugin1)
        {
            MapperProvider.Map(plugin1, this);

            if (plugin1.GetType().Assembly.GetCustomAttributes(typeof(FischlessInternalPluginAttribute), false).Any())
            {
                IsInternalPlugin = true;

                PluginName = Mui(PluginName);
                Description = Mui(Description);
            }
        }
        if (plugin is IPlugin2 plugin2)
        {
            MapperProvider.Map(plugin2, this);
        }
    }
}
