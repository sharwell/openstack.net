namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to update the metadata associated with an image resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareSetImageMetadataAsync"/>
    /// <seealso cref="ComputeServiceExtensions.SetImageMetadataAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class SetImageMetadataApiCall : DelegatingHttpApiCall<MetadataResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetImageMetadataApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public SetImageMetadataApiCall(IHttpApiCall<MetadataResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
