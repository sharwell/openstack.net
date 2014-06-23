namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to list the queues in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareListQueuesAsync"/>
    /// <seealso cref="QueuesServiceExtensions.ListQueuesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListQueuesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Queue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListQueuesApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListQueuesApiCall(IHttpApiCall<ReadOnlyCollectionPage<Queue>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}