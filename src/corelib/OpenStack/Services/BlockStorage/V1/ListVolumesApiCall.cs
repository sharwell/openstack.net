namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListVolumesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Volume>>
    {
        public ListVolumesApiCall(IHttpApiCall<ReadOnlyCollectionPage<Volume>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
