using Fischless.Hosting.Absraction;
using System;
using System.Windows;

namespace Fischless;

public partial class App : Application, IHost
{
    public IServiceProvider Services { get; set; } = null!;

    public App()
    {
        InitializeComponent();
    }
}
