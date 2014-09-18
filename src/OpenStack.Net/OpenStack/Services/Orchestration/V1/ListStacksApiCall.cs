namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to list <see cref="Stack"/> resources in the OpenStack Orchestration
    /// Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareListStacksAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.ListStacksAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListStacksApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Stack>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListStacksApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public ListStacksApiCall(IHttpApiCall<ReadOnlyCollectionPage<Stack>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
