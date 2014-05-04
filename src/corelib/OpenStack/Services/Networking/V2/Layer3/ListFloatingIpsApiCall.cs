namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListFloatingIpsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<FloatingIp>>
    {
        public ListFloatingIpsApiCall(IHttpApiCall<ReadOnlyCollectionPage<FloatingIp>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
