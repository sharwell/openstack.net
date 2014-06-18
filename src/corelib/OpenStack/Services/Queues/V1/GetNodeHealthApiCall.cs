namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to determine if a node is operational in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareGetNodeHealthAsync"/>
    /// <seealso cref="QueuesServiceExtensions.GetNodeHealthAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetNodeHealthApiCall : DelegatingHttpApiCall<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetNodeHealthApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetNodeHealthApiCall(IHttpApiCall<bool> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
