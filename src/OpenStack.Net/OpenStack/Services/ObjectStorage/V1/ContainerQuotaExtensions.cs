namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Rackspace.Threading;
    using OpenStack.Net;

    /// <summary>
    /// This class provides extension methods for working with the optional container quotas
    /// feature in the Object Storage Service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ContainerQuotaExtensions
    {
        /// <summary>
        /// The name of the <c>Quota-Bytes</c> container metadata item.
        /// </summary>
        public static readonly string QuotaBytes = "Quota-Bytes";

        /// <summary>
        /// The name of the <c>Quota-Count</c> container metadata item.
        /// </summary>
        public static readonly string QuotaCount = "Quota-Count";

        /// <summary>
        /// Determines whether a particular Object Storage Service supports the optional Container Quotas feature.
        /// <note type="warning">This method relies on properties which are not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <remarks>
        /// If the Object Storage Service supports the Container Quotas feature, but does not support
        /// feature discoverability, this method might return <see langword="false"/> or result in an
        /// <see cref="HttpWebException"/> even though the Container Quotas feature is supported. To
        /// ensure this situation does not prevent the use of the Container Quotas feature, it is not
        /// automatically checked prior to sending the API calls associated with the feature.
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains a value
        /// indicating whether or not the service supports the Container Quotas feature.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<bool> SupportsContainerQuotasAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return service.GetObjectStorageInfoAsync(cancellationToken)
                .Select(task => task.Result.ContainsKey("container_quotas"));
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateContainerAsync"/>
        /// to include a quota on the total size of objects that can be stored in the container.
        /// </summary>
        /// <remarks>
        /// The Object Storage system uses an eventual consistency model. When you create a new object,
        /// the container size and object count might not be immediately updated. Consequently, you might
        /// be allowed to create objects even though you have actually exceeded the quota.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateContainerApiCall"/> HTTP API call.</param>
        /// <param name="size">The size, in bytes, of objects that can be stored in a container.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> WithSizeQuota(this Task<CreateContainerApiCall> task, long size)
        {
            return
                task.Select(
                    innerTask =>
                    {
                        task.Result.RequestMessage.Headers.Add(ContainerMetadata.ContainerMetadataPrefix + QuotaBytes, size.ToString());
                        return task.Result;
                    });
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateContainerAsync"/>
        /// to include a quota on the total number of objects that can be stored in the container.
        /// </summary>
        /// <remarks>
        /// The Object Storage system uses an eventual consistency model. When you create a new object,
        /// the container size and object count might not be immediately updated. Consequently, you might
        /// be allowed to create objects even though you have actually exceeded the quota.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateContainerApiCall"/> HTTP API call.</param>
        /// <param name="count">The maximum number of objects that can be stored in a container.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> WithObjectCountQuota(this Task<CreateContainerApiCall> task, long count)
        {
            return
                task.Select(
                    innerTask =>
                    {
                        task.Result.RequestMessage.Headers.Add(ContainerMetadata.ContainerMetadataPrefix + QuotaCount, count.ToString());
                        return task.Result;
                    });
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareUpdateContainerMetadataAsync"/>
        /// to include a quota on the total size of objects that can be stored in the container.
        /// </summary>
        /// <remarks>
        /// The Object Storage system uses an eventual consistency model. When you create a new object,
        /// the container size and object count might not be immediately updated. Consequently, you might
        /// be allowed to create objects even though you have actually exceeded the quota.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="UpdateContainerMetadataApiCall"/> HTTP API call.</param>
        /// <param name="size">The size, in bytes, of objects that can be stored in a container.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> WithSizeQuota(this Task<UpdateContainerMetadataApiCall> task, long size)
        {
            return
                task.Select(
                    innerTask =>
                    {
                        task.Result.RequestMessage.Headers.Add(ContainerMetadata.ContainerMetadataPrefix + QuotaBytes, size.ToString());
                        return task.Result;
                    });
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareUpdateContainerMetadataAsync"/>
        /// to include a quota on the total number of objects that can be stored in the container.
        /// </summary>
        /// <remarks>
        /// The Object Storage system uses an eventual consistency model. When you create a new object,
        /// the container size and object count might not be immediately updated. Consequently, you might
        /// be allowed to create objects even though you have actually exceeded the quota.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="UpdateContainerMetadataApiCall"/> HTTP API call.</param>
        /// <param name="count">The maximum number of objects that can be stored in a container.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> WithObjectCountQuota(this Task<UpdateContainerMetadataApiCall> task, long count)
        {
            return
                task.Select(
                    innerTask =>
                    {
                        task.Result.RequestMessage.Headers.Add(ContainerMetadata.ContainerMetadataPrefix + QuotaCount, count.ToString());
                        return task.Result;
                    });
        }

        /// <summary>
        /// Gets the size, in bytes, of objects that can be stored in a container. The information
        /// is extracted directly from a copy of the container metadata, rather than sending a new
        /// HTTP API call to determine the value.
        /// </summary>
        /// <remarks>
        /// The Object Storage system uses an eventual consistency model. When you create a new object,
        /// the container size and object count might not be immediately updated. Consequently, you might
        /// be allowed to create objects even though you have actually exceeded the quota.
        /// </remarks>
        /// <param name="metadata">A <see cref="ContainerMetadata"/> instance containing the metadata associated with a container.</param>
        /// <returns>
        /// The size, in bytes, of objects that can be stored in a container.
        /// <para>-or-</para>
        /// <para><see langword="null"/>If no size quota is set for the container.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="metadata"/> is <see langword="null"/>.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static long? GetSizeQuota(this ContainerMetadata metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            string value;
            if (!metadata.Metadata.TryGetValue(QuotaBytes, out value))
                return null;

            long result;
            if (!long.TryParse(value, out result))
                return null;

            return result;
        }

        /// <summary>
        /// Gets the maximum number of objects that can be stored in a container. The information
        /// is extracted directly from a copy of the container metadata, rather than sending a new
        /// HTTP API call to determine the value.
        /// </summary>
        /// <remarks>
        /// The Object Storage system uses an eventual consistency model. When you create a new object,
        /// the container size and object count might not be immediately updated. Consequently, you might
        /// be allowed to create objects even though you have actually exceeded the quota.
        /// </remarks>
        /// <param name="metadata">A <see cref="ContainerMetadata"/> instance containing the metadata associated with a container.</param>
        /// <returns>
        /// The maximum number of objects that can be stored in a container.
        /// <para>-or-</para>
        /// <para><see langword="null"/>If no object count quota is set for the container.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="metadata"/> is <see langword="null"/>.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static long? GetObjectCountQuota(this ContainerMetadata metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            string value;
            if (!metadata.Metadata.TryGetValue(QuotaCount, out value))
                return null;

            long result;
            if (!long.TryParse(value, out result))
                return null;

            return result;
        }

        /// <summary>
        /// Prepare an API call to set the quota associated with a container in the Object Storage service.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="size">
        /// The size, in bytes, of objects that can be stored in the container.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to leave the size quota, if any, for the container unchanged.</para>
        /// </param>
        /// <param name="count">
        /// The maximum number of objects that can be stored in the container.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to leave the object count quota, if any, for the container unchanged.</para>
        /// </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> PrepareSetContainerQuotaAsync(this IObjectStorageService service, ContainerName container, long? size, long? count, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            Dictionary<string, string> metadata = new Dictionary<string, string>();
            if (size != null)
                metadata[QuotaBytes] = size.ToString();
            if (count != null)
                metadata[QuotaCount] = count.ToString();

            return service.PrepareUpdateContainerMetadataAsync(container, new ContainerMetadata(new Dictionary<string, string>(), metadata), cancellationToken);
        }

        /// <summary>
        /// Prepare an API call to remove the quota, if any, associated with a container
        /// in the Object Storage service.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveContainerQuotaAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            string[] keys = { QuotaBytes, QuotaCount };
            return service.PrepareRemoveContainerMetadataAsync(container, keys, cancellationToken);
        }

        /// <summary>
        /// Gets the size, in bytes, of objects that can be stored in a container.
        /// </summary>
        /// <remarks>
        /// This is a convenience method combining the <see cref="ObjectStorageServiceExtensions.GetContainerMetadataAsync"/>
        /// and <see cref="GetSizeQuota"/> operations into a single asynchronous call. If a
        /// <see cref="ContainerMetadata"/> instance is already available for the container, e.g.
        /// from a call to <see cref="ObjectStorageServiceExtensions.ListObjectsAsync"/>,
        /// the <see cref="GetSizeQuota"/> method is more efficient than this method.
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the maximum the size, in bytes, of all objects stored in the container, or
        /// <see langword="null"/> if no size quota is set for the container.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<long?> GetContainerSizeQuotaAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.GetContainerMetadataAsync(container, cancellationToken)
                .Select(task => GetSizeQuota(task.Result));
        }

        /// <summary>
        /// Gets the maximum number of objects that can be stored in a container.
        /// </summary>
        /// <remarks>
        /// This is a convenience method combining the <see cref="ObjectStorageServiceExtensions.GetContainerMetadataAsync"/>
        /// and <see cref="GetObjectCountQuota"/> operations into a single asynchronous call. If a
        /// <see cref="ContainerMetadata"/> instance is already available for the container, e.g.
        /// from a call to <see cref="ObjectStorageServiceExtensions.ListObjectsAsync"/>,
        /// the <see cref="GetObjectCountQuota"/> method is more efficient than this method.
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the maximum number of items that can be stored in the container, or <see langword="null"/>
        /// if no object count quota is set for the container.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<long?> GetContainerObjectCountQuotaAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.GetContainerMetadataAsync(container, cancellationToken)
                .Select(task => GetObjectCountQuota(task.Result));
        }

        /// <summary>
        /// Set the quota associated with a container.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="size">
        /// The size, in bytes, of objects that can be stored in the container.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to leave the size quota, if any, for the container unchanged.</para>
        /// </param>
        /// <param name="count">
        /// The maximum number of objects that can be stored in the container.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to leave the object count quota, if any, for the container unchanged.</para>
        /// </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task SetContainerQuotaAsync(this IObjectStorageService service, ContainerName container, long? size, long? count, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (container == null)
                throw new ArgumentNullException("container");

            return CoreTaskExtensions.Using(
                () => service.PrepareSetContainerQuotaAsync(container, size, count, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Remove the quota, if any, associated with a container.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/container-quotas.html">Container quotas (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task RemoveContainerQuotaAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveContainerQuotaAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }
    }
}
