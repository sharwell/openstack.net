namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to list <see cref="Deployment"/> resources in the OpenStack Orchestration
    /// Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareListDeploymentsAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.ListDeploymentsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListDeploymentsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Deployment>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDeploymentsApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public ListDeploymentsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Deployment>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
