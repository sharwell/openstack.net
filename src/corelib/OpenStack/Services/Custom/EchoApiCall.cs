namespace OpenStack.Services.Custom
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a call to the echo API.
    /// </summary>
    /// <see cref="IEchoService.PrepareEchoAsync"/>
    public class EchoApiCall : DelegatingHttpApiCall<EchoResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EchoApiCall"/> class.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the underlying behavior of this API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public EchoApiCall(IHttpApiCall<EchoResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}