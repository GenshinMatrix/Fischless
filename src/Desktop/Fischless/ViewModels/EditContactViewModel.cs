using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Fetch.Launch;
using Fischless.Fetch.Regedit;
using Fischless.Models;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace Fischless.ViewModels;

public partial class EditContactViewModel : ObservableObject
{
    [ObservableProperty]
    private string? aliasName = null!;

    [ObservableProperty]
    private string? localIconUri = LocalAvatars.Default;

    public ObservableCollection<ContactSelectionViewModel> LocalIconSelectionUris { get; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ProdMD5))]
    private string? prod = null!;
    partial void OnProdChanged(string? value)
    {
        Prod = value?.Replace("\n", string.Empty) ?? string.Empty;
    }
    public string? ProdMD5
    {
        get
        {
            byte[] hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(Prod));
            StringBuilder sb = new();

            sb.Append("MD5: ");
            if (string.IsNullOrEmpty(Prod))
            {
                sb.Append("NULL");
                return sb.ToString();
            }
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

    [ObservableProperty]
    private string? server = null!;

    [ObservableProperty]
    private int selectedServerIndex = (int)ContactServer.Auto;
    partial void OnSelectedServerIndexChanged(int value)
    {
        RegetProd();
    }

    [ObservableProperty]
    private string? cookie = null!;

    [ObservableProperty]
    private bool isUseCookie = true;

    public EditContactViewModel()
    {
        foreach (var kv in LocalAvatars.Stocks)
        {
            LocalIconSelectionUris.Add(new()
            {
                Parent = this,
                LocalIconUri = kv.Value,
            });
        }
        Reload();
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
            IsUseCookie = contact.IsUseCookie;
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
            case ContactServer.Auto:
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

public partial class ContactSelectionViewModel : ObservableObject
{
    public EditContactViewModel? Parent { get; set; }

    [ObservableProperty]
    private string? localIconUri = null!;
}

public enum ContactServer
{
    Auto,
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
