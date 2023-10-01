using CommunityToolkit.Mvvm.Messaging;
using Fischless.Fetch.Launch;
using Fischless.Fetch.Regedit;
using Fischless.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Fischless.Services;

internal class ForeverTickService : IForeverTickService
{
    public Task Task { get; private set; }
    public CancellationTokenSource tokenSource = null!;

    public ForeverTickService()
    {
    }

    public void Start()
    {
        Stop();
        Task = Task.Factory.StartNew(ForeverTickAsync, TaskCreationOptions.LongRunning);
        tokenSource = new();
    }

    public void Stop()
    {
        tokenSource?.Cancel();
        tokenSource = null!;
    }

    private async void ForeverTickAsync()
    {
        if (string.IsNullOrEmpty(Dispatcher.CurrentDispatcher.Thread.Name))
        {
            Dispatcher.CurrentDispatcher.Thread.Name = nameof(ForeverTickService);
        }
        while (!tokenSource.Token.IsCancellationRequested)
        {
            await ForeverCheckLaunchAsync();
            await Task.Delay(3000);
        }
    }

    private static async Task ForeverCheckLaunchAsync()
    {
        await Task.CompletedTask;
        if (Configurations.IsUseGameRunningHint)
        {
            if (GILauncher.TryGetProcessRegion(out string region))
            {
                string runningProd = region switch
                {
                    GILauncher.RegionOVERSEA => GIRegedit.ProdOVERSEA,
                    GILauncher.RegionCN or _ => GIRegedit.ProdCN,
                };

                WeakReferenceMessenger.Default.Send(new ForeverTickServiceMessage()
                {
                    Method = ForeverTickMethod.CheckLaunch,
                    Params = (region, runningProd),
                });
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new ForeverTickServiceMessage()
                {
                    Method = ForeverTickMethod.CheckLaunch,
                    Params = null,
                });
            }
        }
        else
        {
            WeakReferenceMessenger.Default.Send(new ForeverTickServiceMessage()
            {
                Method = ForeverTickMethod.CheckLaunch,
                Params = null,
            });
        }
    }
}
