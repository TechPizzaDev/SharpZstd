using System;
using System.Runtime.Serialization;
using SharpZstd.Interop;

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
            ErrorCode = (ZSTD_ErrorCode) info.GetInt32(ErrorCodeName);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(ErrorCodeName, (int) ErrorCode);
        }
#endif
    }
}
