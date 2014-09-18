namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to remove a <see cref="Deployment"/> resource in the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareRemoveDeploymentAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.RemoveDeploymentAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RemoveDeploymentApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveDeploymentApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public RemoveDeploymentApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
