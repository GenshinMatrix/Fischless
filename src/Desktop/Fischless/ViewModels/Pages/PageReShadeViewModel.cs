using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Fetch.Datas.Core;
using Fischless.Fetch.Datas.Snap;
using Fischless.Models;
using Fischless.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fischless.ViewModels;

public partial class PageReShadeViewModel : ObservableRecipient, IDisposable
{
    [ObservableProperty]
    private IEnumerable<ReShadeAvatar> avatarStocks = null!;

    [ObservableProperty]
    private ObservableCollectionEx<ReShadeAvatar> avatars = new();

    [ObservableProperty]
    private bool isPyro = false;
    partial void OnIsPyroChanged(bool value) => SyncSearchElement();

    [ObservableProperty]
    private bool isHydro = false;
    partial void OnIsHydroChanged(bool value) => SyncSearchElement();

    [ObservableProperty]
    private bool isAnemo = false;
    partial void OnIsAnemoChanged(bool value) => SyncSearchElement();

    [ObservableProperty]
    private bool isElectro = false;
    partial void OnIsElectroChanged(bool value) => SyncSearchElement();

    [ObservableProperty]
    private bool isDendro = false;
    partial void OnIsDendroChanged(bool value) => SyncSearchElement();

    [ObservableProperty]
    private bool isIce = false;
    partial void OnIsIceChanged(bool value) => SyncSearchElement();

    [ObservableProperty]
    private bool isGeo = false;
    partial void OnIsGeoChanged(bool value) => SyncSearchElement();

    public PageReShadeViewModel()
    {
        AvatarStocks = SnapCharacterProvider.GetSnapCharacters()
            .OrderByDescending(c => c.SortId)
            .Select((c, i) => new ReShadeAvatar()
            {
                Card = $"pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/{c.Card}",
                FaceIcon = $"pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/{c.FaceIcon}",
                RarityBackground = @$"pack://application:,,,/Fischless;component/Assets/Images/Rarity_{c.Rarity}_Background.png",
                NameKey = $"AvatarNameOf{c.FaceIcon.Replace("UI_AvatarIcon_", string.Empty).Replace(".png", string.Empty)}",
                Gender = c.Gender,
                Element = c.Element,
                WeaponType = c.WeaponType,
                Id = c.Id,
                SortId = c.SortId,
                BeginTime = c.BeginTime,
                Outfits = c.Outfits.Select(o => new ReShadeAvatarOutfit()
                {
                    Id = o.Id,
                    CharacterId = o.CharacterId,
                    IsDefault = o.IsDefault,
                    NameKey = $"AvatarNameOf{c.FaceIcon.Replace("UI_AvatarIcon_", string.Empty).Replace(".png", string.Empty)}",
                    Card = $"pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/{o.Card}",
                    FaceIcon = $"pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/{o.FaceIcon}",
                    SkinHash = o.SkinHash,
                }),
                IsSelected = i == 0,
            });
        avatars.Reset(AvatarStocks);
    }

    public void Dispose()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    private void SyncSearchElement()
    {
        if (!IsPyro && !IsHydro && !IsAnemo && !IsElectro && !IsDendro && !IsIce && !IsGeo)
        {
            foreach (ReShadeAvatar avtar in Avatars)
            {
                avtar.IsVisible = true;
            }
            return;
        }

        foreach (ReShadeAvatar avtar in Avatars)
        {
            avtar.IsVisible = (avtar.Element == ElementType.Pyro) && IsPyro
                           || (avtar.Element == ElementType.Hydro) && IsHydro
                           || (avtar.Element == ElementType.Anemo) && IsAnemo
                           || (avtar.Element == ElementType.Electro) && IsElectro
                           || (avtar.Element == ElementType.Dendro) && IsDendro
                           || (avtar.Element == ElementType.Ice) && IsIce
                           || (avtar.Element == ElementType.Geo) && IsGeo;
        }
    }
}
