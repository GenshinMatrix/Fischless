using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Fischless.Design.Controls;

[INotifyPropertyChanged]
public partial class SplashWindow : Window
{
    public Uri ImageUri { get; }

    [ObservableProperty]
    private string hint = null!;

    public bool AutoEnd { get; set; } = false;

    public SplashWindow(Uri imageUri)
    {
        ImageUri = imageUri;
        InitializeComponent();
    }

    private void Start_Completed(object sender, EventArgs e)
    {
        if (AutoEnd)
        {
            StartEnd();
        }
    }

    private void End_Completed(object sender, EventArgs e)
    {
        Shutdown();
    }

    public void StartEnd()
    {
        Dispatcher.Invoke(() =>
        {
            Storyboard storyboard = (Storyboard)FindResource("End");
            storyboard.Begin();
        });
    }

    public void Shutdown()
    {
        Dispatcher.Invoke(() =>
        {
            Close();
            Dispatcher?.InvokeShutdown();
        });
    }
}
