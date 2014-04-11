namespace net.openstack.Core.Compat
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public static class StreamExtensions
    {
        public static Task CopyToAsync(this Stream stream, Stream destination)
        {
            return CopyToAsync(stream, destination, 16 * 1024, CancellationToken.None);
        }

        public static Task CopyToAsync(this Stream stream, Stream destination, int bufferSize)
        {
            return CopyToAsync(stream, destination, bufferSize, CancellationToken.None);
        }

        public static Task CopyToAsync(this Stream stream, Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (!stream.CanRead)
                throw new NotSupportedException("The stream does not support reading");
            if (!destination.CanWrite)
                throw new NotSupportedException("The destination does not support writing");
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize");

            if (cancellationToken.IsCancellationRequested)
                return InternalTaskExtensions.CanceledTask();

            return CopyToAsync(stream, destination, new byte[bufferSize], cancellationToken);
        }

        private static Task CopyToAsync(Stream stream, Stream destination, byte[] buffer, CancellationToken cancellationToken)
        {
            int nread = 0;
            Func<Task<bool>> condition =
                () => stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)
                    .Select(task => (nread = task.Result) != 0);
            Func<Task> body = () => destination.WriteAsync(buffer, 0, nread, cancellationToken);

            return CoreTaskExtensions.While(condition, body);
        }

        public static Task FlushAsync(this Stream stream)
        {
            return FlushAsync(stream, CancellationToken.None);
        }

        public static Task FlushAsync(this Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (cancellationToken.IsCancellationRequested)
                return InternalTaskExtensions.CanceledTask();

            return Task.Factory.StartNew(state => ((Stream)state).Flush(), stream, cancellationToken);
        }

        public static Task<int> ReadAsync(this Stream stream, byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return InternalTaskExtensions.CanceledTask<int>();

            return Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, offset, count, null);
        }

        public static Task WriteAsync(this Stream stream, byte[] buffer, int offset, int count)
        {
            return WriteAsync(stream, buffer, offset, count, CancellationToken.None);
        }

        public static Task WriteAsync(this Stream stream, byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, offset, count, null);
        }
    }
}
