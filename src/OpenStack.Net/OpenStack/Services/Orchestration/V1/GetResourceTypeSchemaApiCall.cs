namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to get the schema for a specific resource type in the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareGetResourceTypeSchemaAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.GetResourceTypeSchemaAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetResourceTypeSchemaApiCall : DelegatingHttpApiCall<ResourceTypeSchema>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetResourceTypeSchemaApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public GetResourceTypeSchemaApiCall(IHttpApiCall<ResourceTypeSchema> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
