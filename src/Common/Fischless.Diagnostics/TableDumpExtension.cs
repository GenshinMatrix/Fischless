using Dumpify;
using System.Diagnostics;

namespace Fischless.Diagnostics;

public static class TableDumpExtension
{
    public static T DumpTableDebug<T>(this T? obj, bool isOn = true, string? label = null, int? maxDepth = null, IRenderer? renderer = null, bool? useDescriptors = null, ColorConfig? colors = null, MembersConfig? members = null, TypeNamingConfig? typeNames = null, TableConfig? tableConfig = null, OutputConfig? outputConfig = null)
    {
#if DEBUG
        if (isOn && Debugger.IsAttached)
        {
            string dump = obj.DumpText(label, maxDepth, renderer, useDescriptors, colors, members, typeNames, tableConfig, outputConfig);
            Debug.WriteLine(dump);
        }
#endif
        return obj;
    }
}
