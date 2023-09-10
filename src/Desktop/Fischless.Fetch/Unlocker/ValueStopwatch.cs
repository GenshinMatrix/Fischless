using System.Diagnostics;

namespace Fischless.Fetch.Unlocker;

internal readonly struct ValueStopwatch
{
    private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;

    private readonly long startTimestamp;

    private ValueStopwatch(long startTimestamp)
    {
        this.startTimestamp = startTimestamp;
    }

    public bool IsActive => startTimestamp != 0;

    public static ValueStopwatch StartNew()
    {
        return new(Stopwatch.GetTimestamp());
    }

    public long GetElapsedTimestamp()
    {
        // Start timestamp can't be zero in an initialized ValueStopwatch.
        // It would have to be literally the first thing executed when the machine boots to be 0.
        // So it being 0 is a clear indication of default(ValueStopwatch)
        if (!IsActive)
        {
            throw new InvalidOperationException($"An uninitialized, or 'default', {nameof(ValueStopwatch)} cannot be used to get elapsed time.");
        }

        long end = Stopwatch.GetTimestamp();
        long timestampDelta = end - startTimestamp;
        long ticks = (long)(TimestampToTicks * timestampDelta);

        return ticks;
    }

    public TimeSpan GetElapsedTime()
    {
        return new TimeSpan(GetElapsedTimestamp());
    }
}
