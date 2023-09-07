using System;
using System.Collections.Concurrent;

namespace Fischless.Configuration;

public sealed class ConfigurationCache : ConcurrentDictionary<string, object?>
{
    public T Get<T>(ConfigurationDefinition<T> definition)
    {
        string key = definition.Name;
        T defaultValue = definition.DefaultValue;
        Func<object, T>? converter = definition.Converter;

        if (!TryGetValue(key, out object? value))
        {
            this[key] = defaultValue;
            return defaultValue;
        }
        else
        {
            if (value is T tValue)
            {
                return tValue;
            }
            else
            {
                if (converter is null)
                {
                    return (T)value!;
                }
                else
                {
                    return converter.Invoke(value!);
                }
            }
        }
    }

    public void Set<T>(ConfigurationDefinition<T> definition, object? value)
    {
        string key = definition.Name;
        this[key] = value;
    }
}
