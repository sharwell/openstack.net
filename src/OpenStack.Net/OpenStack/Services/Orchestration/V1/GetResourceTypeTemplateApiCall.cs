namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get a template representation for a resource type in the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <remarks>
    /// <token>OpenStackObjectNotDefined</token>
    /// </remarks>
    /// <seealso cref="IOrchestrationService.PrepareGetResourceTypeTemplateAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetResourceTypeTemplateAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetResourceTypeTemplateApiCall : DelegatingHttpApiCall<ResourceTypeTemplate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetResourceTypeTemplateApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetResourceTypeTemplateApiCall(IHttpApiCall<ResourceTypeTemplate> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
