namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get a specific <see cref="Deployment"/> in the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareGetDeploymentAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetDeploymentAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetDeploymentApiCall : DelegatingHttpApiCall<DeploymentResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDeploymentApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetDeploymentApiCall(IHttpApiCall<DeploymentResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
