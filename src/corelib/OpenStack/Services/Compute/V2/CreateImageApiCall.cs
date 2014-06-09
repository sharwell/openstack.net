namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to create an image.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareCreateImageAsync"/>
    /// <seealso cref="ComputeServiceExtensions.CreateImageAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateImageApiCall : DelegatingHttpApiCall<Uri>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public CreateImageApiCall(IHttpApiCall<Uri> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
