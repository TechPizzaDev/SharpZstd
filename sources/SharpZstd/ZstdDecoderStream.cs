using System;
using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SharpZstd.Interop;

namespace SharpZstd
{
    public sealed class ZstdDecoderStream : Stream
    {
        private static readonly int DefaultBufferSize = (int) Zstd.ZSTD_DStreamInSize();

        private ZstdDecoder _decoder;
        private bool _leaveDecoderOpen;
        private Stream _stream;
        private bool _leaveStreamOpen;
        private bool _consumeMultipleFrames;

        private int _activeAsyncOperation; // 1 == true, 0 == false
        private byte[] _buffer;
        private int _bufferSize;
        private int _bufferPos;
        private bool _finished;

        public ZstdDecoderStream(ZstdDecoder decoder, bool leaveDecoderOpen, Stream stream, bool leaveStreamOpen, bool consumeMultipleFrames)
        {
            _decoder = decoder ?? throw new ArgumentNullException(nameof(decoder));
            _leaveDecoderOpen = leaveDecoderOpen;
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _leaveStreamOpen = leaveStreamOpen;
            _consumeMultipleFrames = consumeMultipleFrames;

            _buffer = ArrayPool<byte>.Shared.Rent(DefaultBufferSize);
            _bufferSize = 0;
            _bufferPos = 0;
        }

        public ZstdDecoderStream(Stream stream, bool leaveOpen) : this(new ZstdDecoder(), false, stream, leaveOpen, true)
        {
        }

        public ZstdDecoder Decoder => _decoder;

        public Stream BaseStream => _stream;

        private bool AsyncOperationIsActive => _activeAsyncOperation != 0;

        /// <inheritdoc/>
        public override bool CanRead => _stream?.CanRead ?? false;

        /// <inheritdoc/>
        public override bool CanSeek => false;

        /// <inheritdoc/>
        public override bool CanWrite => false;

        /// <exception cref="NotSupportedException" />
        public override long Length => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return ReadCore(buffer.AsSpan(offset, count));
        }

        /// <inheritdoc/>
        public override int ReadByte()
        {
            Span<byte> buffer = stackalloc byte[1];
            int read = ReadCore(buffer);
            if (read != 1)
            {
                return -1;
            }
            return buffer[0];
        }

