using ModernWpf.Controls;
using Fischless.ViewModels;
using System;
using System.Windows.Input;

namespace Fischless.Views;

public partial class PageHome : Page, IDisposable
{
    public PageHomeViewModel ViewModel { get; } = null!;

    public PageHome()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }

    public void Dispose()
    {
    }

    public async void OnContactMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        await ViewModel.LaunchGameFromListAsync();
    }
}
