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

        internal void DangerousAddRef()
        {
            bool success = false;
            DangerousAddRef(ref success);
        }

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

        public OperationStatus DecompressStream(
            ReadOnlySpan<byte> input,
            Span<byte> output,
            out int written,
            out int consumed,
            out int inputHint,
            bool throwOnError = false)
        {
            DangerousAddRef();
            try
            {
                ZSTD_DCtx* dctx = DangerousGetHandle();

                fixed (byte* srcPtr = input)
                fixed (byte* dstPtr = output)
                {
                    ZSTD_outBuffer outputBuf = new() { dst = dstPtr, size = (nuint)output.Length, pos = 0 };
                    ZSTD_inBuffer inputBuf = new() { src = srcPtr, size = (nuint)input.Length, pos = 0 };

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

        public OperationStatus FlushStream(
            Span<byte> output,
            out int written,
            out int inputHint,
            bool throwOnError = false)
        {
            return DecompressStream(ReadOnlySpan<byte>.Empty, output, out written, out _, out inputHint, throwOnError);
        }

        public void Reset(ZSTD_ResetDirective resetDirective)
        {
            DangerousAddRef();
            try
            {
                ZSTD_DCtx* dctx = DangerousGetHandle();
                nuint status = ZSTD_DCtx_reset(dctx, resetDirective);
                ZstdException.ThrowIfError(status);
            }
            finally
            {
                DangerousRelease();
            }
        }

        protected override bool ReleaseHandle()
        {
            ZSTD_DCtx* dctx = DangerousGetHandle();
            nuint status = ZSTD_freeDCtx(dctx);
            return ZSTD_isError(status) == 0;
        }
    }
}
