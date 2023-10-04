using Fischless.Design.Themes;
using Fischless.Extensions;
using Fischless.Hosting.Absraction;
using Fischless.KeyboardCapture;
using Fischless.Services;
using Fischless.ViewModels;
using Fischless.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Fischless;

[SuppressMessage("Performance", "CA1822:")]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        IHost app = new App()
            .UseDispatcherUnhandledExceptionCatched()
            .UseMuiLanguage();

        services.AddSingleton(app)
                .AddSingleton(Log.Logger)
                .AddLogging(c => c.AddSerilog(Log.Logger))
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
        app.UseLogger()
           .UseConfiguration()
           .UseElevated()
           .UseSingleInstance("Fischless")
           .UseMapper()
           .UseAppCenter()
           .UseDpiAware()
           .UseDomainUnhandledExceptionCatched()
           .UseUnobservedTaskExceptionCatched()
           .UseTheme()
           .Forget();

        Log.Information($"Welcome to Fischless v{AppConfig.AppVersion}|System: {Environment.OSVersion}|Command Line: {Environment.CommandLine}");

        if (env.IsDevelopment())
        {
            ///
        }
    }
}
