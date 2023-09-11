using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Views;
using System.Threading.Tasks;

namespace Fischless.ViewModels;

public partial class PageHomeViewModel : ObservableObject
{
    [RelayCommand]
    public async Task AddContactAsync()
    {
        ContactContentDialog contentDialog = new();
        await contentDialog.ShowAsync();
    }

    [RelayCommand]
    public void Refresh()
    {
    }
}
