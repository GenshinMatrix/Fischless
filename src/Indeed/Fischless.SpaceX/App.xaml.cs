using PicaPico;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using ToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem;

namespace Fischless.SpaceX;

public partial class App : Application
{
    private NotifyIcon? notifyIcon;

    public App()
    {
        InitializeComponent();
        AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        SetupNotify();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        string? appName = Assembly.GetExecutingAssembly().GetName().Name;
        Mutex? mutex = new(true, appName, out bool createdNew);
        if (!createdNew)
        {
            Current.Shutdown();
            return;
        }
        GC.KeepAlive(mutex);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        notifyIcon?.Dispose();
        Current.Shutdown();
    }

    private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
    }

    private void SetupNotify()
    {
        notifyIcon = new NotifyIcon()
        {
            Text = Assembly.GetEntryAssembly()?.GetName().Name,
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule?.FileName!)
        };
        ToolStripMenuItem exitItem = new("Exit", null, (_, _) => Current.Shutdown())
        {
            Margin = new System.Windows.Forms.Padding(0, 0, 0, 2)
        };
        notifyIcon.AddMenu([exitItem]);
    }
}
