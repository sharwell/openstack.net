namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides extension methods for using the object Scheduled Deletion
    /// functionality in the OpenStack Object Storage Service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ScheduledDeletionExtensions
    {
        /// <summary>
        /// The name of the <c>X-Delete-After</c> header used to specify the number
        /// of seconds in the future when an object should be deleted.
        /// </summary>
        public static readonly string DeleteAfter = "X-Delete-After";

        /// <summary>
        /// The name of the <c>X-Delete-At</c> header used to specify the time
        /// when an object should be deleted, as a Unix epoch timestamp.
        /// </summary>
        public static readonly string DeleteAt = "X-Delete-At";

        /// <summary>
        /// The time used as a reference for converting <see cref="DateTimeOffset"/> values
        /// to Unix epoch timestamp values.
        /// </summary>
        private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateObjectAsync"/>
        /// to include the <seealso cref="DeleteAfter"/> header, which specifies that the object should be
        /// automatically removed after a specified amount of time.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateObjectApiCall"/> HTTP API call.</param>
        /// <param name="timeSpan">The time span after which the object should be automatically removed.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateObjectApiCall> WithDeleteAfter(this Task<CreateObjectApiCall> task, TimeSpan timeSpan)
        {
            return task.WithDeleteAfterImpl(timeSpan);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCopyObjectAsync"/>
        /// to include the <seealso cref="DeleteAfter"/> header, which specifies that the copy should be
        /// automatically removed after a specified amount of time.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CopyObjectApiCall"/> HTTP API call.</param>
        /// <param name="timeSpan">The time span after which the object should be automatically removed.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CopyObjectApiCall> WithDeleteAfter(this Task<CopyObjectApiCall> task, TimeSpan timeSpan)
        {
            return task.WithDeleteAfterImpl(timeSpan);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareSetObjectMetadataAsync"/>
        /// to include the <seealso cref="DeleteAfter"/> header, which specifies that the object should be
        /// automatically removed after a specified amount of time.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="SetObjectMetadataApiCall"/> HTTP API call.</param>
        /// <param name="timeSpan">The time span after which the object should be automatically removed.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<SetObjectMetadataApiCall> WithDeleteAfter(this Task<SetObjectMetadataApiCall> task, TimeSpan timeSpan)
        {
            return task.WithDeleteAfterImpl(timeSpan);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateObjectAsync"/>
        /// to include the <seealso cref="DeleteAt"/> header, which specifies that the object should be
        /// automatically removed at a particular time.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateObjectApiCall"/> HTTP API call.</param>
        /// <param name="time">A timestamp indicating when object should be automatically removed.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateObjectApiCall> WithDeleteAt(this Task<CreateObjectApiCall> task, DateTimeOffset time)
        {
            return task.WithDeleteAtImpl(time);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCopyObjectAsync"/>
        /// to include the <seealso cref="DeleteAt"/> header, which specifies that the copy should be
        /// automatically removed at a particular time.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CopyObjectApiCall"/> HTTP API call.</param>
        /// <param name="time">A timestamp indicating when object should be automatically removed.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CopyObjectApiCall> WithDeleteAt(this Task<CopyObjectApiCall> task, DateTimeOffset time)
        {
            return task.WithDeleteAtImpl(time);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareSetObjectMetadataAsync"/>
        /// to include the <seealso cref="DeleteAt"/> header, which specifies that the object should be
        /// automatically removed at a particular time.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="SetObjectMetadataApiCall"/> HTTP API call.</param>
        /// <param name="time">A timestamp indicating when object should be automatically removed.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<SetObjectMetadataApiCall> WithDeleteAt(this Task<SetObjectMetadataApiCall> task, DateTimeOffset time)
        {
            return task.WithDeleteAtImpl(time);
        }

        /// <summary>
        /// Gets a timestamp indicating when an object is scheduled for automatic removal.
        /// The information is extracted directly from a copy of the object metadata, rather than
        /// sending a new HTTP API call to determine the value.
        /// </summary>
        /// <remarks>
        /// This method only examines the <see cref="DeleteAt"/> header for an object, since
        /// the <see cref="DeleteAfter"/> header is converted to a <see cref="DeleteAt"/> header
        /// by the Object Storage Service.
        /// </remarks>
        /// <param name="metadata">An <see cref="ObjectMetadata"/> instance containing the metadata associated with an object.</param>
        /// <returns>
        /// A <seealso cref="DateTimeOffset"/> indicating when the object will be automatically removed.
        /// <para>-or-</para>
        /// <para><see langword="null"/>If no value is set for the <see cref="DeleteAt"/> metadata for the object.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="metadata"/> is <see langword="null"/>.</exception>
        /// <seealso cref="DeleteAt"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static DateTimeOffset? GetScheduledDeletionTime(this ObjectMetadata metadata)
        {
            string stringValue;
            if (!metadata.Headers.TryGetValue(DeleteAt, out stringValue))
                return null;

            int timestamp;
            if (!int.TryParse(stringValue, out timestamp))
                return null;

            return Epoch.AddSeconds(timestamp);
        }

        /// <summary>
        /// Gets a timestamp indicating when an object is scheduled for automatic removal.
        /// </summary>
        /// <remarks>
        /// This is a convenience method combining the <see cref="ObjectStorageServiceExtensions.GetObjectMetadataAsync"/>
        /// and <see cref="GetScheduledDeletionTime"/> operations into a single asynchronous call. If an
        /// <see cref="ObjectMetadata"/> instance is already available for the object, e.g.
        /// from a call to <see cref="ObjectStorageServiceExtensions.GetObjectMetadataAsync"/>,
        /// the <see cref="GetScheduledDeletionTime"/> method is more efficient than this method.
        ///
        /// <note>
        /// This method only examines the <see cref="DeleteAt"/> header for an object, since
        /// the <see cref="DeleteAfter"/> header is converted to a <see cref="DeleteAt"/> header
        /// by the Object Storage Service.
        /// </note>
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="object">The object name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <seealso cref="DateTimeOffset"/> indicating when the object will be automatically removed.
        /// <para>-or-</para>
        /// <para><see langword="null"/>If no value is set for the <see cref="DeleteAt"/> metadata for the object.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="object"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <seealso cref="ObjectStorageServiceExtensions.GetObjectMetadataAsync"/>
        /// <seealso cref="GetScheduledDeletionTime"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<DateTimeOffset?> GetScheduledDeletionTimeAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            return service.GetObjectMetadataAsync(container, @object, cancellationToken)
                .Select(task => GetScheduledDeletionTime(task.Result));
        }

        /// <summary>
        /// Schedule an object for automatic removal after a specified amount of time has elapsed.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="object">The object name.</param>
        /// <param name="timeSpan">The time span after which the object should be automatically removed.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="object"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task SetDeleteAfterAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, TimeSpan timeSpan, CancellationToken cancellationToken)
        {
            var headers =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { DeleteAfter, ((int)timeSpan.TotalSeconds).ToString() }
                };
            ObjectMetadata metadata = new ObjectMetadata(headers, new Dictionary<string, string>());
            return service.UpdateObjectMetadataAsync(container, @object, metadata, cancellationToken);
        }

        /// <summary>
        /// Schedule an object for automatic removal at a particular time.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="object">The object name.</param>
        /// <param name="time">A timestamp indicating when object should be automatically removed.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="object"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task SetDeleteAtAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, DateTimeOffset time, CancellationToken cancellationToken)
        {
            var headers =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { DeleteAt, ((int)(time - Epoch).TotalSeconds).ToString() }
                };
            ObjectMetadata metadata = new ObjectMetadata(headers, new Dictionary<string, string>());
            return service.UpdateObjectMetadataAsync(container, @object, metadata, cancellationToken);
        }

        /// <summary>
        /// Remove the scheduled removal of an object, provided the object has not already been removed.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="object">The object name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="object"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task RemoveScheduledDeletionTimeAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            var headers =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { DeleteAt, string.Empty }
                };
            ObjectMetadata metadata = new ObjectMetadata(headers, new Dictionary<string, string>());
            return service.UpdateObjectMetadataAsync(container, @object, metadata, cancellationToken);
        }

        /// <summary>
        /// Update a generic HTTP API call to include the <seealso cref="DeleteAfter"/> header, which
        /// specifies that an object should be automatically removed after a specified amount of time.
        /// </summary>
        /// <typeparam name="TCall">The type of the HTTP API request.</typeparam>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare the HTTP API call.</param>
        /// <param name="timeSpan">The time span after which the object should be automatically removed.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        private static Task<TCall> WithDeleteAfterImpl<TCall>(this Task<TCall> task, TimeSpan timeSpan)
            where TCall : IHttpApiRequest
        {
            return task.Select(
                innerTask =>
                {
                    task.Result.RequestMessage.Headers.Remove(DeleteAfter);
                    task.Result.RequestMessage.Headers.Add(DeleteAfter, ((int)timeSpan.TotalSeconds).ToString());
                    return task.Result;
                });
        }

        /// <summary>
        /// Update a generic HTTP API call to include the <seealso cref="DeleteAt"/> header, which
        /// specifies that an object should be automatically removed at a particular time.
        /// </summary>
        /// <typeparam name="TCall">The type of the HTTP API request.</typeparam>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare the HTTP API call.</param>
        /// <param name="time">A timestamp indicating when object should be automatically removed.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/expire-objects.html">Schedule objects for deletion (OpenStack Object Storage API V1 Reference)</seealso>
        private static Task<TCall> WithDeleteAtImpl<TCall>(this Task<TCall> task, DateTimeOffset time)
            where TCall : IHttpApiRequest
        {
            return task.Select(
                innerTask =>
                {
                    int timestamp = (int)((time - Epoch).TotalSeconds);
                    task.Result.RequestMessage.Headers.Remove(DeleteAt);
                    task.Result.RequestMessage.Headers.Add(DeleteAt, timestamp.ToString());
                    return task.Result;
                });
        }
    }
}
