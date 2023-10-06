using CommunityToolkit.Mvvm.ComponentModel;

namespace Fischless.Models;

public partial class ReShadeAvatar : ObservableObject
{
    [ObservableProperty]
    public string avatarIcon;

    [ObservableProperty]
    public string rarityBackground;

    [ObservableProperty]
    public string nameKey;
}
