namespace Rackspace.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to place a server resource into rescue mode.
    /// </summary>
    /// <seealso cref="RescueExtensions.PrepareRescueServerAsync"/>
    /// <seealso cref="RescueExtensions.RescueServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RescueServerApiCall : DelegatingHttpApiCall<RescueServerResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RescueServerApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public RescueServerApiCall(IHttpApiCall<RescueServerResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
