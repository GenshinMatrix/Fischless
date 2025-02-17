using MicaSetup.Design.Controls;
using MicaSetup.Extension.DependencyInjection;
using MicaSetup.Services;
using MicaSetup.Views;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: Guid("00000000-0000-0000-0000-000000000000")]
[assembly: AssemblyTitle("Fischless Setup")]
[assembly: AssemblyProduct("Fischless")]
[assembly: AssemblyDescription("Fischless Setup")]
[assembly: AssemblyCompany("GenshinMatrix")]
[assembly: AssemblyCopyright("Under MIT License. Copyright (c) GenshinMatrix Contributors.")]
[assembly: AssemblyVersion("0.4.12.0")]
[assembly: AssemblyFileVersion("0.4.12.0")]

namespace MicaSetup;

internal class Program
{
    [STAThread]
    internal static void Main()
    {
        Hosting.CreateBuilder()
            .UseLogger()
            .UseSingleInstance("FischlessMicaSetup")
            .UseTempPathFork()
            .UseElevated()
            .UseDpiAware()
            .UseOptions(option =>
            {
                option.IsCreateDesktopShortcut = true;
                option.IsCreateUninst = true;
                option.IsCreateStartMenu = true;
                option.IsCreateQuickLaunch = false;
                option.IsCreateRegistryKeys = true;
                option.IsCreateAsAutoRun = false;
                option.IsCustomizeVisiableAutoRun = false;
                option.AutoRunLaunchCommand = "-autostart";
                option.UseFolderPickerPreferClassic = false;
                option.UseInstallPathPreferX86 = false;
                option.IsUseRegistryPreferX86 = null!;
                option.IsAllowFullFolderSecurity = true;
                option.IsAllowFirewall = true;
                option.IsRefreshExplorer = false;
                option.IsInstallCertificate = false;
                option.OverlayInstallRemoveExt = "exe,dll,pdb";
                option.OverlayInstallRemoveHandler = null!;
                option.UnpackingPassword = null!;
                option.AppName = "Fischless";
                option.KeyName = "Fischless";
                option.ExeName = "Fischless.exe";
                option.DisplayName = $"{option.AppName}";
                option.DisplayIcon = $"{option.ExeName}";
                option.DisplayVersion = "0.4.12.0";
                option.Publisher = "GenshinMatrix";
                option.SetupName = $"{option.AppName} {Mui("Setup")}";
                option.MessageOfPage1 = $"{option.AppName}";
                option.MessageOfPage2 = Mui("Installing");
                option.MessageOfPage3 = Mui("InstallFinishTips");
            })
            .UseServices(service =>
            {
                service.AddSingleton<IMuiLanguageService, MuiLanguageService>();
                service.AddScoped<IDotNetVersionService, DotNetVersionService>();
                service.AddScoped<IExplorerService, ExplorerService>();
            })
            .CreateApp()
            .UseMuiLanguage()
            .UseTheme(WindowsTheme.Dark)
            .UsePages(page =>
            {
                page.Add(nameof(MainPage), typeof(MainPage));
                page.Add(nameof(InstallPage), typeof(InstallPage));
                page.Add(nameof(FinishPage), typeof(FinishPage));
            })
            .UseDispatcherUnhandledExceptionCatched()
            .UseDomainUnhandledExceptionCatched()
            .UseUnobservedTaskExceptionCatched()
            .RunApp();
    }
}
