namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListSubnetsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Subnet>>
    {
        public ListSubnetsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Subnet>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
