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

        internal void DangerousAddRef()
        {
            bool success = false;
            DangerousAddRef(ref success);
        }

        public new ZSTD_CCtx* DangerousGetHandle()
        {
            return (ZSTD_CCtx*)base.DangerousGetHandle();
        }

        public void SetParameter(ZSTD_cParameter parameter, int value)
        {
            DangerousAddRef();
            try
            {
                ZSTD_CCtx_setParameter(DangerousGetHandle(), parameter, value).ThrowIfZstdError();
            }
            finally
            {
                DangerousRelease();
            }
        }

        public OperationStatus CompressStream(
            ReadOnlySpan<byte> input,
            Span<byte> output,
            out int written,
            out int consumed,
            bool isFinalBlock = false)
        {
            DangerousAddRef();
            try
            {
                ZSTD_CCtx* cctx = DangerousGetHandle();
                ZSTD_outBuffer outputBuf = new() { size = (nuint)output.Length, pos = 0 };
                ZSTD_inBuffer inputBuf = new() { size = (nuint)input.Length, pos = 0 };
                ZSTD_EndDirective mode = isFinalBlock ? ZSTD_e_end : ZSTD_e_continue;

                bool finished;
                do
                {
                    fixed (byte* inputPtr = input)
                    fixed (byte* outputPtr = output)
                    {
                        outputBuf.dst = outputPtr;
                        inputBuf.src = inputPtr;

                        nuint remaining = ZSTD_compressStream2(cctx, &outputBuf, &inputBuf, mode);
                        remaining.ThrowIfZstdError();

                        finished = isFinalBlock ? (remaining == 0) : (inputBuf.pos == inputBuf.size);
                    }
                }
                while (!finished && outputBuf.pos != outputBuf.size);

                written = (int)outputBuf.pos;
                consumed = (int)inputBuf.pos;

                if (outputBuf.pos == outputBuf.size)
                {
                    return OperationStatus.DestinationTooSmall;
                }
                if (isFinalBlock)
                {
                    Debug.Assert(inputBuf.pos == inputBuf.size);
                    return OperationStatus.Done;
                }
                return OperationStatus.NeedMoreData;
            }
            finally
            {
                DangerousRelease();
            }
        }

        public OperationStatus FlushStream(Span<byte> output, out int written, bool isFinalBlock = false)
        {
            DangerousAddRef();
            try
            {
                fixed (byte* outputPtr = output)
                {
                    ZSTD_CCtx* cctx = DangerousGetHandle();
                    ZSTD_outBuffer outputBuf = new() { dst = outputPtr, size = (nuint)output.Length, pos = 0 };
                    ZSTD_inBuffer inputBuf = new() { src = null, size = 0, pos = 0 };
                    ZSTD_EndDirective mode = isFinalBlock ? ZSTD_e_end : ZSTD_e_flush;

                    nuint remaining = ZSTD_compressStream2(cctx, &outputBuf, &inputBuf, mode);
                    remaining.ThrowIfZstdError();

                    written = (int)outputBuf.pos;

                    if (remaining != 0)
                    {
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

        public void Reset(ZSTD_ResetDirective resetDirective)
        {
            DangerousAddRef();
            try
            {
                ZSTD_CCtx_reset(DangerousGetHandle(), resetDirective).ThrowIfZstdError();
            }
            finally
            {
                DangerousRelease();
            }
        }

        protected override bool ReleaseHandle()
        {
            nuint status = ZSTD_freeCCtx(DangerousGetHandle());
            return ZSTD_isError(status) == 0;
        }
    }
}
