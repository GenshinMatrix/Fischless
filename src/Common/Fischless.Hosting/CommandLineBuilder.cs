using Fischless.Hosting.Absraction;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Fischless.Hosting;

public class CommandLineBuilder : ICommandLineBuilder
{
    public StringDictionary Parameters { get; set; } = new();

    public CommandLineBuilder(string[]? args, bool skip1 = false)
    {
#pragma warning disable SYSLIB1045
        Regex spliter = new(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Regex remover = new(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
#pragma warning restore SYSLIB1045

        string param = null!;
        string[] parts;

        args ??= Array.Empty<string>();

        foreach (string txt in (skip1 ? args.Skip(1) : args))
        {
            parts = spliter.Split(txt, 3);

            switch (parts.Length)
            {
                case 1:
                    if (param != null)
                    {
                        if (!Parameters.ContainsKey(param))
                        {
                            parts[0] = remover.Replace(parts[0], "$1");

                            Parameters.Add(param, parts[0]);
                        }
                        param = null!;
                    }
                    break;

                case 2:
                    if (param != null)
                    {
                        if (!Parameters.ContainsKey(param))
                        {
                            Parameters.Add(param, "true");
                        }
                    }
                    param = parts[1];
                    break;

                case 3:
                    if (param != null)
                    {
                        if (!Parameters.ContainsKey(param))
                        {
                            Parameters.Add(param, "true");
                        }
                    }

                    param = parts[1];

                    if (!Parameters.ContainsKey(param))
                    {
                        parts[2] = remover.Replace(parts[2], "$1");
                        Parameters.Add(param, parts[2]);
                    }

                    param = null!;
                    break;
            }
        }
        if (param != null)
        {
            if (!Parameters.ContainsKey(param))
            {
                Parameters.Add(param, bool.TrueString);
            }
        }
    }

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
