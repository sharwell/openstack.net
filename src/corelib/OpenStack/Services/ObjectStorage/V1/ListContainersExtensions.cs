namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides extension methods for specifying optional parameters
    /// for the List Containers API call.
    /// </summary>
    /// <seealso cref="IObjectStorageService.PrepareListContainersAsync"/>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showAccountDetails__v1__account__storage_account_services.html">Show account details and list containers (OpenStack Object Storage API V1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ListContainersExtensions
    {
        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListContainersAsync"/>
        /// to include the <c>limit</c> query parameter, limiting the maximum number of items in the returned
        /// list of containers to a specified value.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListContainersApiCall"/> HTTP API call.</param>
        /// <param name="pageSize">The maximum number of containers to return in a single page of the resulting API call.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showAccountDetails__v1__account__storage_account_services.html">Show account details and list containers (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListContainersApiCall> WithPageSize(this Task<ListContainersApiCall> task, int pageSize)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task.WithQueryParameter("limit", pageSize.ToString());
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListContainersAsync"/>
        /// to include the <c>prefix</c> query parameter, filtering the resulting container listing to only
        /// include containers beginning with the specified prefix.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListContainersApiCall"/> HTTP API call.</param>
        /// <param name="prefix">The container name prefix used to filter the listing.</param>
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
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showAccountDetails__v1__account__storage_account_services.html">Show account details and list containers (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListContainersApiCall> WithPrefix(this Task<ListContainersApiCall> task, ContainerName prefix)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (prefix == null)
                throw new ArgumentNullException("prefix");

            return task.WithQueryParameter("prefix", prefix.Value);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListContainersAsync"/>
        /// to include the <c>marker</c> query parameter, filtering the resulting container listing to only
        /// include containers appearing after the marker when sorted using a binary comparison of container
        /// names, regardless of encoding.
        /// </summary>
        /// <remarks>
        /// This SDK always uses the UTF-8 encoding for container and object names, but containers and/or
        /// objects created through other means may use another encoding.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListContainersApiCall"/> HTTP API call.</param>
        /// <param name="marker">The marker container name used to filter the listing.</param>
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
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showAccountDetails__v1__account__storage_account_services.html">Show account details and list containers (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListContainersApiCall> WithMarker(this Task<ListContainersApiCall> task, ContainerName marker)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (marker == null)
                throw new ArgumentNullException("marker");

            return task.WithQueryParameter("marker", marker.Value);
        }

        /// <summary>
        /// Update an HTTP API call prepared by <see cref="IObjectStorageService.PrepareListContainersAsync"/>
        /// to include the <c>end_marker</c> query parameter, filtering the resulting container listing to only
        /// include containers appearing before the marker when sorted using a binary comparison of container
        /// names, regardless of encoding.
        /// </summary>
        /// <remarks>
        /// This SDK always uses the UTF-8 encoding for container and object names, but containers and/or
        /// objects created through other means may use another encoding.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing an asynchronous operation to prepare a <see cref="ListContainersApiCall"/> HTTP API call.</param>
        /// <param name="endMarker">The marker container name used to filter the listing.</param>
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
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showAccountDetails__v1__account__storage_account_services.html">Show account details and list containers (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ListContainersApiCall> WithEndMarker(this Task<ListContainersApiCall> task, ContainerName endMarker)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (endMarker == null)
                throw new ArgumentNullException("endMarker");

            return task.WithQueryParameter("end_marker", endMarker.Value);
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
