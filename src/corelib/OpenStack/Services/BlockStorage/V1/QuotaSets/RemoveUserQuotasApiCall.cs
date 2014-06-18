namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to remove any customized quotas assigned
    /// to a particular user, resetting the quotas to their default values.
    /// </summary>
    /// <seealso cref="QuotaSetsExtensions.PrepareRemoveUserQuotasAsync"/>
    /// <seealso cref="QuotaSetsExtensions.RemoveUserQuotasAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RemoveUserQuotasApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveUserQuotasApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public RemoveUserQuotasApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}