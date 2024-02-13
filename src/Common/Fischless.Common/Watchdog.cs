using System.Diagnostics;

namespace Fischless.Common;

public class Watchdog
{
    public static Lazy<Watchdog> Instance { get; } = new();

    private const int MemoryThreshold = 100;
    private const int ResponseTimeThreshold = 5000;

    private readonly Stopwatch stopwatch;
    private readonly Process currentProcess;

    public Watchdog()
    {
        stopwatch = new Stopwatch();
        currentProcess = Process.GetCurrentProcess();
    }

    public void Start()
    {
        stopwatch.Start();
        TimerCallback timerCallback = new(CheckHealth);
        Timer timer = new(timerCallback, null, 0, 10000);
    }

    private void CheckHealth(object? state)
    {
        if (stopwatch.ElapsedMilliseconds > ResponseTimeThreshold)
        {
            Debug.WriteLine("[Watchdog] Response time exceeded threshold!");
        }

        float memoryUsage = currentProcess.WorkingSet64 / (float)long.MaxValue * 100f;
        if (memoryUsage > MemoryThreshold)
        {
            Debug.WriteLine("[Watchdog] Memory usage exceeded threshold!");
        }
    }
}
