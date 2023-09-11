using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Fetch.Launch;
using Fischless.Fetch.Regedit;
using Fischless.Models;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace Fischless.ViewModels;

public partial class ContactViewModel : ObservableObject
{
    [ObservableProperty]
    private string? aliasName = null!;

    [ObservableProperty]
    private string? localIconUri = LocalAvatars.Default;

    public ObservableCollection<ContactSelectionViewModel> LocalIconSelectionUris { get; } = new();

    private string? prod = null!;
    public string? Prod
    {
        get => prod;
        set => SetProperty(ref prod, value?.Replace("\n", string.Empty) ?? string.Empty);
    }

    [ObservableProperty]
    private string? server = null!;

    [ObservableProperty]
    private int selectedServerIndex = (int)ContactServer.AUTO;
    partial void OnSelectedServerIndexChanged(int value)
    {
        RegetProd();
    }

    [ObservableProperty]
    private string? cookie = null!;

    public ContactViewModel()
    {
        foreach (var kv in LocalAvatars.Stocks)
        {
            LocalIconSelectionUris.Add(new()
            {
                Parent = this,
                LocalIconUri = kv.Value,
            });
        }
    }

    public void Reload(Contact contact = null!)
    {
        if (contact is null)
        {
            RegetProd();
            LocalIconUri = LocalAvatars.Default;
        }
        else
        {
            AliasName = contact.AliasName;
            LocalIconUri = contact.LocalIconUri;
            Server = contact.Server;
            Prod = contact.Prod;
            Cookie = contact.Cookie;
        }
    }

    [RelayCommand]
    public void ChangeIconButton(string uriString)
    {
        LocalIconUri = uriString;
    }

    [RelayCommand]
    public void GenerateAliasName()
    {
        AliasName = RandomChineseWords.Generate();
    }

    [RelayCommand]
    private void RegetProd()
    {
        switch ((ContactServer)SelectedServerIndex)
        {
            case ContactServer.AUTO:
                if (!string.IsNullOrEmpty(Prod = GIRegedit.ProdCN))
                {
                    Server = GILauncher.RegionCN;
                }
                else
                {
                    Prod = GIRegedit.ProdOVERSEA;
                    Server = GILauncher.RegionOVERSEA;
                }
                break;
            case ContactServer.CN:
                Prod = GIRegedit.ProdCN;
                Server = GILauncher.RegionCN;
                break;
            case ContactServer.OVERSEA:
                Prod = GIRegedit.ProdOVERSEA;
                Server = GILauncher.RegionOVERSEA;
                break;
        }
    }
}

public partial class Contact : ObservableObject
{
    [ObservableProperty]
    public string guid = System.Guid.NewGuid().ToString("N");

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
    private int? uid = null!;

    [ObservableProperty]
    private int? level = null!;
}

public partial class ContactSelectionViewModel : ObservableObject
{
    public ContactViewModel? Parent { get; set; }

    [ObservableProperty]
    private string? localIconUri = null!;
}

public enum ContactServer
{
    AUTO,
    CN,
    OVERSEA,
}

file static class RandomChineseWords
{
    static RandomChineseWords()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public static string Generate(int? countRequest = null!)
    {
        int count = countRequest ?? new Random(Guid.NewGuid().GetHashCode()).Next(2, 6);
        string chineseWords = string.Empty;
        Random rm = new();

        for (int i = 0; i < count; i++)
        {
            int regionCode = rm.Next(16, 56);
            int positionCode;

            if (regionCode == 55)
            {
                positionCode = rm.Next(1, 90);
            }
            else
            {
                positionCode = rm.Next(1, 95);
            }

            int regionCode_Machine = regionCode + 160;
            int positionCode_Machine = positionCode + 160;
            byte[] bytes = new byte[]
            {
                (byte)regionCode_Machine,
                (byte)positionCode_Machine,
            };

            chineseWords += Encoding.GetEncoding("gb2312").GetString(bytes);
        }
        return chineseWords;
    }
}
