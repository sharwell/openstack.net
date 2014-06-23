namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to enqueue messages in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PreparePostMessagesAsync"/>
    /// <seealso cref="O:OpenStack.Services.Queues.V1.QueuesServiceExtensions.PostMessagesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class PostMessagesApiCall : DelegatingHttpApiCall<PostMessagesResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostMessagesApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public PostMessagesApiCall(IHttpApiCall<PostMessagesResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
