using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Fischless.Configuration;
using Fischless.Fetch.Muter;
using Fischless.Models;
using Fischless.Models.Message;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace Fischless.ViewModels;

public partial class NotifyIconViewModel : ObservableObject
{
    public NotifyIconViewModel()
    {
        WeakReferenceMessenger.Default.Register<AutoMuteChangedMessage>(this, (_, _) => OnAutoMuteChangedReceived());
    }

    [RelayCommand]
    public static void ShowOrHide()
    {
        if (Application.Current.MainWindow.Visibility == Visibility.Visible)
        {
            Application.Current.MainWindow.Hide();
        }
        else
        {
            Application.Current.MainWindow.Activate();
            Application.Current.MainWindow.Focus();
            Application.Current.MainWindow.Show();
        }
    }

    [RelayCommand]
    public static void Restart()
    {
        try
        {
            _ = Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = Environment.ProcessPath,
                WorkingDirectory = Environment.CurrentDirectory,
            });
        }
        catch (Win32Exception)
        {
            return;
        }
        Application.Current.Shutdown();
        Environment.Exit('r' + 'e' + 's' + 't' + 'a' + 'r' + 't');
    }

    [RelayCommand]
    public static void Exit()
    {
        Application.Current.Shutdown();
    }

    [ObservableProperty]
    private bool autoMute = Configurations.AutoMute.Get();
    partial void OnAutoMuteChanged(bool value)
    {
        MuteManager.AutoMute = value;
        Configurations.AutoMute.Set(value);
        ConfigurationManager.Save();
        WeakReferenceMessenger.Default.Send(new AutoMuteChangedMessage());
    }

    [SuppressMessage("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "MVVMTK0034:")]
    private void OnAutoMuteChangedReceived()
    {
        autoMute = Configurations.AutoMute;
        OnPropertyChanged(nameof(AutoMute));
    }
}
