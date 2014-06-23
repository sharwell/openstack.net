namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to list the messages in a queue in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareListMessagesAsync"/>
    /// <seealso cref="QueuesServiceExtensions.ListMessagesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListMessagesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<QueuedMessage>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListMessagesApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListMessagesApiCall(IHttpApiCall<ReadOnlyCollectionPage<QueuedMessage>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
