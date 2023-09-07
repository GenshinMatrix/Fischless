using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Diagnostics;
using System;
using System.Windows;

namespace Fischless.ViewModels;

public partial class NotifyIconViewModel : ObservableObject
{
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
}
