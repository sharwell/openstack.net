namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListSnapshotsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Snapshot>>
    {
        public ListSnapshotsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Snapshot>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
