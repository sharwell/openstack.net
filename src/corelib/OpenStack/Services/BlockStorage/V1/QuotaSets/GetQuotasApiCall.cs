namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get the quotas for a particular project.
    /// </summary>
    /// <seealso cref="QuotaSetsExtensions.PrepareGetQuotasAsync"/>
    /// <seealso cref="QuotaSetsExtensions.GetQuotasAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetQuotasApiCall : DelegatingHttpApiCall<QuotaSetResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuotasApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetQuotasApiCall(IHttpApiCall<QuotaSetResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}