using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.ViewModels;

namespace Fischless.ViewModels;

public partial class PageSettingsViewModel : ObservableRecipient
{
    [ObservableProperty]
    private AboutViewModel about = new();
}
