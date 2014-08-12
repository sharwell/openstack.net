namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class contains extension methods to support the <c>X-Newest</c> header
    /// in various requests to the <see cref="IObjectStorageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class NewestExtensions
    {
        /// <summary>
        /// The name of the <c>X-Newest</c> header.
        /// </summary>
        /// <see href=""></see>
        public static readonly string Newest = "X-Newest";

        /// <summary>
        /// Updates the HTTP API call created by <see cref="IObjectStorageService.PrepareGetAccountMetadataAsync"/>
        /// to include the <c>X-Newest</c> header. The value of the header is <c>true</c>.
        /// </summary>
        /// <remarks>
        /// If <c>X-Newest</c> is set to <c>true</c>, Object Storage queries all replicas to return the most recent
        /// one. If you omit this header, Object Storage responds faster after it finds one valid replica. Because
        /// setting this header to <c>true</c> is more expensive for the back end, use it only when it is absolutely
        /// needed.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare an HTTP API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<GetAccountMetadataApiCall> WithNewest(this Task<GetAccountMetadataApiCall> task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithNewestImpl();
        }

        /// <summary>
        /// Updates the HTTP API call created by <see cref="IObjectStorageService.PrepareGetContainerMetadataAsync"/>
        /// to include the <c>X-Newest</c> header. The value of the header is <c>true</c>.
        /// </summary>
        /// <remarks>
        /// If <c>X-Newest</c> is set to <c>true</c>, Object Storage queries all replicas to return the most recent
        /// one. If you omit this header, Object Storage responds faster after it finds one valid replica. Because
        /// setting this header to <c>true</c> is more expensive for the back end, use it only when it is absolutely
        /// needed.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare an HTTP API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<GetContainerMetadataApiCall> WithNewest(this Task<GetContainerMetadataApiCall> task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithNewestImpl();
        }

        /// <summary>
        /// Updates the HTTP API call created by <see cref="IObjectStorageService.PrepareGetObjectMetadataAsync"/>
        /// to include the <c>X-Newest</c> header. The value of the header is <c>true</c>.
        /// </summary>
        /// <remarks>
        /// If <c>X-Newest</c> is set to <c>true</c>, Object Storage queries all replicas to return the most recent
        /// one. If you omit this header, Object Storage responds faster after it finds one valid replica. Because
        /// setting this header to <c>true</c> is more expensive for the back end, use it only when it is absolutely
        /// needed.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare an HTTP API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<GetObjectMetadataApiCall> WithNewest(this Task<GetObjectMetadataApiCall> task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithNewestImpl();
        }

        /// <summary>
        /// Updates the HTTP API call created by <see cref="IObjectStorageService.PrepareGetObjectAsync"/>
        /// to include the <c>X-Newest</c> header. The value of the header is <c>true</c>.
        /// </summary>
        /// <remarks>
        /// If <c>X-Newest</c> is set to <c>true</c>, Object Storage queries all replicas to return the most recent
        /// one. If you omit this header, Object Storage responds faster after it finds one valid replica. Because
        /// setting this header to <c>true</c> is more expensive for the back end, use it only when it is absolutely
        /// needed.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare an HTTP API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<GetObjectApiCall> WithNewest(this Task<GetObjectApiCall> task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithNewestImpl();
        }

        /// <summary>
        /// Updates the HTTP API call created by <see cref="IObjectStorageService.PrepareListContainersAsync"/>
        /// to include the <c>X-Newest</c> header. The value of the header is <c>true</c>.
        /// </summary>
        /// <remarks>
        /// If <c>X-Newest</c> is set to <c>true</c>, Object Storage queries all replicas to return the most recent
        /// one. If you omit this header, Object Storage responds faster after it finds one valid replica. Because
        /// setting this header to <c>true</c> is more expensive for the back end, use it only when it is absolutely
        /// needed.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare an HTTP API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<ListContainersApiCall> WithNewest(this Task<ListContainersApiCall> task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithNewestImpl();
        }

        /// <summary>
        /// Updates the HTTP API call created by <see cref="IObjectStorageService.PrepareListObjectsAsync"/>
        /// to include the <c>X-Newest</c> header. The value of the header is <c>true</c>.
        /// </summary>
        /// <remarks>
        /// If <c>X-Newest</c> is set to <c>true</c>, Object Storage queries all replicas to return the most recent
        /// one. If you omit this header, Object Storage responds faster after it finds one valid replica. Because
        /// setting this header to <c>true</c> is more expensive for the back end, use it only when it is absolutely
        /// needed.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare an HTTP API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<ListObjectsApiCall> WithNewest(this Task<ListObjectsApiCall> task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithNewestImpl();
        }

        /// <summary>
        /// Updates an arbitrary <see cref="IHttpApiRequest"/> to include the <c>X-Newest</c> header.
        /// The value of the header is <c>true</c>.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare an HTTP API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        private static Task<TCall> WithNewestImpl<TCall>(this Task<TCall> task)
            where TCall : IHttpApiRequest
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.Select(
                innerTask =>
                {
                    innerTask.Result.RequestMessage.Headers.Remove(Newest);
                    innerTask.Result.RequestMessage.Headers.Add(Newest, "true");
                    return innerTask.Result;
                });
        }
    }
}
