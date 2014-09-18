namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get a software configuration in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareGetSoftwareConfigurationAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetSoftwareConfigurationAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetSoftwareConfigurationApiCall : DelegatingHttpApiCall<SoftwareConfigurationResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSoftwareConfigurationApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetSoftwareConfigurationApiCall(IHttpApiCall<SoftwareConfigurationResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
