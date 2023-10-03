using Fischless.Design.Controls;
using Fischless.ViewModels;

namespace Fischless.Views;

public partial class ShowLazyDialog : WindowX
{
    public ShowLazyViewModel ViewModel { get; }

    public ShowLazyDialog(string file)
    {
        DataContext = ViewModel = new();
        ViewModel.Reload(file);
        InitializeComponent();
    }
}
