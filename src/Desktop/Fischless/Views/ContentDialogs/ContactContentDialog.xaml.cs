using Fischless.ViewModels;
using ModernWpf.Controls;

namespace Fischless.Views;

public partial class ContactContentDialog : ContentDialog
{
    public ContactViewModel ViewModel { get; }

    public ContactContentDialog()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }
}
