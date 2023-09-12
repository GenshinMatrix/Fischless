using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Configuration;
using Fischless.Models;
using Fischless.Models.Message;
using Fischless.Views;
using System.Linq;
using System.Threading.Tasks;

namespace Fischless.ViewModels;

public partial class PageHomeViewModel : ObservableObject
{
    [RelayCommand]
    public async Task AddContactAsync()
    {
        ContactContentDialog contentDialog = new();
        ContactMessage message = await contentDialog.AddContactAsync();

        if (message != null)
        {
            Configurations.Contacts.Set(Configurations.Contacts.Get().Append(message.Contact));
            ConfigurationManager.Save();
            WeakReferenceMessenger.Default.Send(message);
        }
    }

    [RelayCommand]
    public void Refresh()
    {
    }
}
