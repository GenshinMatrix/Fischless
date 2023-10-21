using Fischless.Extensions;
using Fischless.Hosting.Absraction;
using Fischless.InputSimulator;
using Fischless.KeyboardCapture;
using Fischless.Logging;
using Fischless.Services;
using Fischless.ViewModels;
using Fischless.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Fischless;

[SuppressMessage("Performance", "CA1822:")]
public class Startup
{
    public void ConfigureServices(IApplicationBuilder app, IServiceCollection services)
    {
        app.UseConfiguration()
           .UseElevated()
           .UseSingleInstance("Fischless");

        Log.Logger = LoggerConfiguration.CreateDefault()
            .UseType(LoggerType.Async)
            .UseLevel(LogLevel.Trace)
            .WriteToFile(
                logFolder: AppConfig.LogFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Fischless\log"),
                fileName: $"Fischless_{DateTime.Now:yyyyMMdd}.log"
            )
            .CreateLogger();

        IHost host = new App()
            .UseDispatcherUnhandledExceptionCatched()
            .UseMuiLanguage();

        services.AddSingleton(host)
                .AddLogging(c => c.AddLogger(Log.Logger))
                .AddSingleton(KeyboardReader.Default)
                .AddPlugins(host)
                .AddSingleton<IInputSimulator, InputSimulator.InputSimulator>()
                .AddSingleton<INavigationService, NavigationService>()
                .AddTransient<IAutoStartService, AutoStartRegistyService>()
                .AddSingleton<IForeverTickService, ForeverTickService>()
                .AddTransient<PageHomeViewModel>()
                .AddSingleton<PageHome>()
                .AddTransient<PageSettingsViewModel>()
                .AddTransient<PageSettings>()
                .AddSingleton<PageReShadeViewModel>()
                .AddSingleton<PageReShade>()
                .AddSingleton<PageModelViewerViewModel>()
                .AddSingleton<PageModelViewer>();
    }

    public void Configure(IApplicationBuilder app, IWpfHostEnvironment env, IServiceCollection services)
    {
        app.UseMapper()
           .UseAppCenter()
           .UseDpiAware()
           .UseDomainUnhandledExceptionCatched()
           .UseUnobservedTaskExceptionCatched()
           .UseTheme()
           .UseProtocol()
           .Forget();

        Log.Information($"Welcome to Fischless v{AppConfig.AppVersion}|System: {Environment.OSVersion}|Command Line: {Environment.CommandLine}");

        if (env.IsDevelopment())
        {
            ///
        }
    }
}
