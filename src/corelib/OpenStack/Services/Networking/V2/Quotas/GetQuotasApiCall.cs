namespace OpenStack.Services.Networking.V2.Quotas
{
    using OpenStack.Net;

    public class GetQuotasApiCall : DelegatingHttpApiCall<QuotaResponse>
    {
        public GetQuotasApiCall(IHttpApiCall<QuotaResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
