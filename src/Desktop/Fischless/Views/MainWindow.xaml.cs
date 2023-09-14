using Fischless.Design.Controls;
using Fischless.ViewModels;

namespace Fischless.Views;

public partial class MainWindow : WindowX
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }
}
