using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Design.Helpers;
using Fischless.Fetch.Datas.Core;
using Fischless.Fetch.Datas.Snap;
using Fischless.Fetch.Launch;
using Fischless.Fetch.ReShade;
using Fischless.Models;
using Fischless.Mvvm;
using MethodTimer;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Fischless.Fetch.ReShade.ReShadeSentimentalString;

namespace Fischless.ViewModels;

public partial class PageReShadeViewModel : ObservableRecipient, IDisposable
{
    [ObservableProperty]
    private IEnumerable<ReShadeAvatar> avatarStocks = null!;

    [ObservableProperty]
    private ObservableCollectionEx<ReShadeAvatar> avatars = new();

    [ObservableProperty]
    private IAsyncEnumerable<ReShadeFolder> folder = null!;

    [ObservableProperty]
    private ObservableCollectionEx<ReShadeFolderList> avatarList = new();

    [ObservableProperty]
    private Dictionary<string, List<ReShadeFolderList>> avatarListDict = new();

    [ObservableProperty]
    private ReShadeFolderList selectedAvatarList = null!;
    partial void OnSelectedAvatarListChanged(ReShadeFolderList value)
    {
        if (value == null)
        {
            // Empty avatar
            return;
        }

        IEnumerable<ReShadeFolderListDetailImage> imageDetails = 
            ReShadeFolderWalker.EnumerateFolderImage(value.FolderPath)
                               .Select(imagePath => new ReShadeFolderListDetailImage(imagePath));

        SelectedAvatarListDetail.Images.Reset(imageDetails);
    }

    [ObservableProperty]
    private ReShadeFolderListDetail selectedAvatarListDetail = new();

    [ObservableProperty]
    private bool isPyro = false;
    partial void OnIsPyroChanged(bool value) => SyncSearch();

    [ObservableProperty]
    private bool isHydro = false;
    partial void OnIsHydroChanged(bool value) => SyncSearch();

    [ObservableProperty]
    private bool isAnemo = false;
    partial void OnIsAnemoChanged(bool value) => SyncSearch();

    [ObservableProperty]
    private bool isElectro = false;
    partial void OnIsElectroChanged(bool value) => SyncSearch();

    [ObservableProperty]
    private bool isDendro = false;
    partial void OnIsDendroChanged(bool value) => SyncSearch();

    [ObservableProperty]
    private bool isIce = false;
    partial void OnIsIceChanged(bool value) => SyncSearch();

    [ObservableProperty]
    private bool isGeo = false;
    partial void OnIsGeoChanged(bool value) => SyncSearch();

    [ObservableProperty]
    private bool isFemale = false;
    partial void OnIsFemaleChanged(bool value) => SyncSearch();

    [ObservableProperty]
    private bool isMale = false;
    partial void OnIsMaleChanged(bool value) => SyncSearch();

    [Obsolete]
    [ObservableProperty]
    private bool isMaleFemale = false;

