namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListVirtualAddressesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<VirtualAddress>>
    {
        public ListVirtualAddressesApiCall(IHttpApiCall<ReadOnlyCollectionPage<VirtualAddress>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
