namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListNetworksApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Network>>
    {
        public ListNetworksApiCall(IHttpApiCall<ReadOnlyCollectionPage<Network>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
