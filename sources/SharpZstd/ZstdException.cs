using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using SharpZstd.Interop;
using static SharpZstd.Interop.Zstd;

namespace SharpZstd
{
#if !NET8_0_OR_GREATER
    [Serializable]
#endif
    public class ZstdException : Exception
    {
        private const string ErrorCodeName = "ErrorCode";

        public ZSTD_ErrorCode ErrorCode { get; }

        public ZstdException()
        {
        }

        public ZstdException(string? message) : base(message)
        {
            ErrorCode = ZSTD_ErrorCode.ZSTD_error_GENERIC;
        }

        public ZstdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

#if NET5_0_OR_GREATER
        [System.Text.Json.Serialization.JsonConstructor]
#endif
        public ZstdException(string? message, Exception? innerException, ZSTD_ErrorCode errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

#if !NET8_0_OR_GREATER
        protected ZstdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ErrorCode = (ZSTD_ErrorCode)info.GetInt32(ErrorCodeName);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(ErrorCodeName, (int)ErrorCode);
        }
#endif

        public static void ThrowIfError(nuint functionResult)
        {
            if (ZSTD_isError(functionResult) != 0)
            {
                Throw(functionResult);
            }
        }

        [DoesNotReturn]
        public static unsafe void Throw(nuint functionResult)
        {
            string? message = GetString(ZSTD_getErrorName(functionResult));
            ZSTD_ErrorCode code = ZSTD_getErrorCode(functionResult);
            throw new ZstdException(message, null, code);
        }

        [DoesNotReturn]
        public static unsafe void Throw(ZSTD_ErrorCode code)
        {
            string? message = GetString(ZSTD_getErrorString(code));
            throw new ZstdException(message, null, code);
        }

        public static unsafe string? GetString(sbyte* ptr)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP1_1_OR_GREATER
            return Marshal.PtrToStringUTF8((nint)ptr);
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
