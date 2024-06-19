using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpZstd.Interop;

namespace SharpZstd
{
    using static Zstd;
    using static ZSTD_EndDirective;

    public sealed unsafe class ZstdEncoder : SafeHandle
    {
        private static readonly int DefaultLevel = ZSTD_defaultCLevel();

        public ZstdEncoder() : this(CreateHandle(), true)
        {
        }

        public ZstdEncoder(ZSTD_CCtx* handle, bool ownsHandle) : base((IntPtr)handle, ownsHandle)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        private static ZSTD_CCtx* CreateHandle()
        {
            ZSTD_CCtx* handle = ZSTD_createCCtx();
            if (handle == null)
            {
                throw new ZstdException("Failed to create compression context", null, ZSTD_ErrorCode.ZSTD_error_memory_allocation);
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
        public new ZSTD_CCtx* DangerousGetHandle()
        {
            return (ZSTD_CCtx*)base.DangerousGetHandle();
        }

        public void SetParameter(ZSTD_cParameter parameter, int value)
        {
            DangerousAddRef();
            try
            {
                ZSTD_CCtx* cctx = DangerousGetHandle();
                nuint status = ZSTD_CCtx_setParameter(cctx, parameter, value);
                ZstdException.ThrowIfError(status);
            }
            finally
            {
                DangerousRelease();
            }
        }

        /// <summary>
        /// Tries to compress a source span into a destination span.
        /// </summary>
        /// <include file="Docs.xml" path='//Params/Encode/ConsumeWriteSpans/*' />
        /// <include file="Docs.xml" path='//Returns/Status/GenericOperation/*' />
        public OperationStatus Compress(ReadOnlySpan<byte> source, Span<byte> destination, out int written)
        {
            DangerousAddRef();
            try
            {
                ZSTD_CCtx* cctx = DangerousGetHandle();

                fixed (byte* srcPtr = source)
                fixed (byte* dstPtr = destination)
                {
                    nuint result = ZSTD_compress2(cctx, dstPtr, (nuint)destination.Length, srcPtr, (nuint)source.Length);
                    OperationStatus status = ResultToStatus(result, out written);
                    return status;
                }
            }
            finally
            {
                DangerousRelease();
            }
        }

        /// <summary>
        /// Tries to compress a source span into a destination span.
        /// </summary>
        /// <include file="Docs.xml" path='//Params/Encode/ConsumeWriteSpans/*' />
        /// <include file="Docs.xml" path='//Params/Encode/FinalBlock/*' />
        /// <include file="Docs.xml" path='//Returns/FullContextFlush/*' />
        public OperationStatus CompressStream(
            ReadOnlySpan<byte> source,
            Span<byte> destination,
            out int written,
            out int consumed,
            bool isFinalBlock = false)
        {
            DangerousAddRef();
            try
            {
                ZSTD_CCtx* cctx = DangerousGetHandle();

                fixed (byte* srcPtr = source)
                fixed (byte* dstPtr = destination)
                {
                    ZSTD_outBuffer outputBuf = new() { dst = dstPtr, size = (nuint)destination.Length, pos = 0 };
                    ZSTD_inBuffer inputBuf = new() { src = srcPtr, size = (nuint)source.Length, pos = 0 };
                    ZSTD_EndDirective mode = isFinalBlock ? ZSTD_e_end : ZSTD_e_continue;

                    nuint remaining = ZSTD_compressStream2(cctx, &outputBuf, &inputBuf, mode);
                    ZstdException.ThrowIfError(remaining);

                    bool finished = isFinalBlock ? (remaining == 0) : (inputBuf.pos == inputBuf.size);

                    written = (int)outputBuf.pos;
                    consumed = (int)inputBuf.pos;

                    if (!finished || outputBuf.pos == outputBuf.size)
                    {
                        return OperationStatus.DestinationTooSmall;
                    }

                    Debug.Assert(inputBuf.pos == inputBuf.size);
                    return OperationStatus.Done;
                }
            }
            finally
            {
                DangerousRelease();
            }
        }

        /// <summary>
        /// Tries to flush compressed data into a destination span.
        /// </summary>
        /// <include file="Docs.xml" path='//Params/Encode/WriteSpan/*' />
        /// <include file="Docs.xml" path='//Params/Encode/FinalBlock/*' />
        /// <include file="Docs.xml" path='//Returns/FullContextFlush/*' />
        public OperationStatus FlushStream(
            Span<byte> destination,
            out int written,
            bool isFinalBlock = false)
        {
            DangerousAddRef();
            try
            {
                ZSTD_CCtx* cctx = DangerousGetHandle();

                fixed (byte* dstPtr = destination)
                {
                    ZSTD_outBuffer outputBuf = new() { dst = dstPtr, size = (nuint)destination.Length, pos = 0 };
                    ZSTD_inBuffer inputBuf = new() { src = null, size = 0, pos = 0 };
                    ZSTD_EndDirective mode = isFinalBlock ? ZSTD_e_end : ZSTD_e_flush;

                    nuint remaining = ZSTD_compressStream2(cctx, &outputBuf, &inputBuf, mode);
                    ZstdException.ThrowIfError(remaining);

                    written = (int)outputBuf.pos;

                    if (remaining != 0)
                    {
                        Debug.Assert(outputBuf.pos == outputBuf.size);
                        return OperationStatus.DestinationTooSmall;
                    }
                    return OperationStatus.Done;
                }
            }
            finally
            {
                DangerousRelease();
            }
        }

        /// <include file="Docs.xml" path='//Methods/Reset/*' />
        public void Reset(ZSTD_ResetDirective directive = ZSTD_ResetDirective.ZSTD_reset_session_only)
        {
            DangerousAddRef();
            try
            {
                ZSTD_CCtx* cctx = DangerousGetHandle();
                nuint status = ZSTD_CCtx_reset(cctx, directive);
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
            ZSTD_CCtx* cctx = DangerousGetHandle();
            nuint status = ZSTD_freeCCtx(cctx);
            return ZSTD_isError(status) == 0;
        }

        /// <summary>Gets the maximum expected compressed length for the provided input size.</summary>
        /// <param name="inputSize">The input size to get the maximum expected compressed length from.</param>
        /// <returns>
        /// A number representing the maximum compressed length for the provided input size,
        /// producing zero if the input size is too large.
        /// </returns>
        public static nuint GetMaxCompressedLength(nuint inputSize)
        {
            return ZSTD_compressBound(inputSize);
        }

        /// <summary>Tries to compress a source span into a destination span.</summary>
        /// <include file="Docs.xml" path='//Params/Encode/ConsumeWriteSpans/*' />
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        public static bool TryCompress(ReadOnlySpan<byte> source, Span<byte> destination, out int written)
        {
            return TryCompress(source, destination, out written, DefaultLevel);
        }

        /// <summary>Tries to compress a source span into a destination span.</summary>
        /// <include file="Docs.xml" path='//Params/Encode/ConsumeWriteSpans/*' />
        /// <include file='Docs.xml' path='//Params/Encode/CompressionLevel'/>
        /// <include file="Docs.xml" path='//Returns/Status/Bool/*' />
        public static bool TryCompress(ReadOnlySpan<byte> source, Span<byte> destination, out int written, int compressionLevel)
        {
            fixed (byte* srcPtr = source)
            fixed (byte* dstPtr = destination)
            {
                nuint result = ZSTD_compress(dstPtr, (nuint)destination.Length, srcPtr, (nuint)source.Length, compressionLevel);
                OperationStatus status = ResultToStatus(result, out written);
                return status == OperationStatus.Done;
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

            if (errorCode == ZSTD_ErrorCode.ZSTD_error_dstSize_tooSmall)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }

            ZstdException.Throw(errorCode);

            written = 0;
            return OperationStatus.InvalidData;
        }
    }
}
