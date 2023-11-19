using Fischless.ViewModels;
using ModernWpf.Controls;

namespace Fischless.Views;

public partial class AboutContentDialog : ContentDialog
{
    public AboutViewModel ViewModel { get; }

    public AboutContentDialog()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }
}
