using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Fetch.Launch;
using Fischless.Fetch.ReShade;
using Fischless.Logging;
using Fischless.Models;
using Fischless.Models.Message;
using Fischless.Mvvm;
using Fischless.Threading;
using Fischless.Views;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DragDrop = GongSolutions.Wpf.DragDrop.DragDrop;

namespace Fischless.ViewModels;

public partial class PageHomeViewModel : ObservableRecipient, IDisposable, IDropTarget
{
    [ObservableProperty]
    private ContactViewModel selectedItem = null!;

    [ObservableProperty]
    private ObservableCollectionEx<ContactViewModel> contacts = [];

    [ObservableProperty]
    private CancellationTokenSource cancelLaunchGameDelayTokenSource = null!;

    public PageHomeViewModel()
    {
        WeakReferenceMessenger.Default.Register<ContactMessage>(this, (sender, msg) =>
        {
            if (msg.Type == ContactMessageType.Added)
            {
                Contacts.Add(new ContactViewModel(msg.Contact));
            }
            else if (msg.Type == ContactMessageType.Edited
                  || msg.Type == ContactMessageType.Removed)
            {
                Refresh();
            }
        });
        WeakReferenceMessenger.Default.Register<ForeverTickServiceMessage>(this, (sender, msg) =>
        {
            if (msg.Method == ForeverTickMethod.CheckLaunch)
            {
                if (msg.Params is (string region, string runningProd))
                {
                    UIDispatcherHelper.Invoke(() =>
                    {
                        foreach (ContactViewModel contact in Contacts)
                        {
                            if (contact.Contact.Prod != null && runningProd != null &&
                                contact.Contact.Prod.Replace("\0", string.Empty) == runningProd.Replace("\0", string.Empty))
                            {
                                contact.IsRunning = true;
                            }
                            else
                            {
                                contact.IsRunning = false;
                            }
                        }
                    });
                }
                else
                {
                    UIDispatcherHelper.Invoke(() =>
                    {
                        foreach (ContactViewModel contact in Contacts)
                        {
                            contact.IsRunning = false;
                        }
                    });
                }
            }
        });
        Refresh();
    }

    public void Dispose()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    public void DragOver(IDropInfo dropInfo)
    {
        DragDrop.DefaultDropHandler.DragOver(dropInfo);
    }

    public void Drop(IDropInfo dropInfo)
    {
        DragDrop.DefaultDropHandler.Drop(dropInfo);

        if (dropInfo.Data is Contact contact && dropInfo.VisualTarget is ListView lv)
        {
            lv.SelectedItem = contact;
        }

        Configurations.Contacts.Set(Contacts.Select(vm => vm.Contact).ToArray());
        ConfigurationManager.Save();
    }

    [RelayCommand]
    public async Task AddContactAsync()
    {
        EditContactContentDialog contentDialog = new();
        ContactMessage message = await contentDialog.AddContactAsync();

        if (message != null)
        {
            Configurations.Contacts.Set(Configurations.Contacts.Get().Append(message.Contact).ToArray());
            ConfigurationManager.Save();
            WeakReferenceMessenger.Default.Send(message);
        }
    }

    [RelayCommand]
    public void Refresh()
    {
        Contacts.Clear();
        Contacts.Reset(Configurations.Contacts.Get().Select(v => new ContactViewModel(v)).ToArray());

        foreach (ContactViewModel contact in Contacts)
        {
            _ = contact.FetchAllAsync();
        }
    }

    [RelayCommand]
    public async Task SettingsAsync()
    {
        await new ContactSettingsContentDialog().ShowAsync();
    }

    [RelayCommand]
    public async Task LaunchGameFromListAsync()
    {
        if (SelectedItem == null)
        {
            Toast.Warning(Mui("LaunchGamePleaseSelectAccountToLaunch"));
            return;
        }
        CancelLaunchGameFromListWithDelay();
        await LaunchGameAsync(SelectedItem.Contact);
    }

    [RelayCommand]
    public async Task LaunchGameFromListWithDelayAsync()
    {
        if (SelectedItem == null)
        {
            Toast.Warning(Mui("LaunchGamePleaseSelectAccountToLaunch"));
            return;
        }
        await Task.Delay(180000, (CancelLaunchGameDelayTokenSource ??= new()).Token);
    }

    [RelayCommand]
    public void CancelLaunchGameFromListWithDelay()
    {
        CancelLaunchGameDelayTokenSource?.Cancel();
        CancelLaunchGameDelayTokenSource = null!;
    }

