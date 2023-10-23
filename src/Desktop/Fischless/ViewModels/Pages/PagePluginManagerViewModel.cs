﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Design.Controls;
using Fischless.Helpers;
using Fischless.Mapper;
using Fischless.ModelViewer;
using Fischless.Mvvm;
using Fischless.Plugin.Abstractions;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.System;

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

    [RelayCommand]
    public async Task OpenFolderAsync()
    {
        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginProvider.PluginsPath);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        await Launcher.LaunchUriAsync(new Uri($"file://{folderPath.Replace(Path.DirectorySeparatorChar, '/')}"));
    }

    [RelayCommand]
    public void Install()
    {
        OpenFileDialog dialog = new()
        {
            Title = "选择插件",
            Filter = "插件(*.dll)|*.dll",
            RestoreDirectory = true,
            DefaultExt = "dll",
        };
        if (dialog.ShowDialog() ?? false)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginProvider.PluginsPath);
            FileInfo fileInfo = new(dialog.FileName);

            if (fileInfo.DirectoryName.Replace('/', '\\') == folderPath.Replace('/', '\\'))
            {
                return;
            }

            try
            {
                // TODO: Check assembly
            }
            catch (Exception e)
            {
                MessageBoxX.Error(e.ToString());
                return;
            }

            try
            {
                File.Move(dialog.FileName, Path.Combine(folderPath, fileInfo.Name));
            }
            catch (Exception e)
            {
                MessageBoxX.Error(e.ToString());
                return;
            }

            if (MessageBoxX.Question("需要应用重启后生效，是否立即重启应用？") == MessageBoxResult.Yes)
            {
                RuntimeHelper.Restart(forced: true);
            }
        }
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
