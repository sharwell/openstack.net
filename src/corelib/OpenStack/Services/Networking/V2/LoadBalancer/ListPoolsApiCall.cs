namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListPoolsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Pool>>
    {
        public ListPoolsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Pool>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
