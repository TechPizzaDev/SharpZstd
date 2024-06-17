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
    public sealed class ZstdEncodeStream : Stream
    {
        private static readonly int DefaultBufferSize = (int) Zstd.ZSTD_CStreamOutSize();

        private ZstdEncoder _encoder;
        private bool _leaveEncoderOpen;
        private Stream _stream;
        private bool _leaveStreamOpen;
        private byte[] _buffer;
        private int _activeAsyncOperation; // 1 == true, 0 == false

        public ZstdEncodeStream(ZstdEncoder encoder, bool leaveEncoderOpen, Stream stream, bool leaveStreamOpen)
        {
            _encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
            _leaveEncoderOpen = leaveEncoderOpen;
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _leaveStreamOpen = leaveStreamOpen;
            _buffer = ArrayPool<byte>.Shared.Rent(DefaultBufferSize);
        }

        public ZstdEncodeStream(Stream stream, bool leaveOpen) : this(new ZstdEncoder(), false, stream, leaveOpen)
        {
        }

        public ZstdEncoder Encoder => _encoder;

        public Stream BaseStream => _stream;

        private bool AsyncOperationIsActive => _activeAsyncOperation != 0;

        /// <inheritdoc/>
        public override bool CanWrite => _stream?.CanWrite ?? false;

        /// <inheritdoc/>
        public override bool CanRead => false;

        /// <inheritdoc/>
        public override bool CanSeek => false;

        /// <exception cref="NotSupportedException" />
        public override long Length => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            EnsureNotDisposed();

            FlushCore(false);

            _stream.Flush();
        }

        private void FlushCore(bool finish)
        {
            OperationStatus status;
            do
            {
                status = _encoder.FlushStream(_buffer, out int written, finish);
                if (written > 0)
                {
                    _stream.Write(_buffer, 0, written);
                }
            }
            while (status == OperationStatus.DestinationTooSmall);
        }

        /// <inheritdoc/>
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            EnsureNoActiveAsyncOperation();
            EnsureNotDisposed();

            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(cancellationToken);
            }
            return FlushAsyncCore(false, true, cancellationToken);
        }

        private async Task FlushAsyncCore(bool finish, bool flushBase, CancellationToken cancellationToken)
        {
            AsyncOperationStarting();
            try
            {
                OperationStatus status;
                do
                {
                    status = _encoder.FlushStream(_buffer, out int written, finish);
                    if (written > 0)
                    {
#if NETSTANDARD2_0
                        await _stream.WriteAsync(_buffer, 0, written, cancellationToken).ConfigureAwait(false);
#else
                        await _stream.WriteAsync(_buffer.AsMemory(0, written), cancellationToken).ConfigureAwait(false);
#endif
                    }
                }
                while (status == OperationStatus.DestinationTooSmall);

                if (flushBase)
                {
                    await _stream.FlushAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
                AsyncOperationCompleting();
            }
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            WriteCore(buffer.AsSpan(offset, count));
        }

        /// <inheritdoc/>
        public override unsafe void WriteByte(byte value)
        {
            WriteCore(new ReadOnlySpan<byte>(&value, 1));
        }

        /// <inheritdoc/>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return WriteAsyncCore(new ReadOnlyMemory<byte>(buffer, offset, count), cancellationToken).AsTask();
        }

        /// <inheritdoc/>
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state)
        {
            return TaskToAsyncResult.Begin(WriteAsync(buffer, offset, count, CancellationToken.None), callback, state);
        }

        /// <inheritdoc/>
        public override void EndWrite(IAsyncResult asyncResult)
        {
            EnsureNotDisposed();
            TaskToAsyncResult.End(asyncResult);
        }

        private void WriteCore(ReadOnlySpan<byte> input)
        {
            EnsureNotDisposed();

            int totalConsumed = 0;
            OperationStatus status;
            do
            {
                ReadOnlySpan<byte> src = input.Slice(totalConsumed);
                status = _encoder.CompressStream(src, _buffer, out int written, out int consumed, false);

                totalConsumed += consumed;
                if (written > 0)
                {
                    _stream.Write(_buffer, 0, written);
                }
            }
            while (status == OperationStatus.DestinationTooSmall);

            Debug.Assert(input.Length == totalConsumed);
        }

        private ValueTask WriteAsyncCore(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
        {
            EnsureNoActiveAsyncOperation();
            EnsureNotDisposed();

            if (cancellationToken.IsCancellationRequested)
            {
                return new ValueTask(Task.FromCanceled(cancellationToken));
            }
            return Core(buffer, cancellationToken);

            async ValueTask Core(ReadOnlyMemory<byte> input, CancellationToken cancellationToken)
            {
                AsyncOperationStarting();
                try
                {
                    int totalConsumed = 0;
                    OperationStatus status;
                    do
                    {
                        ReadOnlyMemory<byte> src = input.Slice(totalConsumed);
                        status = _encoder.CompressStream(src.Span, _buffer, out int written, out int consumed, false);

                        totalConsumed += consumed;
                        if (written > 0)
                        {
#if NETSTANDARD2_0
                            await _stream.WriteAsync(_buffer, 0, written, cancellationToken).ConfigureAwait(false);
#else
                            await _stream.WriteAsync(_buffer.AsMemory(0, written), cancellationToken).ConfigureAwait(false);
#endif
                        }
                    }
                    while (status == OperationStatus.DestinationTooSmall);

                    Debug.Assert(input.Length == totalConsumed);
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
            try
            {
                if (_stream != null && disposing)
                {
                    FlushCore(true);
                }
            }
            finally
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
        }

        /// <exception cref="NotSupportedException" />
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override int ReadByte() => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
            throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state) =>
            throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override int EndRead(IAsyncResult asyncResult) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override void SetLength(long value) => throw new NotSupportedException();

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        /// <exception cref="NotSupportedException" />
        public override int Read(Span<byte> buffer) => throw new NotSupportedException();

        /// <exception cref="NotSupportedException" />
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) =>
            throw new NotSupportedException();

        /// <inheritdoc/>
        public override void Write(ReadOnlySpan<byte> buffer)
        {
            WriteCore(buffer);
        }

        /// <inheritdoc/>
        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
        {
            return WriteAsyncCore(buffer, cancellationToken);
        }

        /// <inheritdoc/>
        public override async ValueTask DisposeAsync()
        {
            try
            {
                if (_stream != null)
                {
                    await FlushAsyncCore(true, false, CancellationToken.None).ConfigureAwait(false);
                }
            }
            finally
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

        private static void ThrowInvalidBeginCall()
        {
            throw new InvalidOperationException();
        }

        private void DisposeAuxiliary()
        {
            ZstdEncoder? encoder = _encoder;
            _encoder = null!;
            try
            {
                if (encoder != null && !_leaveEncoderOpen)
                {
                    encoder.Dispose();
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
