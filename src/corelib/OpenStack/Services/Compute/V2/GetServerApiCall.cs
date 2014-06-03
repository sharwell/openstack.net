namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get a server resource by ID.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareGetServerAsync"/>
    /// <seealso cref="ComputeServiceExtensions.GetServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetServerApiCall : DelegatingHttpApiCall<ServerResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetServerApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetServerApiCall(IHttpApiCall<ServerResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
