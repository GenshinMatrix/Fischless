using Fischless.Hosting.Absraction;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fischless.Hosting;

public class CommandLineBuilder : ICommandLineBuilder
{
    public StringDictionary Parameters { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

    public CommandLineBuilder(string[]? args)
    {
#pragma warning disable SYSLIB1045
        Regex spliter = new(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Regex remover = new(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
#pragma warning restore SYSLIB1045

        string param = null!;
        string[] parts;

        args ??= Array.Empty<string>();

        foreach (string txt in args.Skip(1))
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
}
