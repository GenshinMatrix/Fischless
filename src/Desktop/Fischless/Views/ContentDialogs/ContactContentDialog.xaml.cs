using Fischless.Designs.Controls;
using Fischless.Models;
using Fischless.Models.Message;
using Fischless.ViewModels;
using ModernWpf.Controls;
using System.Threading.Tasks;

namespace Fischless.Views;

public partial class ContactContentDialog : ContentDialog
{
    public ContactViewModel ViewModel { get; }

    public ContactContentDialog()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }

    public async Task<ContactMessage> AddContactAsync()
    {
        ContentDialogResult result = await ShowAsync();

        if (result == ContentDialogResult.Secondary)
        {
            return new ContactMessage()
            {
                Type = ContactMessageType.Added,
                Contact = new Contact()
                {
                    AliasName = ViewModel.AliasName,
                    LocalIconUri = ViewModel.LocalIconUri,
                    Server = ViewModel.Server,
                    Prod = ViewModel.Prod,
                    Cookie = ViewModel.Cookie,
                    IsUseCookie = ViewModel.IsUseCookie,
                },
            };
        }
        return null!;
    }

    protected void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs e)
    {
        ///
    }

    protected void OnSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ViewModel.AliasName))
        {
            Toast.Warning("咱们还是先取个别名吧", ToastLocation.BottomCenter);
            e.Cancel = true;
            return;
        }
    }
}
