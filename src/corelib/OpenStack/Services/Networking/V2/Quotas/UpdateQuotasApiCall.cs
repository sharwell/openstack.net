namespace OpenStack.Services.Networking.V2.Quotas
{
    using OpenStack.Net;

    public class UpdateQuotasApiCall : DelegatingHttpApiCall<QuotaResponse>
    {
        public UpdateQuotasApiCall(IHttpApiCall<QuotaResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
