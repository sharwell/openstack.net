﻿namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides extension methods for specifying optional parameters
    /// for the List Objects API call.
    /// </summary>
    /// <seealso cref="IObjectStorageService.PrepareListObjectsAsync"/>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ListObjectsExtensions
    {
        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListObjectsAsync"/>
        /// to include the <c>limit</c> query parameter, limiting the maximum number of items in the returned
        /// list of objects to a specified value.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListObjectsApiCall"/> HTTP API call.</param>
        /// <param name="pageSize">The maximum number of objects to return in a single page of the resulting API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> WithPageSize(this Task<ListObjectsApiCall> task, int pageSize)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithQueryParameter("limit", pageSize.ToString());
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListObjectsAsync"/>
        /// to include the <c>delimiter</c> query parameter, which instructs the Object Storage service to
        /// return the object prefix for objects beginning with the prefix specified by <seealso cref="WithPrefix"/>
        /// up to next the instance of <paramref name="delimiter"/> which follows the prefix.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListObjectsApiCall"/> HTTP API call.</param>
        /// <param name="delimiter">The delimiter to treat as a directory separator character when listing objects.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/pseudo-hierarchical-folders-directories.html">Pseudo-hierarchical folders and directories (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> WithDelimiter(this Task<ListObjectsApiCall> task, char delimiter)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithQueryParameter("delimiter", delimiter.ToString());
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListObjectsAsync"/>
        /// to include the <c>prefix</c> and <c>delimiter</c> query parameters, which instructs the Object Storage service to
        /// return the object prefix for objects beginning with the prefix specified by <paramref name="prefix"/>
        /// up to next the instance of <paramref name="delimiter"/> which follows the prefix.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListObjectsApiCall"/> HTTP API call.</param>
        /// <param name="prefix">The object name prefix used to filter the listing.</param>
        /// <param name="delimiter">The delimiter to treat as a directory separator character when listing objects.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="task"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="prefix"/> is <seealso langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/pseudo-hierarchical-folders-directories.html">Pseudo-hierarchical folders and directories (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> WithDelimiter(this Task<ListObjectsApiCall> task, ObjectName prefix, char delimiter)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (prefix == null)
                throw new ArgumentNullException("prefix");

            return task.WithPrefix(prefix).WithDelimiter(delimiter);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListObjectsAsync"/>
        /// to include the <c>path</c> query parameter, which instructs the Object Storage service to
        /// return a listing of pseudo-directories and objects contained in the pseudo-directory
        /// <paramref name="path"/>.
        /// </summary>
        /// <remarks>
        /// The <c>path</c> query parameter is equivalent to calling
        /// <seealso cref="WithDelimiter(Task{ListObjectsApiCall}, ObjectName, char)"/> with the prefix set
        /// to <paramref name="path"/> with a <c>/</c> at the end, and the delimiter set to <c>/</c>.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListObjectsApiCall"/> HTTP API call.</param>
        /// <param name="path">The object name of the pseudo-directory used to filter the listing.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="task"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="path"/> is <seealso langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/pseudo-hierarchical-folders-directories.html">Pseudo-hierarchical folders and directories (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> WithPath(this Task<ListObjectsApiCall> task, ObjectName path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            return task.WithQueryParameter("path", path.Value);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListObjectsAsync"/>
        /// to include the <c>prefix</c> query parameter, filtering the resulting object listing to only
        /// include objects beginning with the specified prefix.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListObjectsApiCall"/> HTTP API call.</param>
        /// <param name="prefix">The object name prefix used to filter the listing.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="task"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="prefix"/> is <seealso langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> WithPrefix(this Task<ListObjectsApiCall> task, ObjectName prefix)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (prefix == null)
                throw new ArgumentNullException("prefix");

            return task.WithQueryParameter("prefix", prefix.Value);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListObjectsAsync"/>
        /// to include the <c>marker</c> query parameter, filtering the resulting object listing to only
        /// include objects appearing after the marker when sorted using a binary comparison of object
        /// names, regardless of encoding.
        /// </summary>
        /// <remarks>
        /// This SDK always uses the UTF-8 encoding for container and object names, but containers and/or
        /// objects created through other means may use another encoding.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListObjectsApiCall"/> HTTP API call.</param>
        /// <param name="marker">The marker object name used to filter the listing.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="task"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="marker"/> is <seealso langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> WithMarker(this Task<ListObjectsApiCall> task, ObjectName marker)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (marker == null)
                throw new ArgumentNullException("marker");

            return task.WithQueryParameter("marker", marker.Value);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListObjectsAsync"/>
        /// to include the <c>end_marker</c> query parameter, filtering the resulting object listing to only
        /// include objects appearing before the marker when sorted using a binary comparison of object
        /// names, regardless of encoding.
        /// </summary>
        /// <remarks>
        /// This SDK always uses the UTF-8 encoding for container and object names, but containers and/or
        /// objects created through other means may use another encoding.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListObjectsApiCall"/> HTTP API call.</param>
        /// <param name="endMarker">The marker object name used to filter the listing.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="task"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="endMarker"/> is <seealso langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> WithEndMarker(this Task<ListObjectsApiCall> task, ObjectName endMarker)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (endMarker == null)
                throw new ArgumentNullException("endMarker");

            return task.WithQueryParameter("end_marker", endMarker.Value);
        }

        /// <summary>
        /// Prepare an API call to list the objects present in a container in the Object Storage service,
        /// treating the listing as a pseudo-hierarchical listing of directories.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="path">
        /// The object name of the pseudo-directory used to filter the listing.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to list the root objects and directories in the container.</para>
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
        /// <para>If <paramref name="container"/> is <seealso langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="ListObjectsApiCall"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> PrepareListObjectsInDirectoryAsync(this IObjectStorageService service, ContainerName container, ObjectName path, CancellationToken cancellationToken)
        {
            return PrepareListObjectsInDirectoryAsync(service, container, path, '/', cancellationToken);
        }

        /// <summary>
        /// Prepare an API call to list the objects present in a container in the Object Storage service,
        /// treating the listing as a pseudo-hierarchical listing of directories using a specified directory
        /// delimiter character.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container name.</param>
        /// <param name="path">
        /// The object name of the pseudo-directory used to filter the listing.
        /// <para>-or-</para>
        /// <para><see langword="null"/> to list the root objects and directories in the container.</para>
        /// </param>
        /// <param name="delimiter">The delimiter to use for the pseudo-hierarchical directory listing.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <seealso langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="ListObjectsApiCall"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListObjectsApiCall> PrepareListObjectsInDirectoryAsync(this IObjectStorageService service, ContainerName container, ObjectName path, char delimiter, CancellationToken cancellationToken)
        {
            if (path == null)
                path = new ObjectName(delimiter.ToString());
            else if (path.Value[path.Value.Length - 1] != delimiter)
                path = new ObjectName(path.Value + delimiter);

            return service.PrepareListObjectsAsync(container, cancellationToken)
                .WithDelimiter(path, delimiter);
        }

        /// <summary>
        /// Update a generic <see cref="IHttpApiRequest"/> to add or update a query parameter
        /// to a specific value.
        /// </summary>
        /// <typeparam name="TCall">The type of the HTTP API request.</typeparam>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare HTTP API request.</param>
        /// <param name="parameter">The name of the query parameter to add or update.</param>
        /// <param name="value">The value of the query parameter.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="task"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="parameter"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="parameter"/> is empty.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        private static Task<TCall> WithQueryParameter<TCall>(this Task<TCall> task, string parameter, string value)
            where TCall : IHttpApiRequest
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (parameter == null)
                throw new ArgumentNullException("parameter");
            if (value == null)
                throw new ArgumentNullException("value");
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentException("parameter cannot be empty", "parameter");

            return task.Select(
                innerTask =>
                {
                    Uri requestUri = innerTask.Result.RequestMessage.RequestUri;
                    requestUri = UriUtility.SetQueryParameter(requestUri, parameter, value);
                    innerTask.Result.RequestMessage.RequestUri = requestUri;
                    return innerTask.Result;
                });
        }
    }
}
