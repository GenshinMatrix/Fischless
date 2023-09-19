using Fischless.Extensions;
using Fischless.Hosting.Absraction;
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
                .AddPlugins(app)
                .AddSingleton<INavigationService, NavigationService>()
                .AddTransient<PageHomeViewModel>()
                .AddSingleton<PageHome>()
                .AddTransient<PageSettingsViewModel>()
                .AddTransient<PageSettings>()
                .AddTransient<PageReShadeViewModel>()
                .AddTransient<PageReShade>()
                .AddTransient<IAutoStartService, AutoStartRegistyService>();
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
           .Forget();

        Log.Information($"Welcome to Fischless v{AppConfig.AppVersion}|System: {Environment.OSVersion}|Command Line: {Environment.CommandLine}");

        if (env.IsDevelopment())
        {
        }
    }
}
