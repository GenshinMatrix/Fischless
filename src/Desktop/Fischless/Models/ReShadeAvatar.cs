using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Fetch.Datas.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Fischless.Models;

[DebuggerDisplay("{card}")]
public partial class ReShadeAvatar : ObservableObject
{
    public event EventHandler<bool> IsSelectedChanged = null!;

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
    private bool isSelected = false;
    partial void OnIsSelectedChanged(bool value)
    {
        IsSelectedChanged?.Invoke(this, value);
    }

    [ObservableProperty]
    private IEnumerable<ReShadeAvatarOutfit> outfits = Array.Empty<ReShadeAvatarOutfit>();

    [ObservableProperty]
    private string? textureOverride = null;
}

[DebuggerDisplay("{Id},{NameKey}")]
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
}

[DebuggerDisplay("{FolderName}")]
public partial class ReShadeAvatarList : ObservableObject
{
    [ObservableProperty]
    private string nameKey;

    [ObservableProperty]
    private string folderName;

    [ObservableProperty]
    private string folderPath;

    [ObservableProperty]
    private bool isEnabled = false;

    [RelayCommand]
    public void Remove()
    {
        // TODO
    }
}
