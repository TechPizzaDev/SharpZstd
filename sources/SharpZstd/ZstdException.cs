using System;
using System.IO;
using System.Runtime.Serialization;
using SharpZstd.Interop;

namespace SharpZstd
{
    [Serializable]
    public class ZstdException : IOException
    {
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

        public ZstdException(string? message, Exception? innerException, ZSTD_ErrorCode errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected ZstdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
