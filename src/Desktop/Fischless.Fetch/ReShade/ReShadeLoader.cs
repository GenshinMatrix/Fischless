using Fischless.Logging;
using Fischless.Threading;
using System.Text;

namespace Fischless.Fetch.ReShade;

public static class ReShadeLoader
{
    public static void Launch(string loaderPath)
    {
        FluentProcess.Create()
            .FileName(Path.Combine(loaderPath, ReShadeSentimentalString.LoaderExe))
            .WorkingDirectory(loaderPath)
            .UseShellExecute()
            .Verb("runas")
            .Start()
            .Forget();
    }

    public static void SetD3dxIniGameExe(string loaderPath, Func<string> setTarget)
    {
        if (setTarget == null)
        {
            return;
        }

        string d3dxIni = Path.Combine(loaderPath, ReShadeSentimentalString.LoaderD3dxIni);

        if (File.Exists(d3dxIni))
        {
            try
            {
                string text = File.ReadAllText(d3dxIni)?.Replace("\r", string.Empty);
                StringBuilder sb = new();

                foreach (string line in text.Split('\n'))
                {
                    if (line.StartsWith(ReShadeSentimentalString.LoaderD3dxIniGameExePrefix))
                    {
                        string target = line[ReShadeSentimentalString.LoaderD3dxIniGameExePrefix.Length..];

                        if (!File.Exists(target.Trim()))
                        {
                            target = setTarget.Invoke();

                            if (target == null)
                            {
                                return; // Canceled
                            }
                            sb.AppendLine($"{ReShadeSentimentalString.LoaderD3dxIniGameExePrefix}{target.Replace("/", @"\")}");
                            continue;
                        }
                    }
                    sb.AppendLine(line);
                }

                File.WriteAllText(d3dxIni, sb.ToString());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
