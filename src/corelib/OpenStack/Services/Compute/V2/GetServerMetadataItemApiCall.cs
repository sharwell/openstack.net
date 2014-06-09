namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get the value of a particular metadata item associated with a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareGetServerMetadataItemAsync"/>
    /// <seealso cref="ComputeServiceExtensions.GetServerMetadataItemAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetServerMetadataItemApiCall : DelegatingHttpApiCall<MetadataResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetServerMetadataItemApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetServerMetadataItemApiCall(IHttpApiCall<MetadataResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
