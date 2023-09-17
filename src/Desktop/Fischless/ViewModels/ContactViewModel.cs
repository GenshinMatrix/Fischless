using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.Fetch.Launch;
using Fischless.Hoyolab;
using Fischless.Hoyolab.Account;
using Fischless.Models;
using Serilog;
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
                Contact.NickName = RoleFetched!.Nickname;
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
        //try
        //{
        //    LazyInfoViewModel!.IsUnlocked = await LazyVerification.VerifyAssembly(Settings.ComponentLazyPath.Get());

        //    if (!LazyInfoViewModel.IsUnlocked)
        //    {
        //        LazyInfoViewModel.IsUnlocked = await LazyProtocol.IsVaildProtocolAsync();
        //    }

        //    if (Contact.Uid == null)
        //    {
        //        await FetchGenshinRoleInfosAsync();
        //    }

        //    bool hasLazyToday = await LazyOutputHelper.Check(Contact.Uid?.ToString()!);

        //    LazyInfo.SetGreen(hasLazyToday, Settings.HintQuestRandomProceRed);

        //    LazyInfoViewModel!.IsFinished = hasLazyToday;
        //    LazyInfoViewModel!.IsFetched = true;
        //    OnPropertyChanged(nameof(LazyInfoViewModel));
        //}
        //catch (Exception e)
        //{
        //    Logger.Error(e);
        //    LazyInfo.SetYellow(true, Settings.HintQuestRandomProceRed);
        //    LazyInfoViewModel!.IsFinished = false;
        //    LazyInfoViewModel!.IsFetched = false;
        //    OnPropertyChanged(nameof(LazyInfoViewModel));
        //}
    }
}
