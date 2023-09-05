using System;
using System.Threading;

namespace Fischless.Threading;

public static class SpinWaiter
{
    public const int WaitForever = -1;

    public static bool SpinUntil(Func<bool> condition, int? millisecondsTimeout = null!)
    {
        millisecondsTimeout ??= WaitForever;
        if (millisecondsTimeout < WaitForever)
        {
            millisecondsTimeout = WaitForever;
        }
        if (condition == null)
        {
            if (millisecondsTimeout == WaitForever || millisecondsTimeout > 0)
            {
                condition = () => false;
            }
            else
            {
                throw new ArgumentNullException(nameof(condition));
            }
        }
        return SpinWait.SpinUntil(condition, (int)millisecondsTimeout);
    }
}
