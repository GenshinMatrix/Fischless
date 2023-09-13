using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Configuration;
using Fischless.Models;
using Fischless.Models.Message;
using Fischless.Mvvm;
using Fischless.Views;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DragDrop = GongSolutions.Wpf.DragDrop.DragDrop;

namespace Fischless.ViewModels;

public partial class PageHomeViewModel : ObservableRecipient, IDisposable, IDropTarget
{
    [ObservableProperty]
    private ObservableCollectionEx<Contact> contacts = new();

    public PageHomeViewModel()
    {
        WeakReferenceMessenger.Default.Register<ContactMessage>(this, (sender, msg) =>
        {
            if (msg.Type == ContactMessageType.Added)
            {
                Contacts.Add(msg.Contact);
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

        Configurations.Contacts.Set(Contacts.ToArray());
        ConfigurationManager.Save();
    }

    [RelayCommand]
    public async Task AddContactAsync()
    {
        ContactContentDialog contentDialog = new();
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
        Contacts.Reset(Configurations.Contacts.Get().ToArray());
    }

    [RelayCommand]
    public void LaunchGameFromList()
    {
    }

    [RelayCommand]
    public void LaunchGameFromListWithDelay()
    {
    }

    [RelayCommand]
    public void RefreshContact()
    {
    }

    [RelayCommand]
    public void CopyUid()
    {
    }

    [RelayCommand]
    public void CopyCookie()
    {
    }

    [RelayCommand]
    public void EditContact()
    {
    }

    [RelayCommand]
    public void DeleteContact()
    {
    }

    public void OnContextMenuOpened(object sender, RoutedEventArgs e)
    {
    }
}
