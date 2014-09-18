namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to create a deployment in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareCreateDeploymentAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.CreateDeploymentAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateDeploymentApiCall : DelegatingHttpApiCall<DeploymentResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDeploymentApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public CreateDeploymentApiCall(IHttpApiCall<DeploymentResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
