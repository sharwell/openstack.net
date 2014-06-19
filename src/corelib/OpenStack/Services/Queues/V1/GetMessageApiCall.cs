namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get a message in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareGetMessageAsync"/>
    /// <seealso cref="QueuesServiceExtensions.GetMessageAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetMessageApiCall : DelegatingHttpApiCall<QueuedMessage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMessageApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetMessageApiCall(IHttpApiCall<QueuedMessage> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
