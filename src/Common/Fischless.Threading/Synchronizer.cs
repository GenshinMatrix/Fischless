using System;
using System.Threading;

namespace Fischless.Threading;

public class Synchronizer<TImpl, TIRead, TIWrite> where TImpl : TIWrite, TIRead
{
    protected readonly ReaderWriterLockSlim _lock = new();
    protected readonly TImpl _shared;

    public Synchronizer(TImpl shared)
    {
        _shared = shared;
    }

    public void Read(Action<TIRead> functor)
    {
        _lock.EnterReadLock();
        try
        {
            functor(_shared);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void Write(Action<TIWrite> functor)
    {
        _lock.EnterWriteLock();
        try
        {
            functor(_shared);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
}
