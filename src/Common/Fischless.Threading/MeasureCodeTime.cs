using System;
using System.Diagnostics;

namespace Fischless.Threading;

public static class MeasureCodeTime
{
    public static long Execute(Action action)
    {
        Stopwatch stopwatch = new();

        stopwatch.Start();
        action?.Invoke();
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static T Execute<T>(Func<T> action, out long elapsedMilliseconds)
    {
        Stopwatch stopwatch = new();

        stopwatch.Start();
        T result = action.Invoke();
        stopwatch.Stop();
        elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        return result;
    }
}
