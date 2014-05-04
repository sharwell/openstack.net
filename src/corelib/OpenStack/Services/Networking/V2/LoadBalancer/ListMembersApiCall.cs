namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListMembersApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Member>>
    {
        public ListMembersApiCall(IHttpApiCall<ReadOnlyCollectionPage<Member>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
