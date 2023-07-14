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
                ZSTD_DCtx_setParameter(DangerousGetHandle(), parameter, value).ThrowIfZstdError();
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
            bool isFinalBlock = false,
            bool throwOnError = false)
        {
            DangerousAddRef();
            try
            {
                fixed (byte* inputPtr = input)
                fixed (byte* outputPtr = output)
                {
                    ZSTD_DCtx* cctx = DangerousGetHandle();
                    ZSTD_outBuffer outputBuf = new() { dst = outputPtr, size = (nuint)output.Length, pos = 0 };
                    ZSTD_inBuffer inputBuf = new() { src = inputPtr, size = (nuint)input.Length, pos = 0 };

                    nuint status = ZSTD_decompressStream(cctx, &outputBuf, &inputBuf);
                    written = (int)outputBuf.pos;
                    consumed = (int)inputBuf.pos;

                    if (ZSTD_isError(status) != 0)
                    {
                        if (throwOnError)
                        {
                            ZstdStatusExtensions.ThrowZstdException(status);
                        }
                        inputHint = 0;
                        return OperationStatus.InvalidData;
                    }

                    inputHint = (int)status;
                    if (outputBuf.pos == outputBuf.size)
                    {
                        return OperationStatus.DestinationTooSmall;
                    }

                    if (isFinalBlock)
                    {
                        return OperationStatus.Done;
                    }
                    return OperationStatus.NeedMoreData;
                }
            }
            finally
            {
                DangerousRelease();
            }
        }

        public OperationStatus FlushStream(Span<byte> output, out int written, bool throwOnError = false)
        {
            DangerousAddRef();
            try
            {
                ZSTD_DCtx* cctx = DangerousGetHandle();
                ZSTD_outBuffer outputBuf = new() { size = (nuint)output.Length, pos = 0 };
                ZSTD_inBuffer inputBuf = new() { src = null, size = 0, pos = 0 };

                bool invalid = false;
                nuint status = 0;
                while (outputBuf.pos != outputBuf.size)
                {
                    fixed (byte* outputPtr = output)
                    {
                        outputBuf.dst = outputPtr;
                        status = ZSTD_decompressStream(cctx, &outputBuf, &inputBuf);
                    }

                    if (ZSTD_isError(status) != 0)
                    {
                        invalid = true;
                        break;
                    }
                }

                written = (int)outputBuf.pos;
                if (invalid)
                {
                    if (throwOnError)
                    {
                        ZstdStatusExtensions.ThrowZstdException(status);
                    }
                    return OperationStatus.InvalidData;
                }

                if (outputBuf.pos == outputBuf.size)
                {
                    return OperationStatus.DestinationTooSmall;
                }
                return OperationStatus.Done;
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
                ZSTD_DCtx_reset(DangerousGetHandle(), resetDirective).ThrowIfZstdError();
            }
            finally
            {
                DangerousRelease();
            }
        }

        protected override bool ReleaseHandle()
        {
            nuint status = ZSTD_freeDCtx(DangerousGetHandle());
            return ZSTD_isError(status) == 0;
        }
    }
}
