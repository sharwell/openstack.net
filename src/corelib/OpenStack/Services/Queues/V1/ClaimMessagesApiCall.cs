namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    /// <summary>
    /// This class represents a prepared API call to claim messages in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareClaimMessagesAsync"/>
    /// <seealso cref="QueuesServiceExtensions.ClaimMessagesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ClaimMessagesApiCall : DelegatingHttpApiCall<Tuple<Uri, Claim>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimMessagesApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ClaimMessagesApiCall(IHttpApiCall<Tuple<Uri, Claim>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
