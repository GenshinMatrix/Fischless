﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.ModelViewer;
using Microsoft.Win32;
using System.IO;
using Windows.UI.Core;

namespace Fischless.ViewModels;

public partial class PageModelViewerViewModel : ObservableRecipient
{
    [ObservableProperty]
    private string modelPath = null!;
    partial void OnModelPathChanged(string value)
    {
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
            Title = "选择DMM模型文件",
            Filter = "DMM(*.pmx,*.zip,*.7z,*.rar)|*.pmx;*.zip;*.7z;*.rar",
            RestoreDirectory = true,
            DefaultExt = "pmx",
            InitialDirectory = Directory.Exists(ForDispatcher.ApplicationModelPath) ? ForDispatcher.ApplicationModelPath : null,
        };
        if (dialog.ShowDialog() ?? false)
        {
            LoadModel(dialog.FileName);
        }
    }
}
