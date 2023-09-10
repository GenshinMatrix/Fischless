using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Views;
using System.Threading.Tasks;

namespace Fischless.ViewModels;

public partial class PageHomeViewModel : ObservableRecipient
{
    [RelayCommand]
    public async Task AddContact()
    {
        ContactContentDialog contentDialog = new();
        await contentDialog.ShowAsync();
    }

    [RelayCommand]
    public void Refresh()
    {
    }
}

public partial class Contact : ObservableObject
{
}
