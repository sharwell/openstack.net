namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to list the resource types defined in an instance of the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <remarks>
    /// <token>OpenStackObjectNotDefined</token>
    /// </remarks>
    /// <seealso cref="IOrchestrationService.PrepareListResourceTypesAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.ListResourceTypesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListResourceTypesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<ResourceTypeName>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListResourceTypesApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public ListResourceTypesApiCall(IHttpApiCall<ReadOnlyCollectionPage<ResourceTypeName>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
