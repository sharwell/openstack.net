namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to set the value of a particular metadata item associated with an image resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareSetImageMetadataItemAsync"/>
    /// <seealso cref="ComputeServiceExtensions.SetImageMetadataItemAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class SetImageMetadataItemApiCall : DelegatingHttpApiCall<MetadataResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetImageMetadataItemApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public SetImageMetadataItemApiCall(IHttpApiCall<MetadataResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
