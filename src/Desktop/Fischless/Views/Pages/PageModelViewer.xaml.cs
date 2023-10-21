using Fischless.ViewModels;
using ModernWpf.Controls;
using System;
using System.Windows;

namespace Fischless.Views;

public partial class PageModelViewer : Page
{
    public PageModelViewerViewModel ViewModel { get; }

    public PageModelViewer()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }

    private void OnDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetData(DataFormats.FileDrop) is Array array)
        {
            if (array.Length > 0)
            {
                string fileName = array.GetValue(0).ToString();
                ViewModel.LoadModel(fileName);
            }
        }
    }
}