        /// <inheritdoc/>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return ReadAsyncMemory(new Memory<byte>(buffer, offset, count), cancellationToken).AsTask();
        }

        /// <inheritdoc/>
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state)
        {
            return TaskToAsyncResult.Begin(ReadAsync(buffer, offset, count, CancellationToken.None), callback, state);
        }

        /// <inheritdoc/>
        public override int EndRead(IAsyncResult asyncResult)
        {
            EnsureNotDisposed();
            return TaskToAsyncResult.End<int>(asyncResult);
        }

        private int ConsumeInput(Span<byte> dst, out int written, out int inputHint)
        {
            ReadOnlySpan<byte> src = _buffer.AsSpan(_bufferPos, _bufferSize - _bufferPos);

            OperationStatus status = _decoder.DecompressStream(
                src, dst, out written, out int consumed, out inputHint, true);

            _bufferPos += consumed;

            if (status == OperationStatus.Done && !_consumeMultipleFrames)
            {
                Debug.Assert(inputHint == 0);
                _finished = true;
                return -1;
            }

            if (status == OperationStatus.DestinationTooSmall)
            {
                return -1;
            }

            if (_bufferPos != _bufferSize)
            {
                // There is more input to consume.
                return 0;
            }

            _bufferPos = 0;
            _bufferSize = 0;

            // Fill up the buffer to max when trying to read multiple frames.
            int toRead = _consumeMultipleFrames
                ? _buffer.Length
                : Math.Min(inputHint, _buffer.Length);

            Debug.Assert(toRead > 0);

            return toRead;
        }

        private int ReadCore(Span<byte> dst)
        {
            EnsureNotDisposed();

            if (_finished)
            {
                return 0;
            }

            int totalWritten = 0;
            do
            {
                int toRead = ConsumeInput(dst.Slice(totalWritten), out int written, out int inputHint);
                totalWritten += written;

                if (toRead < 0)
                {
                    break;
                }
                else if (toRead == 0)
                {
                    continue;
                }

                int read = _stream.Read(_buffer, _bufferPos, toRead);
                if (read <= 0)
                {
                    if (inputHint > 0)
                    {
                        ThrowEndOfStream();
                    }

                    _finished = true;
                    break;
                }
                _bufferSize += read;
            }
            while (true);

            return totalWritten;
        }

        private ValueTask<int> ReadAsyncMemory(Memory<byte> buffer, CancellationToken cancellationToken)
        {
            EnsureNoActiveAsyncOperation();
            EnsureNotDisposed();

            if (cancellationToken.IsCancellationRequested)
            {
                return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
            }
            if (_finished)
            {
                return new ValueTask<int>(0);
            }
            return Core(buffer, cancellationToken);

            async ValueTask<int> Core(Memory<byte> output, CancellationToken cancellationToken)
            {
                AsyncOperationStarting();
                try
                {
                    int totalWritten = 0;
                    do
                    {
                        int toRead = ConsumeInput(output.Slice(totalWritten).Span, out int written, out int inputHint);
                        totalWritten += written;

                        if (toRead < 0)
                        {
                            break;
                        }
                        else if (toRead == 0)
                        {
                            continue;
                        }

#if NETSTANDARD2_0
                        int read = await _stream.ReadAsync(_buffer, _bufferPos, toRead, cancellationToken).ConfigureAwait(false);
#else
                        int read = await _stream.ReadAsync(_buffer.AsMemory(_bufferPos, toRead), cancellationToken).ConfigureAwait(false);
#endif
                        if (read <= 0)
                        {
                            if (inputHint > 0)
                            {
                                ThrowEndOfStream();
                            }

                            _finished = true;
                            break;
                        }
                        _bufferSize += read;
                    }
                    while (true);

                    return totalWritten;
                }
                finally
                {
                    AsyncOperationCompleting();
                }
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            Stream stream = _stream;
            _stream = null!;
            try
            {
                if (stream != null && disposing && !_leaveStreamOpen)
                {
                    stream.Dispose();
                }
            }
            finally
            {
                DisposeAuxiliary();
            }
        }

        /// <exception cref="NotSupportedException" />
        public override void Flush() => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override void WriteByte(byte value) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
            throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state) =>
            throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override void EndWrite(IAsyncResult asyncResult) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override void SetLength(long value) => throw new NotSupportedException();

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        /// <exception cref="NotSupportedException" />
        public override void Write(ReadOnlySpan<byte> buffer) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken) =>
            throw new NotSupportedException();

        /// <inheritdoc/>
        public override int Read(Span<byte> buffer)
        {
            return ReadCore(buffer);
        }

        /// <inheritdoc/>
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            return ReadAsyncMemory(buffer, cancellationToken);
        }

        /// <inheritdoc/>
        public override async ValueTask DisposeAsync()
        {
            Stream stream = _stream;
            _stream = null!;
            try
            {
                if (stream != null && !_leaveStreamOpen)
                {
                    await stream.DisposeAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                DisposeAuxiliary();
                GC.SuppressFinalize(this);
            }
        }
#endif

        private void EnsureNotDisposed()
        {
            if (_stream == null)
            {
                ThrowDisposed();
            }
        }

        private void EnsureNoActiveAsyncOperation()
        {
            if (AsyncOperationIsActive)
            {
                ThrowInvalidBeginCall();
            }
        }

        private void AsyncOperationStarting()
        {
            if (Interlocked.Exchange(ref _activeAsyncOperation, 1) != 0)
            {
                ThrowInvalidBeginCall();
            }
        }

        private void AsyncOperationCompleting()
        {
            Volatile.Write(ref _activeAsyncOperation, 0);
        }

        [DoesNotReturn]
        private void ThrowDisposed()
        {
            throw new ObjectDisposedException(this?.GetType().FullName);
        }

        [DoesNotReturn]
        private static void ThrowEndOfStream()
        {
            throw new EndOfStreamException();
        }
        
        [DoesNotReturn]
        private static void ThrowInvalidBeginCall()
        {
            throw new InvalidOperationException();
        }

        private void DisposeAuxiliary()
        {
            ZstdDecoder? decoder = _decoder;
            _decoder = null!;
            try
            {
                if (decoder != null && !_leaveDecoderOpen)
                {
                    decoder.Dispose();
                }
            }
            finally
            {
                byte[]? buffer = _buffer;
                _buffer = null!;
                if (buffer != null)
                {
                    if (!AsyncOperationIsActive)
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                    }
                }
            }
        }
    }
}
