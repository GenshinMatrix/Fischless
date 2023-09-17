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

    public async void OnContactMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            await ViewModel.LaunchGameFromListAsync();
        }
    }
}
