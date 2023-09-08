using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Fischless.Helper;

public static class AssemblyHelper
{
    public static string GetAssemblyVersion(this Assembly assembly, VersionType type = VersionType.Major | VersionType.Minor | VersionType.Build)
    {
        Version version = assembly.GetName().Version;
        StringBuilder sb = new();

        if (type.HasFlag(VersionType.Major))
        {
            sb.Append(version.Major);
        }
        if (type.HasFlag(VersionType.Minor))
        {
            if (sb.Length > 0)
                sb.Append(".");
            sb.Append(version.Minor);
        }
        if (type.HasFlag(VersionType.Build))
        {
            if (sb.Length > 0)
                sb.Append(".");
            sb.Append(version.Build);
        }
        if (type.HasFlag(VersionType.Revision))
        {
            if (sb.Length > 0)
                sb.Append(".");
            sb.Append(version.Revision);
        }
        return sb.ToString();
    }

    public static TAssy GetAssembly<TAssy>(this Assembly assembly)
    {
        TAssy[] assemblies = GetAssemblies<TAssy>(assembly);

        if (assemblies.Length > 0)
        {
            return assemblies[0];
        }
        return default;
    }

    public static TAssy[] GetAssemblies<TAssy>(this Assembly assembly)
    {
        object[] attributes = (assembly ?? Assembly.GetExecutingAssembly()).GetCustomAttributes(typeof(TAssy), false);
        List<TAssy> attributeList = new();

        foreach (object attribute in attributes)
        {
            if (attribute is TAssy assy)
            {
                attributeList.Add(assy);
            }
        }
        return attributeList.ToArray();
    }
}

[Flags]
public enum VersionType
{
    Major = 0,
    Minor = 1,
    Build = 2,
    Revision = 4,
}
