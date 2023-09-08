using ModernWpf.Controls;
using Fischless.ViewModels;
using System;

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
}