    public PageReShadeViewModel()
    {
        AvatarStocks = SnapCharacterProvider.GetSnapCharacters()
            .OrderByDescending(c => c.SortId)
            .Select((c, i) => new ReShadeAvatar()
            {
                Card = $"{PackSchemeHelper.ImagesPrefix}AvatarIcons/{c.Card}",
                FaceIcon = $"{PackSchemeHelper.ImagesPrefix}AvatarIcons/{c.FaceIcon}",
                RarityBackground = @$"{PackSchemeHelper.ImagesPrefix}Rarity_{c.Rarity}_Background.png",
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
                    Card = $"{PackSchemeHelper.ImagesPrefix}AvatarIcons/{o.Card}",
                    FaceIcon = $"{PackSchemeHelper.ImagesPrefix}AvatarIcons/{o.FaceIcon}",
                }),
                TextureOverride = c.TextureOverride,
                IsSelected = i == 0,
            })
            .Select(c =>
            {
                c.IsSelectedChanged -= OnIsSelectedChanged;
                c.IsSelectedChanged += OnIsSelectedChanged;
                return c;
            });
        Avatars.Reset(AvatarStocks);
        SyncFolderAsync();
    }

    private void OnIsSelectedChanged(object? sender, bool e)
    {
        if (sender is ReShadeAvatar avatar && e)
        {
            SyncAvatar(avatar.NameKey);
        }
    }

    public void Dispose()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    [Time]
    private async void SyncFolderAsync()
    {
        if (string.IsNullOrWhiteSpace(Configurations.ReShadePath.Get()))
        {
            return;
        }

        Folder = ReShadeFolderWalker.EnumerateFolder($"{Configurations.ReShadePath.Get()}/Mods");
        AvatarListDict.Clear();
        await foreach (ReShadeFolder folder in Folder)
        {
            var avatars = AvatarStocks.Where(avatar =>
            {
                if (!string.IsNullOrWhiteSpace(avatar.TextureOverride))
                {
                    foreach (string tex in avatar.TextureOverride.SplitTextureOverride())
                    {
                        if (folder.Inis.Any(ini => ini.TextureOverride?.StartsWith(tex, StringComparison.OrdinalIgnoreCase) ?? false))
                        {
                            return true;
                        }
                    }
                }
                return false;
            });

            if (avatars.Any())
            {
                var avatar = avatars.First();

                if (!AvatarListDict.ContainsKey(avatar.NameKey))
                {
                    AvatarListDict.Add(avatar.NameKey, new());
                }
                AvatarListDict[avatar.NameKey].Add(new ReShadeFolderList()
                {
                    NameKey = avatar.NameKey,
                    FolderName = folder.FolderName,
                    FolderPath = folder.FolderPath,
                    IsEnabled = folder.FolderPath.IsEnabledFolderPath(),
                });
            }
            else
            {
                const string nameKey = "AvatarNameOfAozi";

                if (!AvatarListDict.ContainsKey(nameKey))
                {
                    AvatarListDict.Add(nameKey, new());
                }
                AvatarListDict[nameKey].Add(new ReShadeFolderList()
                {
                    NameKey = nameKey,
                    FolderName = folder.FolderName,
                    FolderPath = folder.FolderPath,
                    IsEnabled = folder.FolderPath.IsEnabledFolderPath(),
                });
            }
        }

        AvatarList.Clear();
        SyncAvatar();
    }

    private void SyncAvatar(string? nameKey = null)
    {
        string currentNameKey = nameKey ?? Avatars.Where(avatar => avatar.IsSelected).First().NameKey;

        if (AvatarListDict.ContainsKey(currentNameKey))
        {
            if (AvatarList.FirstOrDefault()?.NameKey?.Equals(currentNameKey) ?? false)
            {
                return;
            }

            AvatarList.Reset(AvatarListDict[currentNameKey]);
        }
        else
        {
            AvatarList.Clear();
        }
        SelectedAvatarListDetail.Images.Clear();
    }

    private void SyncSearch()
    {
        if (!IsPyro && !IsHydro && !IsAnemo && !IsElectro && !IsDendro && !IsIce && !IsGeo && !IsMale && !IsFemale)
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

            if (avtar.IsVisible || (!IsPyro && !IsHydro && !IsAnemo && !IsElectro && !IsDendro && !IsIce && !IsGeo))
            {
                if (IsMale || IsFemale)
                {
                    avtar.IsVisible = (avtar.Gender == 1) && IsFemale
                                   || (avtar.Gender == 0) && IsMale;
                }
            }
        }
    }

    [Obsolete]
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

    [Obsolete]
    private void SyncSearchGender()
    {
        if (!IsMale && !IsFemale)
        {
            foreach (ReShadeAvatar avtar in Avatars)
            {
                avtar.IsVisible = true;
            }
            return;
        }

        foreach (ReShadeAvatar avtar in Avatars)
        {
            avtar.IsVisible = (avtar.Gender == 1) && IsFemale
                           || (avtar.Gender == 0) && IsMale
                           || (avtar.Gender != 0) && (avtar.Gender != 1);
        }
    }

    [RelayCommand]
    private void Settings()
    {
        CommonOpenFileDialog dialog = new()
        {
            IsFolderPicker = true,
            RestoreDirectory = true,
            InitialDirectory = Configurations.ReShadePath.Get(),
            DefaultDirectory = Configurations.ReShadePath.Get(),
            Title = "选择 3DMigoto 目录"
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string selectedDirectory = dialog.FileName;

            if (!File.Exists(Path.Combine(selectedDirectory, LoaderExe)))
            {
                Toast.Warning($"{LoaderExe} 不存在");
                MessageBoxX.Error("请选择 3DMigoto 目录");
                return;
            }
            
            ReShadeLoader.SetD3dxIniGameExe(Configurations.ReShadePath.Get(), () =>
            {
                OpenFileDialog dialog = new()
                {
                    Title = $"选择 {GILauncher.FileNameCN} 或 {GILauncher.FileNameOVERSEA}",
                    RestoreDirectory = true,
                    InitialDirectory = new FileInfo(Configurations.GamePath.Get()).DirectoryName,
                    FileName = Configurations.GamePath.Get(),
                    DefaultExt = "*.exe",
                    Filter = "*.exe|*.exe",
                };

                if (dialog.ShowDialog() ?? false)
                {
                    return dialog.FileName;
                }
                return null!;
            });

            Configurations.ReShadePath.Set(selectedDirectory);
            ConfigurationManager.Save();
            Toast.Success("设定成功");
            Refresh();
        }
    }

    [RelayCommand]
    private void Refresh()
    {
        SyncFolderAsync();
    }

    [RelayCommand]
    private async Task LaunchGameAsync()
    {
        try
        {
            await GILauncher.LaunchAsync(delayMs: 1000, relaunchMethod: GIRelaunchMethod.Kill);
        }
        catch (Exception e)
        {
            Notification.AddNotice(string.Empty, e.Message);
        }
    }

    [RelayCommand]
    private void LaunchLoader()
    {
        try
        {
            if (Directory.Exists(Configurations.ReShadePath.Get()))
            {
                ReShadeLoader.Launch(Configurations.ReShadePath.Get());
            }
            else
            {
                Settings();

                if (Directory.Exists(Configurations.ReShadePath.Get()))
                {
                    ReShadeLoader.Launch(Configurations.ReShadePath.Get());
                }
            }
        }
        catch (Exception e)
        {
            Notification.AddNotice(string.Empty, e.Message);
        }
    }
}
