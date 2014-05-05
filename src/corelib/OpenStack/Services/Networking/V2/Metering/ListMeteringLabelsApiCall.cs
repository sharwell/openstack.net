namespace OpenStack.Services.Networking.V2.Metering
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListMeteringLabelsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<MeteringLabel>>
    {
        public ListMeteringLabelsApiCall(IHttpApiCall<ReadOnlyCollectionPage<MeteringLabel>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
