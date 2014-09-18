namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get a <see cref="Resource"/> associated with a stack in the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <remarks>
    /// <token>OpenStackObjectNotDefined</token>
    /// </remarks>
    /// <seealso cref="IOrchestrationService.PrepareGetResourceAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetResourceAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetResourceApiCall : DelegatingHttpApiCall<ResourceResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetResourceApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetResourceApiCall(IHttpApiCall<ResourceResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
