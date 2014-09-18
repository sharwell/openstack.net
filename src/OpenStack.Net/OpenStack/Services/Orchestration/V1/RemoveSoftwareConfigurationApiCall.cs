namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to remove a <see cref="SoftwareConfiguration"/> resource in the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareRemoveSoftwareConfigurationAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.RemoveSoftwareConfigurationAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RemoveSoftwareConfigurationApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveSoftwareConfigurationApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public RemoveSoftwareConfigurationApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
