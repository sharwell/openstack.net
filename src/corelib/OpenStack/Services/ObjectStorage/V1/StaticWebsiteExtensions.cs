namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides extension methods for working with the optional Static Website
    /// functionality of the OpenStack Object Storage Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class StaticWebsiteExtensions
    {
        /// <summary>
        /// The name of the <c>Web-Index</c> container metadata item used to specify the name of
        /// the index page to serve when browsing a container as a website.
        /// </summary>
        public static readonly string WebIndex = "Web-Index";

        /// <summary>
        /// The name of the <c>Web-Error</c> container metadata item used to specify the base name of
        /// the error pages to serve when browsing an object storage container as a website.
        /// </summary>
        public static readonly string WebError = "Web-Error";

        /// <summary>
        /// The name of the <c>Web-Listings</c> container metadata item used to specify whether or not
        /// a static website supports listing objects in a container when no index
        /// page is present.
        /// </summary>
        public static readonly string WebListings = "Web-Listings";

        /// <summary>
        /// The name of the <c>Web-Listings-CSS</c> container metadata item used to specify the name of
        /// the custom CSS file to use when browsing a file listing.
        /// </summary>
        public static readonly string WebListingsCss = "Web-Listings-CSS";

        /// <summary>
        /// Determines whether a particular Object Storage Service supports the optional Static Website feature.
        /// <note type="warning">This method relies on properties which are not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <remarks>
        /// If the Object Storage Service supports the Static Website feature, but does not support
        /// feature discoverability, this method might return <see langword="false"/> or result in an
        /// <see cref="HttpWebException"/> even though the Static Website feature is supported. To
        /// ensure this situation does not prevent the use of the Static Website feature, it is not
        /// automatically checked prior to sending the API calls associated with the feature.
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains a value
        /// indicating whether or not the service supports the Static Website feature.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<bool> SupportsStaticWebsiteAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return service.GetObjectStorageInfoAsync(cancellationToken)
                .Select(task => task.Result.ContainsKey("staticweb"));
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateContainerAsync"/>
        /// to include the <see cref="WebIndex"/> metadata, which specifies the name of the index file (or
        /// default page served, such as <c>index.html</c>) displayed for the website.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateContainerApiCall"/> HTTP API call.</param>
        /// <param name="indexPage">
        /// The name of the default object to serve when a user visits a pseudo-directory in the static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="WebIndex"/> metadata associated with a container.</para>
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> WithWebIndex(this Task<CreateContainerApiCall> task, ObjectName indexPage)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithContainerMetadata(WebIndex, indexPage != null ? indexPage.Value : string.Empty);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateContainerAsync"/>
        /// to include the <see cref="WebError"/> metadata, which specifies the suffix of the object to serve in
        /// the event of an error.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateContainerApiCall"/> HTTP API call.</param>
        /// <param name="errorPage">
        /// The suffix of the object to serve when an error occurs when a user visits a pseudo-directory or object in the static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="WebError"/> metadata associated with a container.</para>
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> WithWebError(this Task<CreateContainerApiCall> task, ObjectName errorPage)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithContainerMetadata(WebError, errorPage != null ? errorPage.Value : null);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateContainerAsync"/>
        /// to include the <see cref="WebListings"/> metadata, which specifies whether or not a request to get
        /// a pseudo-directory of the static website should return a list of objects in the pseudo-directory if
        /// the pseudo-directory does not contain the index page indicated by the <see cref="WebIndex"/> metadata.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateContainerApiCall"/> HTTP API call.</param>
        /// <param name="enableListings"><see langword="true"/> to enable directory listings on the static website; otherwise, <see langword="false"/>.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> WithWebListings(this Task<CreateContainerApiCall> task, bool enableListings)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            if (enableListings)
                return task.WithContainerMetadata(WebListings, "TRUE");
            else
                return task.WithContainerMetadata(WebListings, string.Empty);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareCreateContainerAsync"/>
        /// to include the <see cref="WebListingsCss"/> metadata, which specifies the name of the CSS stylesheet
        /// to reference in directory listings on the static website.
        /// </summary>
        /// <remarks>
        /// This value may be configured, but is not used if web listings are not enabled for the static website.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="CreateContainerApiCall"/> HTTP API call.</param>
        /// <param name="listingsCssPage">
        /// The name of the object to include as the CSS stylesheet when returning a directory listing on the static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="WebListingsCss"/> metadata associated with a container.</para>
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> WithWebListingsStylesheet(this Task<CreateContainerApiCall> task, ObjectName listingsCssPage)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithContainerMetadata(WebListingsCss, listingsCssPage != null ? listingsCssPage.Value : string.Empty);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareUpdateContainerMetadataAsync"/>
        /// to include the <see cref="WebIndex"/> metadata, which specifies the name of the index file (or
        /// default page served, such as <c>index.html</c>) displayed for the website.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="UpdateContainerMetadataApiCall"/> HTTP API call.</param>
        /// <param name="indexPage">
        /// The name of the default object to serve when a user visits a pseudo-directory in the static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="WebIndex"/> metadata associated with a container.</para>
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> WithWebIndex(this Task<UpdateContainerMetadataApiCall> task, ObjectName indexPage)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithContainerMetadata(WebIndex, indexPage != null ? indexPage.Value : string.Empty);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareUpdateContainerMetadataAsync"/>
        /// to include the <see cref="WebError"/> metadata, which specifies the suffix of the object to serve in
        /// the event of an error.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="UpdateContainerMetadataApiCall"/> HTTP API call.</param>
        /// <param name="errorPage">
        /// The suffix of the object to serve when an error occurs when a user visits a pseudo-directory or object in the static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="WebError"/> metadata associated with a container.</para>
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> WithWebError(this Task<UpdateContainerMetadataApiCall> task, ObjectName errorPage)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithContainerMetadata(WebError, errorPage != null ? errorPage.Value : string.Empty);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareUpdateContainerMetadataAsync"/>
        /// to include the <see cref="WebListings"/> metadata, which specifies whether or not a request to get
        /// a pseudo-directory of the static website should return a list of objects in the pseudo-directory if
        /// the pseudo-directory does not contain the index page indicated by the <see cref="WebIndex"/> metadata.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="UpdateContainerMetadataApiCall"/> HTTP API call.</param>
        /// <param name="enableListings"><see langword="true"/> to enable directory listings on the static website; otherwise, <see langword="false"/>.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> WithWebListings(this Task<UpdateContainerMetadataApiCall> task, bool enableListings)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            if (enableListings)
                return task.WithContainerMetadata(WebListings, "TRUE");
            else
                return task.WithContainerMetadata(WebListings, string.Empty);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareUpdateContainerMetadataAsync"/>
        /// to include the <see cref="WebListingsCss"/> metadata, which specifies the name of the CSS stylesheet
        /// to reference in directory listings on the static website.
        /// </summary>
        /// <remarks>
        /// This value may be configured, but is not used if web listings are not enabled for the static website.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="UpdateContainerMetadataApiCall"/> HTTP API call.</param>
        /// <param name="listingsCssPage">
        /// The name of the object to include as the CSS stylesheet when returning a directory listing on the static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to clear the value for the <see cref="WebListingsCss"/> metadata associated with a container.</para>
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<UpdateContainerMetadataApiCall> WithWebListingsStylesheet(this Task<UpdateContainerMetadataApiCall> task, ObjectName listingsCssPage)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithContainerMetadata(WebListingsCss, listingsCssPage != null ? listingsCssPage.Value : string.Empty);
        }

        /// <summary>
        /// Gets the name of the default object to serve when a user visits a pseudo-directory in the
        /// static website. The information is extracted directly from a copy of the container metadata,
        /// rather than sending a new HTTP API call to determine the value.
        /// </summary>
        /// <param name="metadata">A <see cref="ContainerMetadata"/> instance containing the metadata associated with a container.</param>
        /// <returns>
        /// The the name of the default object to serve when a user visits a pseudo-directory in the
        /// static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/>If no value is set for the <see cref="WebIndex"/> metadata for the container.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="metadata"/> is <see langword="null"/>.</exception>
        /// <seealso cref="WebIndex"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static ObjectName GetWebIndexPage(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(WebIndex, out value) || string.IsNullOrEmpty(value))
                return null;

            return new ObjectName(value);
        }

        /// <summary>
        /// Gets the suffix of the object to serve when an error occurs when a user visits a pseudo-directory
        /// or object in the static website. The information is extracted directly from a copy of the container
        /// metadata, rather than sending a new HTTP API call to determine the value.
        /// </summary>
        /// <param name="metadata">A <see cref="ContainerMetadata"/> instance containing the metadata associated with a container.</param>
        /// <returns>
        /// The suffix of the object to serve when an error occurs when a user visits a pseudo-directory
        /// or object in the static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/>If no value is set for the <see cref="WebError"/> metadata for the container.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="metadata"/> is <see langword="null"/>.</exception>
        /// <seealso cref="WebError"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static ObjectName GetWebErrorPage(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(WebError, out value) || string.IsNullOrEmpty(value))
                return null;

            return new ObjectName(value);
        }

        /// <summary>
        /// Gets a value indicating whether directory listings are enabled on a static website. The information
        /// is extracted directly from a copy of the container metadata, rather than sending a new
        /// HTTP API call to determine the value.
        /// </summary>
        /// <param name="metadata">A <see cref="ContainerMetadata"/> instance containing the metadata associated with a container.</param>
        /// <returns><see langword="true"/> if directory listings are enabled on the static website; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="metadata"/> is <see langword="null"/>.</exception>
        /// <seealso cref="WebListings"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static bool GetWebListingsEnabled(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(WebListings, out value))
                return false;

            return string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the name of the CSS stylesheet to reference in directory listings on the static website.
        /// The information is extracted directly from a copy of the container metadata, rather than
        /// sending a new HTTP API call to determine the value.
        /// </summary>
        /// <param name="metadata">A <see cref="ContainerMetadata"/> instance containing the metadata associated with a container.</param>
        /// <returns>
        /// The the name of the CSS stylesheet to reference in directory listings on the static website.
        /// <para>-or-</para>
        /// <para><see langword="null"/>If no value is set for the <see cref="WebListingsCss"/> metadata for the container.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="metadata"/> is <see langword="null"/>.</exception>
        /// <seealso cref="WebListingsCss"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static ObjectName GetWebListingsStylesheet(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(WebListingsCss, out value) || string.IsNullOrEmpty(value))
                return null;

            return new ObjectName(value);
        }

        /// <summary>
        /// Prepare an API call to create a container in the Object Storage service which is preconfigured as a static website.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="indexPage">The name of the default object to serve when a user visits a pseudo-directory in the static website.</param>
        /// <param name="errorPage">The suffix of the object to serve when an error occurs when a user visits a pseudo-directory or object in the static website.</param>
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
        /// <para>If <paramref name="indexPage"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="errorPage"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="CreateContainerApiCall"/>
        /// <seealso cref="ObjectStorageServiceExtensions.CreateContainerAsync"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/PUT_createContainer__v1__account___container__storage_container_services.html">Create container (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> PrepareCreateStaticWebsiteContainerAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, ObjectName errorPage, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (container == null)
                throw new ArgumentNullException("container");
            if (indexPage == null)
                throw new ArgumentNullException("indexPage");
            if (errorPage == null)
                throw new ArgumentNullException("errorPage");

            return service.PrepareCreateContainerAsync(container, cancellationToken)
                .WithWebIndex(indexPage)
                .WithWebError(errorPage);
        }

        /// <summary>
        /// Prepare an API call to create a container in the Object Storage service which is preconfigured as a static website.
        /// Directory listings are enabled for the resulting container, with the name of the stylesheet provided as a parameter.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="indexPage">The name of the default object to serve when a user visits a pseudo-directory in the static website.</param>
        /// <param name="errorPage">The suffix of the object to serve when an error occurs when a user visits a pseudo-directory or object in the static website.</param>
        /// <param name="listingsCssPage">The name of the object to include as the CSS stylesheet when returning a directory listing on the static website.</param>
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
        /// <para>If <paramref name="indexPage"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="errorPage"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="listingsCssPage"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="CreateContainerApiCall"/>
        /// <seealso cref="ObjectStorageServiceExtensions.CreateContainerAsync"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/PUT_createContainer__v1__account___container__storage_container_services.html">Create container (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/static-website.html">Create static website (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<CreateContainerApiCall> PrepareCreateStaticWebsiteContainerAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, ObjectName errorPage, ObjectName listingsCssPage, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (container == null)
                throw new ArgumentNullException("container");
            if (indexPage == null)
                throw new ArgumentNullException("indexPage");
            if (errorPage == null)
                throw new ArgumentNullException("errorPage");
            if (listingsCssPage == null)
                throw new ArgumentNullException("listingsCssPage");

            return service.PrepareCreateStaticWebsiteContainerAsync(container, indexPage, errorPage, cancellationToken)
                .WithWebListings(true)
                .WithWebListingsStylesheet(listingsCssPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetStaticWebsiteAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, ObjectName errorPage, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebIndex(indexPage)
                .WithWebError(errorPage)
                .WithWebListings(false)
                .WithContainerMetadata(WebListingsCss, string.Empty);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetStaticWebsiteAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, ObjectName errorPage, ObjectName listingsCssPage, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebIndex(indexPage)
                .WithWebError(errorPage)
                .WithWebListings(true)
                .WithWebListingsStylesheet(listingsCssPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetWebIndexAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebIndex(indexPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveWebIndexAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithContainerMetadata(WebIndex, string.Empty);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetWebErrorAsync(this IObjectStorageService service, ContainerName container, ObjectName errorPage, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebError(errorPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveWebErrorAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithContainerMetadata(WebError, string.Empty);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetWebListingsStylesheetAsync(this IObjectStorageService service, ContainerName container, ObjectName listingsCssPage, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebListings(true)
                .WithWebListingsStylesheet(listingsCssPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveWebListingsStylesheetAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebListings(false)
                .WithContainerMetadata(WebListingsCss, string.Empty);
        }

        public static Task CreateStaticWebsiteContainerAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, ObjectName errorPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateStaticWebsiteContainerAsync(container, indexPage, errorPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task CreateStaticWebsiteContainerAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, ObjectName errorPage, ObjectName listingsCssPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateStaticWebsiteContainerAsync(container, indexPage, errorPage, listingsCssPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetStaticWebsiteAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, ObjectName errorPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetStaticWebsiteAsync(container, indexPage, errorPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetStaticWebsiteAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, ObjectName errorPage, ObjectName listingsCssPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetStaticWebsiteAsync(container, indexPage, errorPage, listingsCssPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetWebIndexAsync(this IObjectStorageService service, ContainerName container, ObjectName indexPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetWebIndexAsync(container, indexPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveWebIndexAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveWebIndexAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetWebErrorAsync(this IObjectStorageService service, ContainerName container, ObjectName errorPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetWebErrorAsync(container, errorPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveWebErrorAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveWebErrorAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetWebListingsStylesheetAsync(this IObjectStorageService service, ContainerName container, ObjectName listingsCssPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetWebListingsStylesheetAsync(container, listingsCssPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveWebListingsStylesheetAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveWebListingsStylesheetAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        private static Task<TCall> WithContainerMetadata<TCall>(this Task<TCall> task, string metadata, string value)
            where TCall : IHttpApiRequest
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (metadata == null)
                throw new ArgumentNullException("metadata");
            if (value == null)
                throw new ArgumentNullException("value");
            if (string.IsNullOrEmpty(metadata))
                throw new ArgumentException("metadata cannot be empty", "metadata");
            // allow empty value as a means of removing the metadata

            return
                task.Select(
                    innerTask =>
                    {
                        task.Result.RequestMessage.Headers.Remove(ContainerMetadata.ContainerMetadataPrefix + metadata);
                        task.Result.RequestMessage.Headers.Add(ContainerMetadata.ContainerMetadataPrefix + metadata, value);
                        return task.Result;
                    });
        }
    }
}
