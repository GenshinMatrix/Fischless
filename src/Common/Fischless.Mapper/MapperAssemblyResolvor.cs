using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Fischless.Mapper;

public static class MapperAssemblyResolver
{
    private static readonly HashSet<Assembly> stock = [];

    public static void Register(Assembly assembly = null!)
    {
        if (assembly == null)
        {
            StackTrace stackTrace = new();
            MethodBase methodName = stackTrace.GetFrame(1)?.GetMethod()!;

            if (methodName != null)
            {
                assembly = methodName.DeclaringType.Assembly;
            }
        }

        if (!stock.Contains(assembly))
        {
            _ = stock.Add(assembly);
        }
        Debug.WriteLine($"[MapperAssemblyResolver] Register assembly named `{assembly}`.");
    }

    public static void Unregister(Assembly assembly = null!)
    {
        if (assembly == null)
        {
            StackTrace stackTrace = new();
            MethodBase methodName = stackTrace.GetFrame(1)?.GetMethod()!;

            if (methodName != null)
            {
                assembly = methodName.DeclaringType.Assembly;
            }
        }

        if (!stock.Contains(assembly))
        {
            _ = stock.Add(assembly);
        }
        Debug.WriteLine($"[MapperAssemblyResolver] Unregister assembly named `{assembly}`.");
    }

    public static IEnumerable<Assembly> Resolve()
    {
        return stock;
    }
}
