using System;
using System.Runtime.InteropServices;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]

namespace SharpZstd.Interop;

internal static class Configuration
{
    private static readonly bool _disableResolveLibraryHook = GetAppContextData(
        "SharpZstd.Interop.DisableResolveLibraryHook", 
        defaultValue: false);

    public static bool DisableResolveLibraryHook => _disableResolveLibraryHook;

    private static bool GetAppContextData(string name, bool defaultValue)
    {
        object? data = AppContext.GetData(name);
        if (data is bool value)
        {
            return value;
        }
        else if ((data is string s) && bool.TryParse(s, out bool result))
        {
            return result;
        }
        else
        {
            return defaultValue;
        }
    }
}
