using System;
using System.Buffers;
using System.Runtime.InteropServices;
using SharpZstd.Interop;

namespace SharpZstd
{
    using static Zstd;

    public sealed unsafe class ZstdDecoder : SafeHandle
    {
        public ZstdDecoder() : this(CreateHandle(), true)
        {
        }

        public ZstdDecoder(ZSTD_DCtx* handle, bool ownsHandle) : base((IntPtr)handle, ownsHandle)
        {
        }

        /// <inheritdoc/>
        public override bool IsInvalid => handle == IntPtr.Zero;

        private static ZSTD_DCtx* CreateHandle()
        {
            ZSTD_DCtx* handle = ZSTD_createDCtx();
            if (handle == null)
            {
                throw new ZstdException("Failed to create decompression context", null, ZSTD_ErrorCode.ZSTD_error_memory_allocation);
            }
            return handle;
        }

        /// <inheritdoc cref="SafeHandle.DangerousAddRef(ref bool)"/>
        internal bool DangerousAddRef()
        {
            bool success = false;
            DangerousAddRef(ref success);
            return success;
        }

        /// <inheritdoc cref="SafeHandle.DangerousGetHandle"/>
        public new ZSTD_DCtx* DangerousGetHandle()
        {
            return (ZSTD_DCtx*)base.DangerousGetHandle();
        }

        public void SetParameter(ZSTD_dParameter parameter, int value)
        {
            DangerousAddRef();
            try
            {
                ZSTD_DCtx* dctx = DangerousGetHandle();
                nuint status = ZSTD_DCtx_setParameter(dctx, parameter, value);
                ZstdException.ThrowIfError(status);
            }
            finally
            {
                DangerousRelease();
            }
        }

        /// <summary>Tries to decompress a source span into a destination span.</summary>
        /// <include file="Docs.xml" path='//Params/Decode/ConsumeWriteSpans/*' />
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        public OperationStatus Decompress(ReadOnlySpan<byte> source, Span<byte> destination, out int written)
        {
            DangerousAddRef();
            try
            {
                ZSTD_DCtx* dctx = DangerousGetHandle();

                fixed (byte* srcPtr = source)
                fixed (byte* dstPtr = destination)
                {
                    nuint result = ZSTD_decompressDCtx(dctx, dstPtr, (nuint)destination.Length, srcPtr, (nuint)source.Length);
                    return ResultToStatus(result, out written);
                }
            }
            finally
            {
                DangerousRelease();
            }
        }
        
        /// <include file="Docs.xml" path='//Params/Decode/ConsumeWriteSpans/*' />
        /// <include file="Docs.xml" path='//Params/Decode/InputHint/*' />
        /// <include file="Docs.xml" path='//Params/ThrowOnError/*' />
        public OperationStatus DecompressStream(
            ReadOnlySpan<byte> source,
            Span<byte> destination,
            out int written,
            out int consumed,
            out int inputHint,
            bool throwOnError = false)
        {
            DangerousAddRef();
            try
            {
                ZSTD_DCtx* dctx = DangerousGetHandle();

                fixed (byte* srcPtr = source)
                fixed (byte* dstPtr = destination)
                {
                    ZSTD_outBuffer outputBuf = new() { dst = dstPtr, size = (nuint)destination.Length, pos = 0 };
                    ZSTD_inBuffer inputBuf = new() { src = srcPtr, size = (nuint)source.Length, pos = 0 };

                    nuint status = ZSTD_decompressStream(dctx, &outputBuf, &inputBuf);

                    written = (int)outputBuf.pos;
                    consumed = (int)inputBuf.pos;

                    if (ZSTD_isError(status) != 0)
                    {
                        if (throwOnError)
                        {
                            ZstdException.Throw(status);
                        }
                        inputHint = 0;
                        return OperationStatus.InvalidData;
                    }

                    inputHint = (int)status;
                    if (inputHint == 0)
                    {
                        return OperationStatus.Done;
                    }

                    if (outputBuf.pos == outputBuf.size)
                    {
                        return OperationStatus.DestinationTooSmall;
                    }
                    return OperationStatus.NeedMoreData;
                }
            }
            finally
            {
                DangerousRelease();
            }
        }

        /// <include file="Docs.xml" path='//Params/DecodeWriteSpans/*' />
        /// <include file="Docs.xml" path='//Params/ThrowOnError/*' />
        public OperationStatus FlushStream(
            Span<byte> destination,
            out int written,
            out int inputHint,
            bool throwOnError = false)
        {
            return DecompressStream(ReadOnlySpan<byte>.Empty, destination, out written, out _, out inputHint, throwOnError);
        }

        /// <include file="Docs.xml" path='//Methods/Reset/*' />
        public void Reset(ZSTD_ResetDirective directive = ZSTD_ResetDirective.ZSTD_reset_session_only)
        {
            DangerousAddRef();
            try
            {
                ZSTD_DCtx* dctx = DangerousGetHandle();
                nuint status = ZSTD_DCtx_reset(dctx, directive);
                ZstdException.ThrowIfError(status);
            }
            finally
            {
                DangerousRelease();
            }
        }

