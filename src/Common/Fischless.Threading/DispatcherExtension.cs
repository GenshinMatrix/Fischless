using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Fischless.Threading;

public static class DispatcherExtension
{
    public static void DoEvents(this Dispatcher dispatcher)
    {
        DispatcherFrame frame = new();
        dispatcher.BeginInvoke(new Action<object>((obj) =>
        {
            if (obj is DispatcherFrame frm)
            {
                frm.Continue = false;
            }
        }), DispatcherPriority.Background, frame);
        Dispatcher.PushFrame(frame);
    }

    public static async Task WaitEvents(this Dispatcher dispatcher, int millisecondsDelay = default)
    {
        await Task.Delay(millisecondsDelay);
        await dispatcher.BeginInvoke(() => { });
    }
}
