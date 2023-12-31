﻿using System.Diagnostics;

namespace Fischless.Fetch.Lazy;

public static class LazyVerification
{
    public static async Task<bool> VerifyAssembly(string path)
    {
        FileVersionInfo fvi = await Task.Run(() =>
        {
            if (File.Exists(path))
            {
                return FileVersionInfo.GetVersionInfo(path);
            }
            return null!;
        });

        if (fvi != null)
        {
            return (fvi.ProductName == "GenshinLazy" || fvi.ProductName == "Fischless.Lazy")
                && (fvi.OriginalFilename == "GenshinLazy.dll" || fvi.OriginalFilename == "Fischless.Lazy.dll")
                && fvi.CompanyName == "GenshinMatrix";
        }
        return false;
    }
}
