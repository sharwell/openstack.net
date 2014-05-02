namespace OpenStack.IO
{
    using System;
    using System.Threading.Tasks;
    using SeekOrigin = System.IO.SeekOrigin;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    // Uses IProgress<T>
    using Rackspace.Threading;
#endif

#if NET45PLUS
    using System.Threading;
    // Uses CoreTaskExtensions.Select
    using Rackspace.Threading;
#endif

    public class ProgressStream : Stream
    {
        private readonly Stream _underlyingStream;
        private readonly IProgress<long> _progress;

        public ProgressStream(Stream underlyingStream, IProgress<long> progress)
        {
            if (underlyingStream == null)
                throw new ArgumentNullException("underlyingStream");
            if (progress == null)
                throw new ArgumentNullException("progress");

            _underlyingStream = underlyingStream;
            _progress = progress;
        }

        public override bool CanRead
        {
            get
            {
                return _underlyingStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return _underlyingStream.CanSeek;
            }
        }

        public override bool CanTimeout
        {
            get
            {
                return _underlyingStream.CanTimeout;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return _underlyingStream.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                return _underlyingStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return _underlyingStream.Position;
            }

            set
            {
                _underlyingStream.Position = value;
                _progress.Report(Position);
            }
        }

        public override int ReadTimeout
        {
            get
            {
                return _underlyingStream.ReadTimeout;
            }

            set
            {
                _underlyingStream.ReadTimeout = value;
            }
        }

        public override int WriteTimeout
        {
            get
            {
                return _underlyingStream.WriteTimeout;
            }

            set
            {
                _underlyingStream.WriteTimeout = value;
            }
        }

#if NET45PLUS
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            return
                _underlyingStream.CopyToAsync(destination, bufferSize, cancellationToken)
                .Select(task => _progress.Report(Position));
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return
                _underlyingStream.FlushAsync(cancellationToken)
                .Select(task => _progress.Report(Position));
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return
                _underlyingStream.ReadAsync(buffer, offset, count, cancellationToken)
                .Select(
                    task =>
                    {
                        _progress.Report(Position);
                        return task.Result;
                    });
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return
                _underlyingStream.WriteAsync(buffer, offset, count, cancellationToken)
                .Select(task => _progress.Report(Position));
        }
#endif

#if !PORTABLE
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _underlyingStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _underlyingStream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void Close()
        {
            _underlyingStream.Close();
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            int result = _underlyingStream.EndRead(asyncResult);
            _progress.Report(Position);
            return result;
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            _underlyingStream.EndWrite(asyncResult);
            _progress.Report(Position);
        }
#endif

        public override void Flush()
        {
            _underlyingStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int result = _underlyingStream.Read(buffer, offset, count);
            _progress.Report(Position);
            return result;
        }

        public override int ReadByte()
        {
            int result = _underlyingStream.ReadByte();
            _progress.Report(Position);
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long result = _underlyingStream.Seek(offset, origin);
            _progress.Report(result);
            return result;
        }

        public override void SetLength(long value)
        {
            _underlyingStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _underlyingStream.Write(buffer, offset, count);
            _progress.Report(Position);
        }

        public override void WriteByte(byte value)
        {
            _underlyingStream.WriteByte(value);
            _progress.Report(Position);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _underlyingStream.Dispose();

            base.Dispose(disposing);
        }
    }
}
