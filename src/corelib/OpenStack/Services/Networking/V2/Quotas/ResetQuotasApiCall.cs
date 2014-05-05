namespace OpenStack.Services.Networking.V2.Quotas
{
    using OpenStack.Net;

    public class ResetQuotasApiCall : DelegatingHttpApiCall<string>
    {
        public ResetQuotasApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
