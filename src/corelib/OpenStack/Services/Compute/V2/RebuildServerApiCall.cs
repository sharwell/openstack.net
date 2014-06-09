namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to rebuild a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareRebuildServerAsync"/>
    /// <seealso cref="ComputeServiceExtensions.RebuildServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RebuildServerApiCall : DelegatingHttpApiCall<ServerResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RebuildServerApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public RebuildServerApiCall(IHttpApiCall<ServerResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}