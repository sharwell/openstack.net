namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to update quotas for a project.
    /// </summary>
    /// <seealso cref="QuotaSetsExtensions.PrepareUpdateQuotasAsync"/>
    /// <seealso cref="QuotaSetsExtensions.UpdateQuotasAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class UpdateQuotasApiCall : DelegatingHttpApiCall<QuotaSetResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateQuotasApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public UpdateQuotasApiCall(IHttpApiCall<QuotaSetResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}