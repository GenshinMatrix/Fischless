using System;
using System.Threading;
using System.Windows.Threading;

namespace Fischless.Threading;

public sealed class STAThread : STAThread<object>
{
    public STAThread(Action<STAThread<object>> start) : base(start)
    {
    }
}

public class STAThread<T> : STADispatcherObject, IDisposable where T : class
{
    public Thread Value { get; set; }
    public T Result { get; set; }

    public STAThread(Action<STAThread<T>> start)
    {
        Value = new(() =>
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
            start?.Invoke(this);
            Dispatcher.Run();
        })
        {
            IsBackground = true,
            Name = $"STAThread<{typeof(T)}>",
        };
        Value.SetApartmentState(ApartmentState.STA);
    }

    public void Start()
    {
        Value?.Start();
    }

    public void Forget()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
        {
            try
            {
                ((IDisposable?)Result)?.Dispose();
            }
            catch
            {
            }
        }
        Dispatcher?.InvokeShutdown();
#if false
        Value?.Abort();
#endif
    }
}

public class STADispatcherObject
{
    private Dispatcher dispatcher = null;

    public Dispatcher Dispatcher
    {
        get => dispatcher;
        set => dispatcher = value;
    }

    public STADispatcherObject(Dispatcher dispatcher = null)
    {
        this.dispatcher = dispatcher;
    }
}
