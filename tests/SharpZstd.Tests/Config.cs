using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpZstd.Interop;

namespace SharpZstd.Tests;

internal class Config
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        ZstdImportResolver.ResolveLibrary += ZstdImportResolver_ResolveLibrary;
    }

    private static nint ZstdImportResolver_ResolveLibrary(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName.Equals(ZstdImportResolver.DllName))
        {
            ZstdImportResolver.GetRuntimeInfo(out string platform, out string architecture, out string extension);
            string fileName = ZstdImportResolver.GetDllName(platform, architecture, extension);
            string fullName = Path.Combine("runtimes", $"{platform}-{architecture}", "native", fileName);

            if (NativeLibrary.TryLoad(fullName, assembly, searchPath, out IntPtr nativeLibrary))
            {
                return nativeLibrary;
            }
        }

        return IntPtr.Zero;
    }
}
