namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to update the properties of a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareUpdateServerAsync"/>
    /// <seealso cref="ComputeServiceExtensions.UpdateServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class UpdateServerApiCall : DelegatingHttpApiCall<ServerResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateServerApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public UpdateServerApiCall(IHttpApiCall<ServerResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
