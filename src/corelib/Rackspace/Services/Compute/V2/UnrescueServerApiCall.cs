namespace Rackspace.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to take a server resource out of rescue mode.
    /// </summary>
    /// <seealso cref="RescueExtensions.PrepareUnrescueServerAsync"/>
    /// <seealso cref="RescueExtensions.UnrescueServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class UnrescueServerApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnrescueServerApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public UnrescueServerApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
