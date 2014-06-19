namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get messages in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareGetMessagesAsync"/>
    /// <seealso cref="QueuesServiceExtensions.GetMessagesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetMessagesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<QueuedMessage>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMessagesApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetMessagesApiCall(IHttpApiCall<ReadOnlyCollectionPage<QueuedMessage>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
