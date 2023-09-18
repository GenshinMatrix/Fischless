using Fischless.Design.Controls;
using Fischless.Models;
using Fischless.ViewModels;

namespace Fischless.Views;

public partial class MainWindow : WindowX
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
        Closing += (_, e) =>
        {
            if (Configurations.CloseToTray.Get())
            {
                e.Cancel = true;
                Hide();
            }
        };
    }
}
