using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using SharpZstd.Interop;
using static SharpZstd.Interop.Zstd;

namespace SharpZstd
{
    public static unsafe class ZstdStatusExtensions
    {
        internal static void ThrowIfZstdError(this nuint functionResult)
        {
            if (ZSTD_isError(functionResult) != 0)
            {
                ThrowZstdException(functionResult);
            }
        }

        public static void ThrowIfError(nuint functionResult)
        {
            if (ZSTD_isError(functionResult) != 0)
            {
                ThrowZstdException(functionResult);
            }
        }

        [DoesNotReturn]
        public static void ThrowZstdException(nuint functionResult)
        {
            throw new ZstdException(GetString(ZSTD_getErrorName(functionResult)), null, ZSTD_getErrorCode(functionResult));
        }

        [DoesNotReturn]
        public static void ThrowZstdException(ZSTD_ErrorCode code)
        {
            throw new ZstdException(GetString(ZSTD_getErrorString(code)), null, code);
        }

        public static string? GetString(sbyte* ptr)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP1_1_OR_GREATER
            return Marshal.PtrToStringUTF8((IntPtr)ptr);
#else
            if (ptr == null)
            {
                return null;
            }

            int length = 0;
            while (ptr[length] != 0)
            {
                length++;
            }
            return Encoding.UTF8.GetString((byte*)ptr, length);
#endif
        }
    }
}
