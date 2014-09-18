namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to abandon a stack in the Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareAbandonStackAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.AbandonStackAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class AbandonStackApiCall : DelegatingHttpApiCall<Stack>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbandonStackApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public AbandonStackApiCall(IHttpApiCall<Stack> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
