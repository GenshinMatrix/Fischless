using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection;

namespace Fischless.Models;

[Obfuscation]
public partial class Contact : ObservableObject
{
    [ObservableProperty]
    private string guid = System.Guid.NewGuid().ToString("N");

    [ObservableProperty]
    private string? aliasName = null!;

    [ObservableProperty]
    private string? nickName = null!;

    [ObservableProperty]
    private string? localIconUri = null!;

    [ObservableProperty]
    private string? prod = null!;

    [ObservableProperty]
    private string? server = null!;

    [ObservableProperty]
    private string? regionName = null!;

    [ObservableProperty]
    private string? cookie = null!;

    [ObservableProperty]
    private bool isUseCookie = true;

    [ObservableProperty]
    private int? uid = null!;

    [ObservableProperty]
    private int? level = null!;
}
