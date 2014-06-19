namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to remove a message in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareRemoveMessageAsync"/>
    /// <seealso cref="QueuesServiceExtensions.RemoveMessageAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RemoveMessageApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveMessageApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public RemoveMessageApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
