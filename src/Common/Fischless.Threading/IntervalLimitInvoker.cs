using System;
using System.Diagnostics;

namespace Fischless.Threading;

public class IntervalLimitInvoker
{
    public DateTime DateTime { get; private set; } = DateTime.MinValue;
    public int IntervalMilliseconds { get; set; } = default;

    public IntervalLimitInvoker(int intervalMilliseconds = default)
    {
        IntervalMilliseconds = intervalMilliseconds;
    }

    public void Invoke(Action action)
    {
        DateTime now = DateTime.Now;
        if (now.Subtract(DateTime).TotalMilliseconds >= IntervalMilliseconds)
        {
            DateTime = now;
            action?.Invoke();
        }
        else
        {
            Debug.WriteLine($"[IntervalLimitInvoker] Action was limitted");
        }
    }

    public T Invoke<T>(Func<T> func) where T : class
    {
        DateTime now = DateTime.Now;
        if (now.Subtract(DateTime).TotalMilliseconds >= IntervalMilliseconds)
        {
            DateTime = now;
            return func?.Invoke();
        }
        else
        {
            Debug.WriteLine($"[IntervalLimitInvoker] Func<T> was limitted");
        }
        return default;
    }
}
