using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop
{
    public static class ZstdImportResolver
    {
        public const string DllName = "SharpZstd.Native";

#if NETCOREAPP3_0_OR_GREATER
        public static event DllImportResolver? ResolveLibrary;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2255", Justification = "<Pending>")]
        [System.Runtime.CompilerServices.ModuleInitializer]
        internal static void Initialize()
        {
            NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), OnDllImport);
        }

        private static IntPtr OnDllImport(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (TryResolveLibrary(libraryName, assembly, searchPath, out IntPtr nativeLibrary))
            {
                return nativeLibrary;
            }

            if (libraryName.Equals(DllName))
            {
                GetRuntimeInfo(out string platform, out string architecture, out string extension);
                string fileName = GetDllName(platform, architecture, extension);

                if (NativeLibrary.TryLoad(fileName, assembly, searchPath, out nativeLibrary))
                {
                    return nativeLibrary;
                }
            }

            return IntPtr.Zero;
        }

        public static bool TryResolveLibrary(
            string libraryName, Assembly assembly, DllImportSearchPath? searchPath, out IntPtr nativeLibrary)
        {
            DllImportResolver? resolveLibrary = ResolveLibrary;
            if (resolveLibrary != null)
            {
                Delegate[] resolvers = resolveLibrary.GetInvocationList();
                foreach (Delegate resolver in resolvers)
                {
                    nativeLibrary = ((DllImportResolver)resolver).Invoke(libraryName, assembly, searchPath);
                    if (nativeLibrary != IntPtr.Zero)
                    {
                        return true;
                    }
                }
            }

            nativeLibrary = IntPtr.Zero;
            return false;
        }
#endif

        public static string GetDllName(
            string platform,
            string architecture,
            string extension)
        {
            string name = $"{DllName}.{platform}-{architecture}.{extension}";
            return name;
        }

        public static void GetRuntimeInfo(
            out string platform,
            out string architecture,
            out string extension)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                platform = "win";
                extension = "dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                platform = "linux";
                extension = "so";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                platform = "osx";
                extension = "dylib";
            }
            else
            {
                throw new PlatformNotSupportedException("Unsupported OS platform.");
            }

            if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                architecture = "x64";
            }
            else if (RuntimeInformation.ProcessArchitecture == Architecture.X86)
            {
                architecture = "x86";
            }
            else if (RuntimeInformation.ProcessArchitecture == Architecture.Arm)
            {
                architecture = "arm";
            }
            else if (RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
            {
                architecture = "arm64";
            }
            else
            {
                throw new PlatformNotSupportedException("Unsupported process architecture.");
            }
        }
    }
}