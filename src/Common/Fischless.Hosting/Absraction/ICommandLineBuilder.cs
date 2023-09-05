using System;
using System.Collections.Specialized;

namespace Fischless.Hosting.Absraction;

public interface ICommandLineBuilder
{
    public StringDictionary Parameters { get; init; }

    public bool Has(string key) => Parameters.ContainsKey(key);

    public bool? GetValueBoolean(string key)
    {
        bool? ret = null;

        try
        {
            string? value = Parameters[key];

            if (!string.IsNullOrEmpty(value))
            {
                ret = Convert.ToBoolean(value);
            }
        }
        catch
        {
        }
        return ret;
    }

    public int? GetValueInt32(string key)
    {
        int? ret = null;

        try
        {
            string? value = Parameters[key];

            if (!string.IsNullOrEmpty(value))
            {
                ret = Convert.ToInt32(value);
            }
        }
        catch
        {
        }
        return ret;
    }

    public double? GetValueDouble(string key)
    {
        double? ret = null;

        try
        {
            string? value = Parameters[key];

            if (!string.IsNullOrEmpty(value))
            {
                ret = Convert.ToDouble(value);
            }
        }
        catch
        {
        }
        return ret;
    }

    public bool IsValueBoolean(string key)
    {
        return GetValueBoolean(key) ?? false;
    }

    public string? this[string key] => Parameters[key];
}
