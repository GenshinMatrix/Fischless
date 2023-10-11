using Fischless.Fetch.Launch;
using Fischless.Fetch.Regedit;
using Fischless.Logging;
using NAudio.CoreAudioApi;
using System.Diagnostics;
using MMDevices = NAudio.CoreAudioApi.MMDeviceEnumerator;

namespace Fischless.Fetch.Muter;

#pragma warning disable CS0465

public class MuteManager
{
    private static bool autoMute = false;
    public static bool AutoMute
    {
        get => autoMute;
        set
        {
            autoMute = value;
            _ = MuteGameAsync(value);
            if (autoMute)
            {
                ForegroundWindowHelper.Initialize();
            }
            else
            {
                ForegroundWindowHelper.Uninitialize();
            }
        }
    }

    static MuteManager()
    {
        ForegroundWindowHelper.ForegroundWindowChanged += OnForegroundWindowChanged;
    }

    public static void Finalize()
    {
        ForegroundWindowHelper.ForegroundWindowChanged -= OnForegroundWindowChanged;
        ForegroundWindowHelper.Uninitialize();
    }

    private static async void OnForegroundWindowChanged(ForegroundWindowHelperEventArgs e)
    {
        if (!AutoMute)
        {
            return;
        }

        bool matchProcess = false;
        if (e.HWnd != IntPtr.Zero)
        {
            if (e.WindowTitle == GIRegeditKeys.CN || e.WindowTitle == GIRegeditKeys.OVERSEA)
            {
                matchProcess = true;
            }
        }
        await MuteGameAsync(!matchProcess);
    }

    public static async Task MuteGameAsync(bool isMuted)
    {
        if (GILauncher.TryGetProcess(out Process? process))
        {
            await MuteProcessAsync(process!.Id, isMuted);
        };
    }

    private static async Task MuteProcessAsync(int pid, bool isMuted)
    {
        try
        {
            await Task.Run(() =>
            {
                MuteProcess(pid, isMuted);
            });
        }
        catch (Exception e)
        {
            Log.Warning(e.ToString());
        }
    }

    private static void MuteProcess(int pid, bool isMuted)
    {
        MMDevices audio = new();
        foreach (MMDevice device in audio.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
        {
            for (int i = default; i < device.AudioSessionManager.Sessions.Count; i++)
            {
                AudioSessionControl session = device.AudioSessionManager.Sessions[i];

                if (session.GetProcessID == pid)
                {
                    session.SimpleAudioVolume.Mute = isMuted;
                    break;
                }
            }
        }
    }
}
