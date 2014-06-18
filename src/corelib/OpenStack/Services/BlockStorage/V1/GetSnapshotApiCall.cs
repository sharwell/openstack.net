namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Net;

    public class GetSnapshotApiCall : DelegatingHttpApiCall<SnapshotResponse>
    {
        public GetSnapshotApiCall(IHttpApiCall<SnapshotResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
