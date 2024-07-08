using MicaSetup.Design.Controls;
using MicaSetup.Extension.DependencyInjection;
using MicaSetup.Services;
using MicaSetup.Views;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: Guid("00000000-0000-0000-0000-000000000000")]
[assembly: AssemblyTitle("Fischless Uninst")]
[assembly: AssemblyProduct("Fischless")]
[assembly: AssemblyDescription("Fischless Uninst")]
[assembly: AssemblyCompany("GenshinMatrix")]
[assembly: AssemblyCopyright("Under MIT License. Copyright (c) GenshinMatrix Contributors.")]
[assembly: AssemblyVersion("0.4.8.0")]
[assembly: AssemblyFileVersion("0.4.8.0")]

namespace MicaSetup;

internal class Program
{
    [STAThread]
    internal static void Main()
    {
        Hosting.CreateBuilder()
            .UseAsUninst()
            .UseLogger()
            .UseSingleInstance("FischlessMicaSetup")
            .UseTempPathFork()
            .UseElevated()
            .UseDpiAware()
            .UseOptions(option =>
            {
                option.IsCreateDesktopShortcut = true;
                option.IsCreateUninst = true;
                option.IsCreateRegistryKeys = true;
                option.IsCreateStartMenu = true;
                option.IsCreateQuickLaunch = false;
                option.IsCreateAsAutoRun = false;
                option.IsUseRegistryPreferX86 = null!;
                option.IsAllowFirewall = true;
                option.IsRefreshExplorer = false;
                option.IsInstallCertificate = false;
                option.AppName = "Fischless";
                option.KeyName = "Fischless";
                option.ExeName = "Fischless.exe";
                option.DisplayName = $"{option.AppName}";
                option.DisplayIcon = $"{option.ExeName}";
                option.DisplayVersion = "0.4.8.0";
                option.Publisher = "GenshinMatrix";
                option.SetupName = $"{option.AppName} {Mui("UninstallProgram")}";
                option.MessageOfPage1 = $"{option.AppName}";
                option.MessageOfPage2 = Mui("ProgressTipsUninstalling");
                option.MessageOfPage3 = Mui("UninstallFinishTips");
            })
            .UseServices(service =>
            {
                service.AddSingleton<IMuiLanguageService, MuiLanguageService>();
                service.AddScoped<IExplorerService, ExplorerService>();
            })
            .CreateApp()
            .UseMuiLanguage()
            .UseTheme(WindowsTheme.Dark)
            .UsePages(page =>
            {
                page.Add(nameof(MainPage), typeof(MainPage));
                page.Add(nameof(UninstallPage), typeof(UninstallPage));
                page.Add(nameof(FinishPage), typeof(FinishPage));
            })
            .UseDispatcherUnhandledExceptionCatched()
            .UseDomainUnhandledExceptionCatched()
            .UseUnobservedTaskExceptionCatched()
            .RunApp();
    }
}
