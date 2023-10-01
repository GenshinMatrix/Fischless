using Fischless.Design.Controls;
using Fischless.Models;
using Fischless.Services;
using Fischless.ViewModels;

namespace Fischless.Views;

public partial class MainWindow : WindowX
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
        Loaded += (_, _) =>
        {
            AppConfig.GetService<IForeverTickService>()?.Start();
        };
        Closing += (_, e) =>
        {
            if (Configurations.CloseToTray.Get())
            {
                e.Cancel = true;
                Hide();
            }
        };
        Closed += (_, _) =>
        {
            AppConfig.GetService<IForeverTickService>()?.Stop();
        };
    }
}
