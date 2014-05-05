namespace OpenStack.Services.Networking.V2.Metering
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListMeteringLabelRulesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<MeteringLabelRule>>
    {
        public ListMeteringLabelRulesApiCall(IHttpApiCall<ReadOnlyCollectionPage<MeteringLabelRule>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
