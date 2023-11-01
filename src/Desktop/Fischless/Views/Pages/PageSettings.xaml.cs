using Fischless.ViewModels;
using ModernWpf.Controls;
using System;

namespace Fischless.Views;

public partial class PageSettings : Page, IDisposable
{
    public PageSettingsViewModel ViewModel { get; } = null!;

    public PageSettings(PageSettingsViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    public void Dispose()
    {
    }
}
