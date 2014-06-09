namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;
    using INetworkingService = OpenStack.Services.Networking.V2.INetworkingService;

    /// <summary>
    /// This class represents a prepared API call to get an extension resource in the
    /// <see cref="IComputeService"/> or <see cref="INetworkingService"/>.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareGetExtensionAsync"/>
    /// <seealso cref="ComputeServiceExtensions.GetExtensionAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetExtensionApiCall : DelegatingHttpApiCall<ExtensionResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetExtensionApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetExtensionApiCall(IHttpApiCall<ExtensionResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
