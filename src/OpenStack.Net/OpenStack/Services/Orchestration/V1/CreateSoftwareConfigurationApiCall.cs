namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to create a software configuration in the OpenStack Orchestration
    /// Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareCreateSoftwareConfigurationAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.CreateSoftwareConfigurationAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateSoftwareConfigurationApiCall : DelegatingHttpApiCall<SoftwareConfigurationResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSoftwareConfigurationApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public CreateSoftwareConfigurationApiCall(IHttpApiCall<SoftwareConfigurationResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
