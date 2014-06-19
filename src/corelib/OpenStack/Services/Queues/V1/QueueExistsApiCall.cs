namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to check if a queue exists in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareQueueExistsAsync"/>
    /// <seealso cref="QueuesServiceExtensions.QueueExistsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class QueueExistsApiCall : DelegatingHttpApiCall<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueExistsApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public QueueExistsApiCall(IHttpApiCall<bool> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
