namespace OpenStack.Services.Compute.V2
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListExtensionsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Extension>>
    {
        public ListExtensionsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Extension>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
