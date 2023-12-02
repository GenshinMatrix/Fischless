using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.ModelViewer;
using Fischless.Mvvm;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Fischless.ViewModels;

public partial class PageModelViewerViewModel : ObservableRecipient
{
    [ObservableProperty]
    private string modelPath = null!;

    [ObservableProperty]
    private Func<string[], Task<string>> selector = null!;

    [ObservableProperty]
    private ObservableCollectionEx<string> pmxs = [];

    public PageModelViewerViewModel()
    {
        Selector = async (inputs) =>
        {
            Pmxs = new(inputs);
            await Task.CompletedTask;
            return inputs[0];
        };
    }

    partial void OnModelPathChanged(string value)
    {
        ///
    }

    public void LoadModel(string modelPath)
    {
        ModelPath = modelPath;
    }

    [RelayCommand]
    public void OpenModel()
    {
        OpenFileDialog dialog = new()
        {
            Title = Mui("ModelViewerSelectMMD"),
            Filter = "MMD(*.pmx,*.zip,*.7z,*.rar)|*.pmx;*.zip;*.7z;*.rar",
            RestoreDirectory = true,
            DefaultExt = "pmx",
            InitialDirectory = Directory.Exists(ForDispatcher.ApplicationModelPath) ? ForDispatcher.ApplicationModelPath : null,
        };
        if (dialog.ShowDialog() ?? false)
        {
            LoadModel(dialog.FileName);
        }
    }

    [RelayCommand]
    public void ResetCamera(object param)
    {
        if (param is HelixViewer viewer)
        {
            viewer.ResetCamera();
        }
    }
}
