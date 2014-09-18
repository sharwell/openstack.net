namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This is the base class for HTTP API calls which perform an action on a <see cref="Stack"/> resource in the
    /// OpenStack Orchestration Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class StackActionApiCall<T> : DelegatingHttpApiCall<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public StackActionApiCall(IHttpApiCall<T> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