        /// <inheritdoc/>
        protected override bool ReleaseHandle()
        {
            ZSTD_DCtx* dctx = DangerousGetHandle();
            nuint status = ZSTD_freeDCtx(dctx);
            return ZSTD_isError(status) == 0;
        }

        /// <summary>Attempts to find the compressed size of the first frame in the provided input.</summary>
        /// <include file="Docs.xml" path='//Params/SingleFrameSpan/*' />
        /// <param name="length">The compressed size of data in the first frame.</param>
        /// <include file="Docs.xml" path='//Params/ThrowOnError/*' />
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        public static bool GetFrameCompressedLength(ReadOnlySpan<byte> source, out ulong length, bool throwOnError = false)
        {
            fixed (byte* srcPtr = source)
            {
                nuint result = ZSTD_findFrameCompressedSize(srcPtr, (nuint)source.Length);
                if (ZSTD_isError(result) != 0)
                {
                    if (throwOnError)
                    {
                        ZstdException.Throw(result);
                    }
                    length = 0;
                    return false;
                }
                length = result;
                return true;
            }
        }

        /// <summary>Attempts to find the content size of the first frame in the provided input.</summary>
        /// <include file="Docs.xml" path='//Params/SingleFrameSpan/*' />
        /// <param name="length">The content size of data in the first frame.</param>
        /// <include file="Docs.xml" path='//Params/ThrowOnError/*' />
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        public static bool GetFrameContentLength(ReadOnlySpan<byte> source, out ulong length, bool throwOnError = false)
        {
            fixed (byte* srcPtr = source)
            {
                ulong result = ZSTD_getFrameContentSize(srcPtr, (nuint)source.Length);
                return ValidateContentSize(result, out length, throwOnError);
            }
        }

        /// <summary>Attempts to find the decompressed size for the provided input.</summary>
        /// <include file="Docs.xml" path='//Params/MultiFrameSpan/*' />
        /// <param name="length">The decompressed size of all data in all successive frames.</param>
        /// <include file="Docs.xml" path='//Params/ThrowOnError/*' />
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        public static bool GetDecompressedLength(ReadOnlySpan<byte> source, out ulong length, bool throwOnError = false)
        {
            fixed (byte* srcPtr = source)
            {
                ulong result = ZSTD_findDecompressedSize(srcPtr, (nuint)source.Length);
                return ValidateContentSize(result, out length, throwOnError);
            }
        }

        /// <summary>Attempts to find the maximum decompressed size for the provided input.</summary>
        /// <include file="Docs.xml" path='//Params/MultiFrameSpan/*' />
        /// <param name="length">The upper-bound for the decompressed size of all data in all successive frames.</param>
        /// <include file="Docs.xml" path='//Params/ThrowOnError/*' />
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        public static bool GetMaxDecompressedLength(ReadOnlySpan<byte> source, out ulong length, bool throwOnError = false)
        {
            fixed (byte* srcPtr = source)
            {
                ulong result = ZSTD_decompressBound(srcPtr, (nuint)source.Length);
                return ValidateContentSize(result, out length, throwOnError);
            }
        }

        /// <summary>Tries to decompress a source span into a destination span.</summary>
        /// <include file="Docs.xml" path='//Params/Decode/ConsumeWriteSpans/*' />
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        public static bool TryDecompress(ReadOnlySpan<byte> source, Span<byte> destination, out int written)
        {
            fixed (byte* srcPtr = source)
            fixed (byte* dstPtr = destination)
            {
                nuint result = ZSTD_decompress(dstPtr, (nuint)destination.Length, srcPtr, (nuint)source.Length);
                return ResultToStatus(result, out written) == OperationStatus.Done;
            }
        }

        /// <include file="Docs.xml" path='//Params/ThrowOnError/*' />
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        private static bool ValidateContentSize(ulong result, out ulong length, bool throwOnError)
        {
            switch (result)
            {
                case ZSTD_CONTENTSIZE_ERROR:
                    if (throwOnError)
                    {
                        ZstdException.Throw(ZSTD_ErrorCode.ZSTD_error_GENERIC);
                    }
                    length = 0;
                    return false;

                case ZSTD_CONTENTSIZE_UNKNOWN:
                    length = ulong.MaxValue;
                    return true;

                default:
                    length = result;
                    return true;
            }
        }

        private static OperationStatus ResultToStatus(nuint result, out int written)
        {
            ZSTD_ErrorCode errorCode = ZSTD_getErrorCode(result);
            if (errorCode == ZSTD_ErrorCode.ZSTD_error_no_error)
            {
                written = (int)result;
                return OperationStatus.Done;
            }

            written = 0;
            return errorCode switch
            {
                ZSTD_ErrorCode.ZSTD_error_dstSize_tooSmall => OperationStatus.DestinationTooSmall,
                ZSTD_ErrorCode.ZSTD_error_srcSize_wrong => OperationStatus.NeedMoreData,
                _ => OperationStatus.InvalidData,
            };
        }
    }
}
