using Fischless.Design.Controls;
using Fischless.Models;
using Fischless.Models.Message;
using Fischless.ViewModels;
using ModernWpf.Controls;
using System.Threading.Tasks;

namespace Fischless.Views;

public partial class EditContactContentDialog : ContentDialog
{
    public EditContactViewModel ViewModel { get; }

    public EditContactContentDialog()
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

    public async Task<ContactMessage> EditContactAsync(Contact contact)
    {
        ViewModel.Reload(contact);
        ContentDialogResult result = await ShowAsync();

        if (result == ContentDialogResult.Secondary)
        {
            return new ContactMessage()
            {
                Type = ContactMessageType.Edited,
                Contact = new Contact()
                {
                    Guid = contact.Guid,
                    AliasName = ViewModel.AliasName,
                    LocalIconUri = ViewModel.LocalIconUri,
                    Prod = ViewModel.Prod,
                    Server = ViewModel.Server,
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
            Toast.Warning(Mui("LaunchGameSettingsAliasNamingHint"), ToastLocation.BottomCenter);
            e.Cancel = true;
            return;
        }
    }
}
