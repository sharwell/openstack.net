namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides extension methods for working with object versioning
    /// in the Object Storage Service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ObjectVersioningExtensions
    {
        /// <summary>
        /// Gets the name of the <c>X-Versions-Location</c> header used for specifying the
        /// name of the container where old versions of objects in a container get placed
        /// when the objects are updated.
        /// </summary>
        public static readonly string VersionsLocation = "X-Versions-Location";

        /// <summary>
        /// Gets the name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// </summary>
        /// <param name="metadata">A <see cref="ContainerMetadata"/> instance containing the metadata associated with a container.</param>
        /// <returns>
        /// The name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the specified <paramref name="metadata"/> indicates that the container is not a versioned container.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="metadata"/> is <see langword="null"/>.</exception>
        public static ContainerName GetVersionsLocation(this ContainerMetadata metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            string location;
            if (!metadata.Headers.TryGetValue(VersionsLocation, out location) || string.IsNullOrEmpty(location))
                return null;

            // first, URL-decode the value
            location = UriUtility.UriDecode(location);

            // then UTF-8 decode the value
            location = StorageMetadata.DecodeHeaderValue(location);

            // then return the result as a ContainerName
            return new ContainerName(location);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateContainerAsync"/>
        /// to include the <see cref="VersionsLocation"/> header, which specifies the name of the container
        /// where old versions of objects in a versioned container get placed when the objects are updated.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateContainerApiCall"/> HTTP API call.</param>
        /// <param name="versionsLocation">
        /// The name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="task"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="versionsLocation"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> WithVersionsLocation(this Task<CreateContainerApiCall> task, ContainerName versionsLocation)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (versionsLocation == null)
                throw new ArgumentNullException("versionsLocation");

            return task.WithVersionsLocationImpl(versionsLocation);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareUpdateContainerMetadataAsync"/>
        /// to include the <see cref="VersionsLocation"/> header, which specifies the name of the container
        /// where old versions of objects in a versioned container get placed when the objects are updated.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="UpdateContainerMetadataApiCall"/> HTTP API call.</param>
        /// <param name="versionsLocation">
        /// The name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="VersionsLocation"/> header associated with a container.</para>
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> WithVersionsLocation(this Task<UpdateContainerMetadataApiCall> task, ContainerName versionsLocation)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            // allow location to be null as a way to remove the X-Versions-Location header

            return task.WithVersionsLocationImpl(versionsLocation);
        }

        /// <summary>
        /// Prepare an API call to set the name of the container where old versions of objects in a
        /// versioned container get placed when the objects are updated.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The name of the container to update.</param>
        /// <param name="versionsLocation">
        /// The name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="VersionsLocation"/> header associated with a container.</para>
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
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> PrepareSetVersionsLocationAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, CancellationToken.None)
                .WithVersionsLocation(versionsLocation);
        }

        /// <summary>
        /// Prepare an API call to remove the <see cref="VersionsLocation"/> header, if any, associated with
        /// a container.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The name of the container to update.</param>
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
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveVersionsLocationAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, CancellationToken.None)
                .WithVersionsLocation(null);
        }

        /// <summary>
        /// Create a versioned container in the Object Storage Service, which places a copy of old versions
        /// of objects in a separate container whenever the objects are updated.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The name of the container to update.</param>
        /// <param name="versionsLocation">
        /// The name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
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
        /// <para>-or-</para>
        /// <para>If <paramref name="versionsLocation"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task CreateVersionedContainerAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateContainerAsync(container, cancellationToken).WithVersionsLocation(versionsLocation),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Gets the name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// </summary>
        /// <remarks>
        /// This is a convenience method combining the <see cref="ObjectStorageServiceExtensions.GetContainerMetadataAsync"/>
        /// and <see cref="GetVersionsLocation"/> operations into a single asynchronous call. If a
        /// <see cref="ContainerMetadata"/> instance is already available for the container, e.g.
        /// from a call to <see cref="ObjectStorageServiceExtensions.ListObjectsAsync"/>,
        /// the <see cref="GetVersionsLocation"/> method is more efficient than this method.
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated, or <see langword="null"/>
        /// if the <see cref="VersionsLocation"/> header is not set for the container.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ContainerName> GetVersionsLocationAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.GetContainerMetadataAsync(container, cancellationToken)
                .Select(task => GetVersionsLocation(task.Result));
        }

        /// <summary>
        /// Sets the name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="versionsLocation">
        /// The name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="VersionsLocation"/> header associated with a container.</para>
        /// </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task SetVersionsLocationAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetVersionsLocationAsync(container, versionsLocation, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Remove the <see cref="VersionsLocation"/> header, if any, which is associated with a container.
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
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task RemoveVersionsLocationAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveVersionsLocationAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Update a generic HTTP API call to include the <see cref="VersionsLocation"/> header, which
        /// specifies the name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare the HTTP API call.</param>
        /// <param name="location">
        /// The name of the container where old versions of objects in a versioned
        /// container get placed when the objects are updated.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="VersionsLocation"/> header associated with a container.</para>
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/set-object-versions.html">Object versioning (OpenStack Object Storage API V1 Reference)</seealso>
        private static Task<TCall> WithVersionsLocationImpl<TCall>(this Task<TCall> task, ContainerName location)
            where TCall : IHttpApiRequest
        {
            string encodedLocation = string.Empty;
            if (location != null)
            {
                encodedLocation = location.Value;

                // first, UTF-8 encode the value
                encodedLocation = StorageMetadata.EncodeHeaderValue(encodedLocation);

                // then, URL-encode the value
                encodedLocation = UriUtility.UriEncode(encodedLocation, UriPart.Any);
            }

            return
                task
                .Select(
                    innerTask =>
                    {
                        innerTask.Result.RequestMessage.Headers.Add(VersionsLocation, encodedLocation);
                        return innerTask.Result;
                    });
        }
    }
}
