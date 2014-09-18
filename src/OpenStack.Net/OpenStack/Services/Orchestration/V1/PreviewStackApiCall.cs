namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to preview a <see cref="Stack"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PreparePreviewStackAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.PreviewStackAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class PreviewStackApiCall : DelegatingHttpApiCall<StackResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewStackApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public PreviewStackApiCall(IHttpApiCall<StackResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
