namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to create a queue in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareCreateQueueAsync"/>
    /// <seealso cref="QueuesServiceExtensions.CreateQueueAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateQueueApiCall : DelegatingHttpApiCall<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateQueueApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public CreateQueueApiCall(IHttpApiCall<bool> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
