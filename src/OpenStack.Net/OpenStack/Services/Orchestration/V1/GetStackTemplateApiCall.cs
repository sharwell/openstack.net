namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get the template for a stack in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// <token>OpenStackObjectNotDefined</token>
    /// </remarks>
    /// <seealso cref="IOrchestrationService.PrepareGetStackTemplateAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetStackTemplateAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetStackTemplateApiCall : DelegatingHttpApiCall<StackTemplate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStackTemplateApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetStackTemplateApiCall(IHttpApiCall<StackTemplate> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
