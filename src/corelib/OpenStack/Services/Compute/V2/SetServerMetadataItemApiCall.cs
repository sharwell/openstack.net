namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to set the value of a particular metadata item associated with a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareSetServerMetadataItemAsync"/>
    /// <seealso cref="ComputeServiceExtensions.SetServerMetadataItemAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class SetServerMetadataItemApiCall : DelegatingHttpApiCall<MetadataResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetServerMetadataItemApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public SetServerMetadataItemApiCall(IHttpApiCall<MetadataResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
