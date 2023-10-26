using System.Diagnostics;

namespace Fischless.Diagnostics;

public static class CodeDumpExtension
{
    public static T DumpCodeDebug<T>(this T? obj, DumpStyle dumpStyle = DumpStyle.Console, bool isOn = true)
    {
#if DEBUG
        if (isOn && Debugger.IsAttached)
        {
            string dump = ObjectDumper.Dump(obj, dumpStyle);
            Debug.WriteLine(dump);
        }
#endif
        return obj;
    }

    public static T DumpCodeDebug<T>(this T? obj, DumpOptions dumpOptions, bool isOn = true)
    {
#if DEBUG
        if (isOn && Debugger.IsAttached)
        {
            string dump = ObjectDumper.Dump(obj, dumpOptions);
            Debug.WriteLine(dump);
        }
#endif
        return obj;
    }
}
