namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Net;

    public class RemoveVolumeApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveVolumeApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
