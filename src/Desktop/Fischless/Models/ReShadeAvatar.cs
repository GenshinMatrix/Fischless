using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Fetch.Datas.Core;
using Fischless.Fetch.ReShade;
using Fischless.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Fischless.Models;

[DebuggerDisplay("{Card}")]
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
public partial class ReShadeFolderList : ObservableObject
{
    [ObservableProperty]
    private string nameKey;

    [ObservableProperty]
    private string folderName;

    [ObservableProperty]
    private string folderPath;

    [ObservableProperty]
    private bool isEnabled = false;
    partial void OnIsEnabledChanged(bool value)
    {
        if (value && !FolderPath.IsEnabledFolderPath())
        {
            Enable();
        }
        else if (!value && FolderPath.IsEnabledFolderPath())
        {
            Disable();
        }
    }

    [RelayCommand]
    public void Enable()
    {
        DirectoryInfo directoryInfo = new(FolderPath);

        if (directoryInfo.Exists)
        {
            string newFolderName = directoryInfo.Name.RemoveDisabledPrefix();
            string newFolderPath = Path.Combine(directoryInfo.Parent.FullName, newFolderName);

            if (newFolderPath.Equals(directoryInfo.FullName, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            directoryInfo.MoveTo(newFolderPath);
            FolderPath = newFolderPath;
            FolderName = newFolderName;
        }
    }

    [RelayCommand]
    public void Disable()
    {
        DirectoryInfo directoryInfo = new(FolderPath);

        if (directoryInfo.Exists)
        {
            if (!directoryInfo.Name.IsDisabledFolderPath())
            {
                string newFolderName = $"{ReShadeSentimentalString.DISABLED}{directoryInfo.Name}";
                string newFolderPath = Path.Combine(directoryInfo.Parent.FullName, newFolderName);

                if (newFolderPath.Equals(directoryInfo.FullName, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                directoryInfo.MoveTo(newFolderPath);
                FolderPath = newFolderPath;
                FolderName = newFolderName;
            }
        }
    }

    [RelayCommand]
    public void Remove()
    {
        // TODO
    }
}

[DebuggerDisplay("{Images?.Count}")]
public partial class ReShadeFolderListDetail : ObservableObject
{
    [ObservableProperty]
    private ObservableCollectionEx<ReShadeFolderListDetailImage> images = new();
}

[DebuggerDisplay("{ImagePath}")]
public partial class ReShadeFolderListDetailImage : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ImageFileProtocol))]
    private string imagePath = null!;
    public string ImageFileProtocol => $"file:///{ImagePath.Replace(@"\", "/")}";

    public ReShadeFolderListDetailImage()
    {
    }

    public ReShadeFolderListDetailImage(string imagePath)
    {
        ImagePath = imagePath;
    }
}
