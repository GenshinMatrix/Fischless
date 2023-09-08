using Fischless.Designs.Controls;
using Fischless.Hosting.Absraction;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;

namespace Fischless;

public partial class App : Application, IHost
{
    public TaskbarIcon Taskbar { get; protected set; } = null!;
    public IServiceProvider Services { get; set; } = null!;

    public App()
    {
        Notification.ClearNotice();
        InitializeComponent();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Taskbar = (TaskbarIcon)FindResource("PART_Taskbar");
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Notification.ClearNotice();
        base.OnExit(e);
    }
}
