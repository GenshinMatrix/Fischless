using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Fischless.Threading;

public static class TplExtensions
{
    [SuppressMessage("Style", "IDE0060:")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Forget(this Task? task)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void KeepAlive(this Task? task)
    {
        GC.KeepAlive(task);
    }

    [SuppressMessage("Style", "IDE0060:")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Forget(this ConfiguredTaskAwaitable? self)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void KeepAlive(this ConfiguredTaskAwaitable? self)
    {
        GC.KeepAlive(self);
    }

    public static Task<T> FaultedTask<T>(Exception ex)
    {
        return Task.FromException<T>(ex);
    }
}
