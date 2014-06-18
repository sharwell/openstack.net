namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Net;

    public class RemoveSnapshotApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveSnapshotApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
