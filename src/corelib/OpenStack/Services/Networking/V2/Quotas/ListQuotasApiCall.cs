namespace OpenStack.Services.Networking.V2.Quotas
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListQuotasApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Quota>>
    {
        public ListQuotasApiCall(IHttpApiCall<ReadOnlyCollectionPage<Quota>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
