namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Net;

    public class CreateSnapshotApiCall : DelegatingHttpApiCall<SnapshotResponse>
    {
        public CreateSnapshotApiCall(IHttpApiCall<SnapshotResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
