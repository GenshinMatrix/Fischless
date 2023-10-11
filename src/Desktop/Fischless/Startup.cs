using Fischless.Extensions;
using Fischless.Hosting.Absraction;
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
    public void ConfigureServices(IServiceCollection services)
    {
        Log.Logger = LoggerConfiguration.CreateDefault()
            .UseType(LoggerType.Async)
            .UseLevel(LogLevel.Trace)
            .WriteToFile(
                logFolder: AppConfig.LogFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Fischless\log"),
                fileName: $"Fischless_{DateTime.Now:yyyyMMdd}.log"
            )
            .CreateLogger();

        IHost app = new App()
            .UseDispatcherUnhandledExceptionCatched()
            .UseMuiLanguage();

        services.AddSingleton(app)
                .AddLogging(c => c.AddLogger(Log.Logger))
                .AddSingleton(KeyboardReader.Default)
                .AddPlugins(app)
                .AddSingleton<INavigationService, NavigationService>()
                .AddTransient<IAutoStartService, AutoStartRegistyService>()
                .AddSingleton<IForeverTickService, ForeverTickService>()
                .AddTransient<PageHomeViewModel>()
                .AddSingleton<PageHome>()
                .AddTransient<PageSettingsViewModel>()
                .AddTransient<PageSettings>()
                .AddTransient<PageReShadeViewModel>()
                .AddTransient<PageReShade>();
    }

    public void Configure(IApplicationBuilder app, IWpfHostEnvironment env, IServiceCollection services)
    {
        app.UseConfiguration()
           .UseElevated()
           .UseSingleInstance("Fischless")
           .UseMapper()
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
