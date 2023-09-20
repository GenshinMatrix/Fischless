using Fischless.ViewModels;
using ModernWpf.Controls;

namespace Fischless.Views;

public partial class ContactSettingsContentDialog : ContentDialog
{
    public ContactSettingsViewModel ViewModel { get; }

    public ContactSettingsContentDialog()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }

    protected void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs e)
    {
        ///
    }
}
