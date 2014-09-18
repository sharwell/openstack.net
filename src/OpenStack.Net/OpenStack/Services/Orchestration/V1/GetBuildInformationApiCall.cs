namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get build information for a specific instance of the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareGetBuildInformationAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetBuildInformationAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetBuildInformationApiCall : DelegatingHttpApiCall<BuildInformation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetBuildInformationApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetBuildInformationApiCall(IHttpApiCall<BuildInformation> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
