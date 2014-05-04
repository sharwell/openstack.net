namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListRoutersApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Router>>
    {
        public ListRoutersApiCall(IHttpApiCall<ReadOnlyCollectionPage<Router>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
