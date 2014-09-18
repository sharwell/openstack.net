namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using Newtonsoft.Json.Linq;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get the metadata associated with a <see cref="Deployment"/> in the
    /// OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareGetDeploymentMetadataAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetDeploymentMetadataAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetDeploymentMetadataApiCall : DelegatingHttpApiCall<DeploymentMetadataResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDeploymentMetadataApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetDeploymentMetadataApiCall(IHttpApiCall<DeploymentMetadataResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
