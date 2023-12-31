﻿using Fischless.Design.Controls;
using Fischless.Hosting.Absraction;
using Fischless.Models;
using Fischless.Services;
using Fischless.ViewModels;

namespace Fischless.Views;

public partial class MainWindow : WindowX
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
        if (Configurations.IsUseSmallerSize.Get())
        {
            Width = 640d;
            Height = 560d;
        }
        if (AppConfig.GetService<ICommandLineBuilder>() is ICommandLineBuilder cli)
        {
            if (cli.Parameters.ContainsKey(AppConfig.AutoStartCommand.TrimStart('-')))
            {
                WindowBacktray.Hide(this);
            }
        }
        Loaded += (_, _) =>
        {
            AppConfig.GetService<IForeverTickService>()?.Start();
        };
        Closing += (_, e) =>
        {
            if (Configurations.CloseToTray.Get())
            {
                e.Cancel = true;
                Hide();
            }
        };
        Closed += (_, _) =>
        {
            AppConfig.GetService<IForeverTickService>()?.Stop();
        };
    }
}
