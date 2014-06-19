namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get the metadata associated with a queue in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareGetQueueMetadataAsync"/>
    /// <seealso cref="QueuesServiceExtensions.GetQueueMetadataAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetQueueMetadataApiCall<T> : DelegatingHttpApiCall<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQueueMetadataApiCall{T}"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetQueueMetadataApiCall(IHttpApiCall<T> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
