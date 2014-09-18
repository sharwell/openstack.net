namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get an <see cref="Event"/> associated with a <see cref="Resource"/> in
    /// the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// <token>OpenStackObjectNotDefined</token>
    /// </remarks>
    /// <seealso cref="IOrchestrationService.PrepareGetResourceEventAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetResourceEventAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetResourceEventApiCall : DelegatingHttpApiCall<EventResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetResourceEventApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetResourceEventApiCall(IHttpApiCall<EventResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
