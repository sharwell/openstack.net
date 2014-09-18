namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to create a stack from existing resources in the Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareAdoptStackAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.AdoptStackAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class AdoptStackApiCall : DelegatingHttpApiCall<StackResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptStackApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public AdoptStackApiCall(IHttpApiCall<StackResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
