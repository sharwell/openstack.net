namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

#warning Unlike "Update Stack", this operation is documented as returning the updated resource.

    /// <summary>
    /// This class represents an HTTP API call to update a <see cref="Deployment"/> resource in the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareUpdateDeploymentAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.UpdateDeploymentAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class UpdateDeploymentApiCall : DelegatingHttpApiCall<DeploymentResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDeploymentApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public UpdateDeploymentApiCall(IHttpApiCall<DeploymentResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
