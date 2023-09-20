using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Configuration;
using Fischless.Design.Controls;
using Fischless.Fetch.Launch;
using Fischless.Models;
using Fischless.Models.Message;
using Fischless.Mvvm;
using Fischless.Views;
using GongSolutions.Wpf.DragDrop;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DragDrop = GongSolutions.Wpf.DragDrop.DragDrop;

namespace Fischless.ViewModels;

public partial class PageHomeViewModel : ObservableRecipient, IDisposable, IDropTarget
{
    [ObservableProperty]
    private ContactViewModel selectedItem = null!;

    [ObservableProperty]
    private ObservableCollectionEx<ContactViewModel> contacts = new();

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
            Toast.Warning($"请选择要启动的账号");
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
            Toast.Warning("请选择要启动的账号");
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

    private static async Task LaunchGameAsync(Contact contact)
    {
        try
        {
            await GILauncher.LaunchAsync(delayMs: 1000, relaunchMethod: GIRelaunchMethod.Kill, launchParameter: new GILaunchParameter()
            {
                Server = contact.Server,
                Prod = contact.Prod,
                IsFullScreen = Configurations.IsUseResolution.Get() ? Configurations.IsUseFullScreen.Get() : null,
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

    [RelayCommand]
    public async Task RefreshContact()
    {
        if (SelectedItem == null)
        {
            Toast.Warning($"请选择要刷新的账号");
            return;
        }
        if (string.IsNullOrWhiteSpace(SelectedItem.Contact.Cookie))
        {
            Toast.Warning($"选中账号无 Cookie 信息");
            return;
        }
        if (!SelectedItem.Contact.IsUseCookie)
        {
            Toast.Warning($"选中账号未启用 Cookie 使用");
            return;
        }
        await SelectedItem.FetchAllAsync();
    }

    [RelayCommand]
    public void CopyUid()
    {
        if (SelectedItem == null)
        {
            Toast.Warning("请选择要复制的账号");
            return;
        }

        string uid = SelectedItem.Contact.Uid?.ToString();

        if (!string.IsNullOrWhiteSpace(uid))
        {
            try
            {
                Clipboard.SetText(uid);
                Toast.Information($"角色UID:{uid}已复制到剪贴板");
            }
            catch (Exception e)
            {
                Log.Warning(e.ToString());
            }
        }
        else
        {
            Toast.Warning("选中账号无 Cookie 信息");
            return;
        }
    }

    [RelayCommand]
    public void CopyCookie()
    {
        if (SelectedItem == null)
        {
            Toast.Warning($"请选择要复制的账号");
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
            Toast.Information($"Cookie 已复制到剪贴板");
        }
        else
        {
            Toast.Warning($"选中账号无 Cookie 信息");
            return;
        }
    }

    [RelayCommand]
    public async Task EditContact()
    {
        if (SelectedItem == null)
        {
            Toast.Warning($"请选择要编辑的账号");
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
            Toast.Warning("请选择要删除的账号");
            return;
        }

        if (MessageBoxX.Question($"是否确定要删除账号“{SelectedItem.Contact.AliasName}”？", "删除账号") == MessageBoxResult.Yes)
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
            Toast.Success($"添加 {message.Contact.AliasName} 成功");
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
                Log.Fatal($"[AddOrUpdateContact] Lag of {message.Contact.Guid}");
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
