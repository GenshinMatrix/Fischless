using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Fetch.Launch;
using Fischless.Fetch.Lazy;
using Fischless.Hoyolab;
using Fischless.Hoyolab.Account;
using Fischless.Logging;
using Fischless.Models;
using Fischless.Mvvm;
using Fischless.Threading;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fischless.ViewModels;

public sealed partial class ContactViewModel : ObservableObject
{
    public HoyolabClient Client { get; private set; } = new();

    [ObservableProperty]
    private Contact contact = null!;

    [ObservableProperty]
    private bool isRunning = false;

    [ObservableProperty]
    private bool isFetched = false;

    [ObservableProperty]
    private HoyolabUserInfo? userInfoFetched = new();

    [ObservableProperty]
    private GenshinRoleInfo? roleFetched = new();

    [ObservableProperty]
    private LazyInfo? lazyInfoFetched = new();

    [ObservableProperty]
    private ContactProgress lazyInfo = new();

    public ContactViewModel(Contact contact)
    {
        Contact = contact;
    }

    public async Task FetchAllAsync()
    {
        if (!string.IsNullOrWhiteSpace(Contact.Cookie) && Contact.IsUseCookie)
        {
            try
            {
                _ = FetchLazyInfoAsync();
                await FetchHoyolabUserInfoAsync();
                await FetchGenshinRoleInfosAsync();

                IsFetched = true;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }

    public async Task FetchHoyolabUserInfoAsync()
    {
        if (!string.IsNullOrEmpty(Contact.Cookie))
        {
            try
            {
                UserInfoFetched = await Client.GetHoyolabUserInfoAsync(Contact.Server.ToHoyolabRegion(), Contact.Cookie);
                Log.Information($"[HoyolabUserInfo] Uid=\"{UserInfoFetched.Uid}\" NickName=\"{UserInfoFetched.Nickname}\" AvatarUrl=\"{UserInfoFetched.AvatarUrl}\"");
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }

    public async Task FetchGenshinRoleInfosAsync()
    {
        if (!string.IsNullOrEmpty(Contact.Cookie))
        {
            try
            {
                RoleFetched = (await Client.GetGenshinRoleInfosAsync(Contact.Server.ToHoyolabRegion(), Contact.Cookie)).FirstOrDefault();

                Log.Information($"[GenshinRoleInfo] Uid=\"{RoleFetched!.Uid}\" NickName=\"{RoleFetched!.Nickname}\"");
#if DEMOPRO
                Contact.NickName = Mui("AvatarNameOfAozi");
#else
                Contact.NickName = RoleFetched!.Nickname;
#endif
                Contact.Uid = RoleFetched!.Uid;
                Contact.Level = RoleFetched!.Level;
                Contact.RegionName = RoleFetched!.RegionName;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }

    public async Task FetchLazyInfoAsync()
    {
        if (!Configurations.IsUseLazy.Get())
        {
            return;
        }

        try
        {
            if (!LazyInfoFetched.IsUnlocked)
            {
                LazyInfoFetched.IsUnlocked = await LazyProtocol.IsVaildProtocolAsync();
            }

            if (Contact.Uid == null)
            {
                await FetchGenshinRoleInfosAsync();
            }

            bool hasLazyToday = await LazyOutputHelper.Check(Contact.Uid?.ToString()!);

            LazyInfo.SetGreen(hasLazyToday);

            LazyInfoFetched!.IsFinished = hasLazyToday;
            LazyInfoFetched!.IsFetched = true;
            OnPropertyChanged(nameof(LazyInfoFetched));
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
            LazyInfo.SetYellow(true);
            LazyInfoFetched!.IsFinished = false;
            LazyInfoFetched!.IsFetched = false;
            OnPropertyChanged(nameof(LazyInfoFetched));
        }
    }

    [RelayCommand]
    public static async Task LaunchLazyAsync()
    {
        if (await LazyProtocol.IsVaildProtocolAsync())
        {
            await LazyProtocol.LaunchAsync();
        }
    }
}

public partial class ContactProgress : ObservableRecipient
{
    [ObservableProperty]
    private bool isShown = true;

    [ObservableProperty]
    private ObservableBox<double> value = 0d;

    [ObservableProperty]
    private ObservableBox<double> valueMin = 0d;

    [ObservableProperty]
    private ObservableBox<double> valueMax = 100d;

    [ObservableProperty]
    private double opacity = 1d;

    [ObservableProperty]
    private bool isNotified = false;

    [ObservableProperty]
    private bool isRed = false;

    [ObservableProperty]
    private bool isYellow = false;

    [ObservableProperty]
    private bool isGreen = false;

    [ObservableProperty]
    private bool isRedCanceled = false;

    [RelayCommand]
    public void CancelRed()
    {
        IsRedCanceled = true;
        IsRed = false;
    }

    public void SetGreen(bool value, bool isRedable = false)
    {
        IsGreen = value;
        IsRed = !IsRedCanceled && isRedable && !value;
        IsYellow = false;
    }

    public void SetYellow(bool value, bool isRedable = false)
    {
        IsYellow = value;
        IsRed = !IsRedCanceled && isRedable && !value;
        IsGreen = !value;
    }

    public void SetRed(bool value, bool isRedable = false)
    {
        IsRed = !IsRedCanceled && isRedable && value;
        IsGreen = !value;
        IsYellow = false;
    }
}

public partial class LazyInfo : ObservableObject
{
    [ObservableProperty]
    private bool isUnlocked = false;

    [ObservableProperty]
    private bool isFetched = false;

    [ObservableProperty]
    private bool isFinished = false;
}
