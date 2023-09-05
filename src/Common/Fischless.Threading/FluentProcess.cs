using System.Diagnostics;

namespace Fischless.Threading;

public class FluentProcess : Process
{
    public static FluentProcess Create()
    {
        return new FluentProcess();
    }

    public new static FluentProcess Start(string fileName)
    {
        return Create()
            .FileName(fileName)
            .Start();
    }

    public new static FluentProcess Start(string fileName, string args)
    {
        return new FluentProcess()
            .FileName(fileName)
            .Arguments(args)
            .Start();
    }

    public new FluentProcess BeginErrorReadLine()
    {
        base.BeginErrorReadLine();
        return this;
    }

    public new FluentProcess BeginOutputReadLine()
    {
        base.BeginOutputReadLine();
        return this;
    }

    public new FluentProcess WaitForExit()
    {
        base.WaitForExit();
        return this;
    }

    public new FluentProcess WaitForInputIdle()
    {
        base.WaitForInputIdle();
        return this;
    }

    public new FluentProcess Start()
    {
        Debug.WriteLine(
        $"""
            "{StartInfo.FileName}" {StartInfo.Arguments}
        """.Trim());
        base.Start();
        return this;
    }
}
