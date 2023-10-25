using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Design.Controls;
using Fischless.Fetch.Regedit;
using Fischless.Logging;
using Fischless.Mapper;
using Fischless.Models;
using Fischless.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows.Forms;

namespace Fischless.Plugin.RepairRegedit.ViewModel;

public partial class RepairRegeditViewModel : ObservableObject
{
    [ObservableProperty]
    private string registryKey = null!;

    [ObservableProperty]
    private string displayIcon = null!;

    [ObservableProperty]
    private string displayName = null!;

    [ObservableProperty]
    private string displayVersion = null!;

    [ObservableProperty]
    private object estimatedSize = null!;

    [ObservableProperty]
    private string exeName = null!;

    [ObservableProperty]
    private string installPath = null!;

    [ObservableProperty]
    private string networkType = null!;

    [ObservableProperty]
    private string publisher = null!;

    [ObservableProperty]
    private string uninstallString = null!;

    [ObservableProperty]
    private string uRLInfoAbout = null!;

    [ObservableProperty]
    private string uUID = null!;

    [ObservableProperty]
    private int regionType = 0;
    partial void OnRegionTypeChanged(int value)
    {
        if (!Reload((RegionTypeIndex)value switch
        {
            RegionTypeIndex.OVERSEA => GIGameType.OVERSEA,
            _ => GIGameType.CN,
        }))
        {
            ReloadKey();
        }
    }

    public RepairRegeditViewModel()
    {
        Reload();
    }

    private void Reload()
    {
        if (!Reload(GIGameType.CN))
        {
            if (!Reload(GIGameType.OVERSEA))
            {
                ReloadKey();
            }
        }
    }

    private bool Reload(GIGameType type)
    {
        try
        {
            GIRegeditUninstallInfo info = GIRegedit.GetUninstallInfo(type);

            if (info == null)
            {
                return false;
            }

            MapperProvider.Map(info, this);
            return true;
        }
        catch (Exception e)
        {
            Log.Information(e.ToString());
        }
        return false;
    }

    public void ReloadKey()
    {
        RegistryKey = GIRegedit.GetRegUninstallKey((RegionTypeIndex)RegionType switch
        {
            RegionTypeIndex.Auto => InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName switch
            {
                "zh" => GIGameType.CN,
                _ => GIGameType.OVERSEA,
            },
            RegionTypeIndex.CN => GIGameType.CN,
            RegionTypeIndex.OVERSEA or _ => GIGameType.OVERSEA,
        });
    }

    public bool Save()
    {
        try
        {
            GIRegedit.SetUninstallInfo(MapperProvider.Map(this, new GIRegeditUninstallInfo()).Repair());
            return true;
        }
        catch (Exception e)
        {
            Log.Information(e.ToString());
            _ = MessageBoxX.Error(e.ToString());
        }
        return false;
    }

    [RelayCommand]
    public void OpenRegedit()
    {
        FluentProcess.Create()
            .FileName("regedit")
            .UseShellExecute()
            .Verb("runas")
            .Start()
            .Forget();
    }

    [RelayCommand]
    public void OpenFolder()
    {
        CommonOpenFileDialog dialog = new()
        {
            IsFolderPicker = true,
            RestoreDirectory = true,
            InitialDirectory = Configurations.ReShadePath.Get(),
            DefaultDirectory = Configurations.ReShadePath.Get(),
            Title = "选择 Genshin Impact 目录"
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string selectedDirectory = dialog.FileName;

            if (!File.Exists(Path.Combine(selectedDirectory, "launcher.exe")))
            {
                Toast.Warning($"launcher.exe 不存在");
                return;
            }

            InstallPath = selectedDirectory;
        }
    }

    public string ToRegFileText()
    {
        return MapperProvider.Map(this, new GIRegeditUninstallInfo()).Repair().GetRegFileText();
    }
}

file enum RegionTypeIndex
{
    Auto = 0,
    CN = 1,
    OVERSEA = 2,
}
