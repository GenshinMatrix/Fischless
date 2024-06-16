﻿using MicaSetup.Design.ComponentModel;
using MicaSetup.Design.Controls;
using MicaSetup.Helper;
using MicaSetup.Helper.Helper;
using MicaSetup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace MicaSetup.ViewModels;

#pragma warning disable CS0162

public partial class InstallViewModel : ObservableObject
{
    public string Message => Option.Current.MessageOfPage2;

    [ObservableProperty]
    private string installInfo = string.Empty;

    [ObservableProperty]
    private double installProgress = 0d;

    public InstallViewModel()
    {
        Option.Current.Installing = true;
        InstallInfo = Mui("Preparing");

        _ = Task.Run(async () =>
        {
            await Task.Delay(200).ConfigureAwait(true);

            try
            {
                using Stream archiveStream = ResourceHelper.GetStream("pack://application:,,,/MicaSetup;component/Resources/Setups/publish.7z");
                InstallInfo = Mui("ProgressTipsInstalling");
                InstallHelper.Install(archiveStream, (progress, key) =>
                {
                    UIDispatcherHelper.BeginInvoke(() =>
                    {
                        InstallProgress = progress * 100d;
                        InstallInfo = key;
                    });
                });

                using Stream uninstStream = ResourceHelper.GetStream("pack://application:,,,/MicaSetup;component/Resources/Setups/Uninst.exe");
                InstallHelper.CreateUninst(uninstStream);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            if (Option.Current.IsAllowFullFolderSecurity)
            {
                try
                {
                    SecurityControlHelper.AllowFullFolderSecurity(Option.Current.InstallLocation);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            if (Option.Current.IsAllowFirewall)
            {
                try
                {
                    FirewallHelper.AllowApplication(Path.Combine(Option.Current.InstallLocation, Option.Current.ExeName));
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            if (false)
            {
                try
                {
                    IDotNetVersionService dotNetService = ServiceManager.GetService<IDotNetVersionService>();
                    DotNetInstallInfo info = dotNetService.GetInfo(new Version(4, 8));

                    try
                    {
                        if (dotNetService.GetNetFrameworkVersion() < info.Version)
                        {
                            InstallInfo = $"{Mui("Preparing")} {info.Name}";
                            if (!dotNetService.InstallNetFramework(info.Version, (t, e) =>
                            {
                                UIDispatcherHelper.BeginInvoke(() =>
                                {
                                    InstallInfo = $"{t switch { ProgressType.Download => Mui("Downloading"), _ or ProgressType.Install => Mui("Installing") }} {info.Name}";
                                    InstallProgress = e.ProgressPercentage;
                                });
                            }))
                            {
                                UIDispatcherHelper.BeginInvoke(() =>
                                {
                                    _ = MessageBoxX.Info(null!, Mui("ComponentInstallFailedTips", info.Name));
                                    _ = FluentProcess.Start("explorer.exe", info.ThankYouUrl);
                                });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                        UIDispatcherHelper.BeginInvoke(() =>
                        {
                            _ = MessageBoxX.Info(null!, Mui("ComponentInstallFailedTips", info.Name) + Environment.NewLine + e.Message);
                            _ = FluentProcess.Start("explorer.exe", info.ThankYouUrl);
                        });
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            InstallInfo = Mui("InstallFinishTips");
            Option.Current.Installing = false;
            await Task.Delay(200).ConfigureAwait(false);

            if (Option.Current.IsRefreshExplorer)
            {
                try
                {
                    ServiceManager.GetService<IExplorerService>()?.Refresh();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
            UIDispatcherHelper.Invoke(Routing.GoToNext);
        });
    }
}

partial class InstallViewModel
{
    public string InstallInfo
    {
        get => installInfo;
        set
        {
            if (!EqualityComparer<string>.Default.Equals(installInfo, value))
            {
                OnInstallInfoChanging(value);
                OnInstallInfoChanging(default, value);
                OnPropertyChanging(new PropertyChangingEventArgs("InstallInfo"));
                installInfo = value;
                OnInstallInfoChanged(value);
                OnInstallInfoChanged(default, value);
                OnPropertyChanged(new PropertyChangedEventArgs("InstallInfo"));
            }
        }
    }

    public double InstallProgress
    {
        get => installProgress;
        set
        {
            if (!EqualityComparer<double>.Default.Equals(installProgress, value))
            {
                OnInstallProgressChanging(value);
                OnInstallProgressChanging(default, value);
                OnPropertyChanging(new PropertyChangingEventArgs("InstallProgress"));
                installProgress = value;
                OnInstallProgressChanged(value);
                OnInstallProgressChanged(default, value);
                OnPropertyChanged(new PropertyChangedEventArgs("InstallProgress"));
            }
        }
    }

    partial void OnInstallInfoChanging(string value);

    partial void OnInstallInfoChanging(string? oldValue, string newValue);

    partial void OnInstallInfoChanged(string value);

    partial void OnInstallInfoChanged(string? oldValue, string newValue);

    partial void OnInstallProgressChanging(double value);

    partial void OnInstallProgressChanging(double oldValue, double newValue);

    partial void OnInstallProgressChanged(double value);

    partial void OnInstallProgressChanged(double oldValue, double newValue);
}
