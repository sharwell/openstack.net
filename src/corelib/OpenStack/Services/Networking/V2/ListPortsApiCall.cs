namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListPortsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Port>>
    {
        public ListPortsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Port>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
