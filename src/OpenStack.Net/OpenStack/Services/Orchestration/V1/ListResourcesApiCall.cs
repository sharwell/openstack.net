namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to list <see cref="Resource"/> resources associated with a
    /// <see cref="Stack"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// <token>OpenStackObjectNotDefined</token>
    /// </remarks>
    /// <seealso cref="IOrchestrationService.PrepareListResourcesAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.ListResourcesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListResourcesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Resource>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListResourcesApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public ListResourcesApiCall(IHttpApiCall<ReadOnlyCollectionPage<Resource>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
