using System;
using System.Diagnostics;
using System.Threading;

namespace Fischless.Threading;

public class FpsThread
{
    public const int _1Hz = 1000 / 1;
    public const int _5Hz = 1000 / 5;
    public const int _10Hz = 1000 / 10;
    public const int _15Hz = 1000 / 15;
    public const int _20Hz = 1000 / 20;
    public const int _24Hz = 1000 / 24;
    public const int _30Hz = 1000 / 30;
    public const int _60Hz = 1000 / 60;
    public const int _90Hz = 1000 / 90;
    public const int _144Hz = 1000 / 144;

    protected Thread thread = null!;
    protected ThreadStart cyclicMethod = null!;
    private readonly Stopwatch stopwatch = null!;

    public string Name { get; protected set; } = null!;

    public bool IsRunning { get; set; } = false;
    public bool IsPaused { get; set; } = false;

    protected int interval = default;
    public int Interval
    {
        get => interval;
        set
        {
            if (interval != value)
            {
                interval = value;
            }
        }
    }

    protected int intervalOriginal = default;
    public int IntervalOriginal
    {
        get => intervalOriginal;
        protected set => intervalOriginal = value;
    }

    public FpsThread(ThreadStart cyclicMethod, int interval = _30Hz, string? name = null)
    {
        this.interval = interval;
        this.cyclicMethod = cyclicMethod;
        Name = name ?? $"FpsThreadOf{name}";
        thread = NewThread();
        stopwatch = new Stopwatch();
    }

    private Thread NewThread()
    {
        return new Thread(ThreadStart)
        {
            IsBackground = true,
            Name = Name,
        };
    }

    public void Start()
    {
        if (!IsRunning)
        {
            thread = NewThread();
        }
        IsRunning = true;
        IntervalOriginal = Interval;
        thread.Start();
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Pause() => IsPaused = true;
    public void Resume() => IsPaused = false;

    public void ThreadStart()
    {
        while (IsRunning)
        {
            if (!IsPaused)
            {
                stopwatch.Restart();
                cyclicMethod?.Invoke();
                stopwatch.Stop();
            }
            Thread.Sleep(Math.Max(default, interval - (int)stopwatch.ElapsedMilliseconds));
        }
    }
}