    [ObservableProperty]
    private bool isInstalleShade = !string.IsNullOrWhiteSpace(Configurations.ReShadePath.Get());

    [RelayCommand]
    public async Task LaunchGameFromListWithReShadeAsync()
    {
        if (SelectedItem == null)
        {
            Toast.Warning(Mui("LaunchGamePleaseSelectAccountToLaunch"));
            return;
        }

        Contact contact = SelectedItem.Contact;

        try
        {
            if (Directory.Exists(Configurations.ReShadePath.Get()))
            {
                await GILauncher.KillAsync(GIRelaunchMethod.Kill);
                await ReShadeLoader.LaunchAsync(Configurations.ReShadePath.Get(), Configurations.IsUseReShadeSlient.Get());
            }

            await GILauncher.LaunchAsync(delayMs: 1000, relaunchMethod: GIRelaunchMethod.Kill, launchParameter: new GILaunchParameter()
            {
                Server = contact.Server,
                Prod = contact.Prod,
                GamePath = Configurations.IsUseGamePath.Get() ? Configurations.GamePath.Get() : null,
                IsFullScreen = Configurations.IsUseResolution.Get() ? Configurations.IsUseFullScreen.Get() : null,
                IsBorderless = Configurations.IsUseResolution.Get() ? Configurations.IsUseBorderless.Get() : null,
                ScreenWidth = Configurations.IsUseResolution.Get() ? Configurations.ResolutionWidth.Get() : null,
                ScreenHeight = Configurations.IsUseResolution.Get() ? Configurations.ResolutionHeight.Get() : null,
                Fps = Configurations.IsUseFps.Get() ? Configurations.Fps.Get() : null,
            });
        }
        catch (Exception e)
        {
            Notification.AddNotice(string.Empty, e.Message);
        }
    }

    private static async Task LaunchGameAsync(Contact contact)
    {
        try
        {
            if (GILauncher.TryGetGamePath(Configurations.IsUseGamePath.Get() ? Configurations.GamePath.Get() : null, out string fileName))
            {
                FluentProcess netsh1 = FluentProcess.Create()
                    .FileName("netsh")
                    .Arguments(@$"advfirewall firewall add rule name=""DIS_GENSHIN_NETWORK"" dir=out action=block program=""{fileName}""")
                    .CreateNoWindow()
                    .UseShellExecute(false)
                    .Verb("runas")
                    .Start()
                    .WaitForExit();
            }

            if (Configurations.IsUseReShade.Get() && Directory.Exists(Configurations.ReShadePath.Get()))
            {
                await GILauncher.KillAsync(GIRelaunchMethod.Kill);
                await ReShadeLoader.LaunchAsync(Configurations.ReShadePath.Get(), Configurations.IsUseReShadeSlient.Get());
            }

            await GILauncher.LaunchAsync(delayMs: 1000, relaunchMethod: GIRelaunchMethod.Kill, launchParameter: new GILaunchParameter()
            {
                Server = contact.Server,
                Prod = contact.Prod,
                GamePath = Configurations.IsUseGamePath.Get() ? Configurations.GamePath.Get() : null,
                IsFullScreen = Configurations.IsUseResolution.Get() ? Configurations.IsUseFullScreen.Get() : null,
                IsBorderless = Configurations.IsUseResolution.Get() ? Configurations.IsUseBorderless.Get() : null,
                ScreenWidth = Configurations.IsUseResolution.Get() ? Configurations.ResolutionWidth.Get() : null,
                ScreenHeight = Configurations.IsUseResolution.Get() ? Configurations.ResolutionHeight.Get() : null,
                Fps = Configurations.IsUseFps.Get() ? Configurations.Fps.Get() : null,
            });

            // TODO: unlocker
            FluentProcess netsh2 = FluentProcess.Create()
                .FileName("netsh")
                .Arguments(@"advfirewall firewall delete rule name=""DIS_GENSHIN_NETWORK""")
                .CreateNoWindow()
                .UseShellExecute(false)
                .Verb("runas")
                .Start()
                .WaitForExit();
        }
        catch (Exception e)
        {
            Notification.AddNotice(string.Empty, e.Message);
        }
    }

