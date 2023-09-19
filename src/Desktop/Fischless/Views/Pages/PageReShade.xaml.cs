using Fischless.ViewModels;
using ModernWpf.Controls;
using System;

namespace Fischless.Views;

public partial class PageReShade : Page, IDisposable
{
    public PageReShadeViewModel ViewModel { get; } = null!;

    public PageReShade(PageReShadeViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    public void Dispose()
    {
    }
}
