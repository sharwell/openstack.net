namespace OpenStack.Services.Queues.V1
{
    using System;
    using OpenStack.Net;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    /// <summary>
    /// This class represents a prepared API call to get information about an existing claim in the Queueing Service.
    /// </summary>
    /// <seealso cref="IQueuesService.PrepareQueryClaimAsync"/>
    /// <seealso cref="QueuesServiceExtensions.QueryClaimAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class QueryClaimApiCall : DelegatingHttpApiCall<Tuple<Uri, Claim>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryClaimApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public QueryClaimApiCall(IHttpApiCall<Tuple<Uri, Claim>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
