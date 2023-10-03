using Fischless.Design.Controls;
using Fischless.ViewModels;

namespace Fischless.Views;

public partial class SetLazyTokenDialog : WindowX
{
    public SetLazyTokenViewModel ViewModel { get; }

    public SetLazyTokenDialog()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }
}
