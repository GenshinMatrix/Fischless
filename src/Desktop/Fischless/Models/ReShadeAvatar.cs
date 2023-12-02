using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Fetch.Datas.Core;
using Fischless.Fetch.ReShade;
using Fischless.Logging;
using Fischless.Mvvm;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;

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
    public event EventHandler<bool> Removed = null!;

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

                if (Directory.Exists(newFolderPath))
                {
                    Log.Warning($"[ReShadeAvatar] Can not disable '{newFolderPath}', cuz target folder already exists.");
                    return;
                }
                directoryInfo.MoveTo(newFolderPath);
                FolderPath = newFolderPath;
                FolderName = newFolderName;
            }
        }
    }

    [RelayCommand]
    public async Task OpenFolderAsync()
    {
        if (Directory.Exists(FolderPath))
        {
            await Launcher.LaunchUriAsync(new Uri($"file://{FolderPath.Replace(Path.DirectorySeparatorChar, '/')}"));
        }
    }

    public string FolderNameForEdit
    {
        get => FolderName;
        set
        {
            DirectoryInfo directoryInfo = new(FolderPath);

            if (directoryInfo.Exists)
            {
                string newFolderName = value ?? string.Empty;
                string newFolderPath = Path.Combine(directoryInfo.Parent.FullName, new string(newFolderName.Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray()));

                if (newFolderPath.Equals(directoryInfo.FullName, StringComparison.OrdinalIgnoreCase)
                 || newFolderPath.Length > 248)
                {
                    return;
                }
                directoryInfo.MoveTo(newFolderPath);
                FolderPath = newFolderPath;
                FolderName = newFolderName;
                IsEnabled = newFolderName.IsEnabledFolderPath();
            }
        }
    }

    [RelayCommand]
    public void RenameFolder()
    {
        ///
    }

    [ObservableProperty]
    private bool isRemoved = false;

    [RelayCommand]
    public void RemoveFolder()
    {
        if (Directory.Exists(FolderPath))
        {
            FileSystem.DeleteDirectory(FolderPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            Removed?.Invoke(this, IsRemoved = true);
        }
    }
}

[DebuggerDisplay("{Images?.Count}")]
public partial class ReShadeFolderListDetail : ObservableObject
{
    [ObservableProperty]
    private ObservableCollectionEx<ReShadeFolderListDetailImage> images = [];
}

[DebuggerDisplay("{ImagePath}")]
public partial class ReShadeFolderListDetailImage : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ImageFileProtocol))]
    private string imagePath = null!;

    public string ImageFileProtocol => $"file://{ImagePath.Replace(Path.DirectorySeparatorChar, '/')}";

    public ReShadeFolderListDetailImage()
    {
    }

    public ReShadeFolderListDetailImage(string imagePath)
    {
        ImagePath = imagePath;
    }
}
