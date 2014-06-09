namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;
    using INetworkingService = OpenStack.Services.Networking.V2.INetworkingService;

    /// <summary>
    /// This class represents a prepared API call to list the extension resources in the
    /// <see cref="IComputeService"/> or <see cref="INetworkingService"/>.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareListExtensionsAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ListExtensionsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListExtensionsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Extension>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListExtensionsApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListExtensionsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Extension>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
