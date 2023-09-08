using ModernWpf.Controls;
using Fischless.ViewModels;
using System;

namespace Fischless.Views;

public partial class PageSettings : Page, IDisposable
{
    public PageSettingsViewModel ViewModel { get; } = null!;

    public PageSettings()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }

    public void Dispose()
    {
    }
}
