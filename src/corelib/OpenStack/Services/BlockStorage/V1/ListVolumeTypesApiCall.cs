namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListVolumeTypesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<VolumeType>>
    {
        public ListVolumeTypesApiCall(IHttpApiCall<ReadOnlyCollectionPage<VolumeType>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
