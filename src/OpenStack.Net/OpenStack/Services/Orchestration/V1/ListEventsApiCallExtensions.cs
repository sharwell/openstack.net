﻿namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides extension methods for specifying additional parameters for the
    /// <see cref="ListEventsApiCall"/> HTTP API call.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ListEventsApiCallExtensions
    {
        /// <summary>
        /// Sets (or removes) the <c>limit</c> query parameter for a <see cref="ListEventsApiCall"/> HTTP API call.
        /// </summary>
        /// <param name="apiCall">The prepared HTTP API call.</param>
        /// <param name="pageSize">
        /// <para>The maximum number of items to return in a single page of results.</para>
        /// <para>-or-</para>
        /// <para><see langword="null"/> to remove the <c>limit</c> query parameter and use the default page size
        /// configured by the vendor for the service.</para>
        /// </param>
        /// <returns>Returns the input argument <paramref name="apiCall"/>, which was modified according to the
        /// specified <paramref name="pageSize"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="apiCall"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="pageSize"/> is less than 0.</exception>
        /// <exception cref="ObjectDisposedException">If the HTTP API call has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the HTTP API call has already been sent.</exception>
        public static ListEventsApiCall WithPageSize(this ListEventsApiCall apiCall, int? pageSize)
        {
            if (apiCall == null)
                throw new ArgumentNullException("apiCall");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize");

            Uri uri = apiCall.RequestMessage.RequestUri;
            if (!pageSize.HasValue)
                apiCall.RequestMessage.RequestUri = UriUtility.RemoveQueryParameter(uri, "limit");
            else
                apiCall.RequestMessage.RequestUri = UriUtility.SetQueryParameter(uri, "limit", pageSize.ToString());

            return apiCall;
        }

        /// <summary>
        /// Sets (or removes) the <c>marker</c> query parameter for a <see cref="ListEventsApiCall"/> HTTP API call.
        /// </summary>
        /// <param name="apiCall">The prepared HTTP API call.</param>
        /// <param name="eventId">
        /// <para>The unique identifier of the last <see cref="Event"/> in the previous page of results.</para>
        /// <para>-or-</para>
        /// <para><see langword="null"/> to remove the <c>marker</c> query parameter and have the resulting page start
        /// with the first item in the list.</para>
        /// </param>
        /// <returns>Returns the input argument <paramref name="apiCall"/>, which was modified according to the
        /// specified <paramref name="eventId"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="apiCall"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the HTTP API call has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the HTTP API call has already been sent.</exception>
        public static ListEventsApiCall WithMarker(this ListEventsApiCall apiCall, EventId eventId)
        {
            if (apiCall == null)
                throw new ArgumentNullException("apiCall");

            Uri uri = apiCall.RequestMessage.RequestUri;
            if (eventId == null)
                apiCall.RequestMessage.RequestUri = UriUtility.RemoveQueryParameter(uri, "marker");
            else
                apiCall.RequestMessage.RequestUri = UriUtility.SetQueryParameter(uri, "marker", eventId.Value);

            return apiCall;
        }

        /// <summary>
        /// Sets (or removes) the <c>limit</c> query parameter for a <see cref="ListEventsApiCall"/> HTTP API call.
        /// </summary>
        /// <remarks>
        /// <para>This method simplifies the use of <see cref="WithPageSize(ListEventsApiCall, int?)"/> in
        /// scenarios where <see langword="async/await"/> are not used for the preparation of the HTTP API call.</para>
        /// </remarks>
        /// <param name="apiCall">A <see cref="Task"/> representing the asynchronous operation to prepare the HTTP API
        /// call.</param>
        /// <param name="pageSize">
        /// <para>The maximum number of items to return in a single page of results.</para>
        /// <para>-or-</para>
        /// <para><see langword="null"/> to remove the <c>limit</c> query parameter and use the default page size
        /// configured by the vendor for the service.</para>
        /// </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property contains the result of the input task
        /// <paramref name="apiCall"/>, which was modified according to the specified
        /// <paramref name="pageSize"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="apiCall"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="pageSize"/> is less than 0.</exception>
        /// <exception cref="ObjectDisposedException">If the HTTP API call has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the HTTP API call has already been sent.</exception>
        public static Task<ListEventsApiCall> WithPageSize(this Task<ListEventsApiCall> apiCall, int? pageSize)
        {
            return apiCall.Select(task => apiCall.Result.WithPageSize(pageSize));
        }

        /// <summary>
        /// Sets (or removes) the <c>marker</c> query parameter for a <see cref="ListEventsApiCall"/> HTTP API call.
        /// </summary>
        /// <remarks>
        /// <para>This method simplifies the use of <see cref="WithMarker(ListEventsApiCall, EventId)"/> in
        /// scenarios where <see langword="async/await"/> are not used for the preparation of the HTTP API call.</para>
        /// </remarks>
        /// <param name="apiCall">A <see cref="Task"/> representing the asynchronous operation to prepare the HTTP API
        /// call.</param>
        /// <param name="eventId">
        /// <para>The unique identifier of the last <see cref="Event"/> in the previous page of results.</para>
        /// <para>-or-</para>
        /// <para><see langword="null"/> to remove the <c>marker</c> query parameter and have the resulting page start
        /// with the first item in the list.</para>
        /// </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property contains the result of the input task
        /// <paramref name="apiCall"/>, which was modified according to the specified
        /// <paramref name="eventId"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="apiCall"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the HTTP API call has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the HTTP API call has already been sent.</exception>
        public static Task<ListEventsApiCall> WithMarker(this Task<ListEventsApiCall> apiCall, EventId eventId)
        {
            return apiCall.Select(task => apiCall.Result.WithMarker(eventId));
        }
    }
}
