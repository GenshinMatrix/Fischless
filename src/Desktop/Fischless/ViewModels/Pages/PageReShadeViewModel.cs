﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Fetch.Datas.Core;
using Fischless.Fetch.Datas.Snap;
using Fischless.Models;
using Fischless.Mvvm;
using System;
using System.Linq;

namespace Fischless.ViewModels;

public partial class PageReShadeViewModel : ObservableRecipient, IDisposable
{
    [ObservableProperty]
    private ObservableCollectionEx<ReShadeAvatar> avatars = new();

    public PageReShadeViewModel()
    {
        avatars.Reset(SnapCharacterProvider.GetSnapCharacters()
            .Select(c => new ReShadeAvatar()
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
            })
            .OrderByDescending(c => c.SortId)
        );
    }

    public void Dispose()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }
}