    [RelayCommand]
    public async Task RefreshContact()
    {
        if (SelectedItem == null)
        {
            Toast.Warning(Mui("LaunchGamePleaseSelectAccountToRefresh"));
            return;
        }
        if (string.IsNullOrWhiteSpace(SelectedItem.Contact.Cookie))
        {
            Toast.Warning(Mui("LaunchGameSelectAccountNoCookie"));
            return;
        }
        if (!SelectedItem.Contact.IsUseCookie)
        {
            Toast.Warning(Mui("LaunchGameSelectAccountDisCookie"));
            return;
        }
        await SelectedItem.FetchAllAsync();
    }

    [RelayCommand]
    public void CopyUid()
    {
        if (SelectedItem == null)
        {
            Toast.Warning(Mui("LaunchGamePleaseSelectAccountToCopy"));
            return;
        }

        string uid = SelectedItem.Contact.Uid?.ToString();

        if (!string.IsNullOrWhiteSpace(uid))
        {
            try
            {
                Clipboard.SetText(uid);
                Toast.Information(Mui("LaunchGameUidCopied", uid));
            }
            catch (Exception e)
            {
                Log.Warning(e.ToString());
            }
        }
        else
        {
            Toast.Warning(Mui("LaunchGameSelectAccountNoCookie"));
            return;
        }
    }

    [RelayCommand]
    public void CopyCookie()
    {
        if (SelectedItem == null)
        {
            Toast.Warning(Mui("LaunchGamePleaseSelectAccountToCopy"));
            return;
        }

        string cookie = SelectedItem?.Contact.Cookie;

        if (!string.IsNullOrWhiteSpace(cookie))
        {
            try
            {
                Clipboard.SetText(cookie);
            }
            catch (Exception e)
            {
                Log.Warning(e.ToString());
            }
            Toast.Information(Mui("LaunchGameCookieCopied"));
        }
        else
        {
            Toast.Warning(Mui("LaunchGameSelectAccountNoCookie"));
            return;
        }
    }

    [RelayCommand]
    public async Task EditContact()
    {
        if (SelectedItem == null)
        {
            Toast.Warning(Mui("LaunchGamePleaseSelectAccountToEdit"));
            return;
        }

        EditContactContentDialog dialog = new();
        ContactMessage message = await dialog.EditContactAsync(SelectedItem.Contact);

        if (message != null)
        {
            await AddOrUpdateContactAsync(message);
        }
    }

    [RelayCommand]
    public async Task DeleteContact()
    {
        if (SelectedItem == null)
        {
            Toast.Warning(Mui("LaunchGamePleaseSelectAccountToDelete"));
            return;
        }

        if (MessageBoxX.Question(Mui("LaunchGameDeleteAccountHint", SelectedItem.Contact.AliasName), Mui("LaunchGameDeleteAccount")) == MessageBoxResult.Yes)
        {
            await AddOrUpdateContactAsync(new ContactMessage()
            {
                Type = ContactMessageType.Removed,
                Contact = SelectedItem.Contact,
            });
        }
    }

    private async Task AddOrUpdateContactAsync(ContactMessage message)
    {
        List<Contact> contacts = Configurations.Contacts.Get().ToList();

        if (message.Type == ContactMessageType.Added)
        {
            ContactViewModel vm = new(message.Contact);
            contacts.Add(message.Contact);
            Contacts.Add(vm);
            Toast.Success(Mui("LaunchGameAddAccountSuccessed", message.Contact.AliasName));
            if (!string.IsNullOrWhiteSpace(message.Contact.Cookie) && message.Contact.IsUseCookie)
            {
                await vm.FetchAllAsync();
            }
        }
        else if (message.Type == ContactMessageType.Edited)
        {
            if (contacts.Where(c => c.Guid == message.Contact.Guid).Any())
            {
                Contact contactToRemoved = contacts.Where(c => c.Guid == message.Contact.Guid).First();
                int indexToInsert = contacts.IndexOf(contactToRemoved);

                contacts.Remove(contactToRemoved);
                contacts.Insert(indexToInsert, message.Contact);
            }
            else
            {
                Log.Critical($"[AddOrUpdateContact] Lag of {message.Contact.Guid}");
                Debugger.Break();
                return;
            }
        }
        else if (message.Type == ContactMessageType.Removed)
        {
            contacts.Remove(contacts.Where(c => c.Guid == message.Contact.Guid).First());

            if (Contacts.Where(c => c.Contact.Guid == message.Contact.Guid).FirstOrDefault() is ContactViewModel contactToRemove)
            {
                Contacts.Remove(contactToRemove);
            }
        }

        Configurations.Contacts.Set(contacts);
        ConfigurationManager.Save();
        WeakReferenceMessenger.Default.Send(message);
    }
}
