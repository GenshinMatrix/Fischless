using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Models;
using Fischless.Mvvm;
using System;

namespace Fischless.ViewModels;

public partial class PageReShadeViewModel : ObservableRecipient, IDisposable
{
    [ObservableProperty]
    private ObservableCollectionEx<ReShadeAvatar> avatars = new();

    public PageReShadeViewModel()
    {
        avatars.Reset(ReShadeAvatars.Avatars);
    }

    public void Dispose()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }
}
