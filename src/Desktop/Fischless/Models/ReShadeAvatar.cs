using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.Fetch.Datas.Core;
using System;
using System.Collections.Generic;

namespace Fischless.Models;

public partial class ReShadeAvatar : ObservableObject
{
    [ObservableProperty]
    private string card;

    [ObservableProperty]
    private string faceIcon;

    [ObservableProperty]
    private string rarityBackground;

    [ObservableProperty]
    private string nameKey;

    [ObservableProperty]
    private int gender;

    [ObservableProperty]
    private ElementType element;

    [ObservableProperty]
    private WeaponType weaponType;

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private int sortId;

    [ObservableProperty]
    private DateTime beginTime;

    [ObservableProperty]
    private bool isVisible = true;

    [ObservableProperty]
    private IEnumerable<ReShadeAvatarOutfit> outfits = Array.Empty<ReShadeAvatarOutfit>();
}

public partial class ReShadeAvatarOutfit : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private int characterId;

    [ObservableProperty]
    private string nameKey;

    [ObservableProperty]
    private bool isDefault;

    [ObservableProperty]
    private string card;

    [ObservableProperty]
    private string faceIcon;

    [ObservableProperty]
    private string skinHash;
}
