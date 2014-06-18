namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get detailed information about quota
    /// usage for a particular user.
    /// </summary>
    /// <seealso cref="QuotaSetsExtensions.PrepareGetUserQuotaDetailsAsync"/>
    /// <seealso cref="QuotaSetsExtensions.GetUserQuotaDetailsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetUserQuotaDetailsApiCall : DelegatingHttpApiCall<QuotaSetDetailsResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserQuotaDetailsApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetUserQuotaDetailsApiCall(IHttpApiCall<QuotaSetDetailsResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}