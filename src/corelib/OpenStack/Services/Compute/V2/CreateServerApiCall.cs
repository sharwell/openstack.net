namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to create a new server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareCreateServerAsync"/>
    /// <seealso cref="ComputeServiceExtensions.CreateServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateServerApiCall : DelegatingHttpApiCall<ServerResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateServerApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public CreateServerApiCall(IHttpApiCall<ServerResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
