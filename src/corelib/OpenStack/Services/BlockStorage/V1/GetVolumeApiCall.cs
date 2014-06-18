namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Net;

    public class GetVolumeApiCall : DelegatingHttpApiCall<VolumeResponse>
    {
        public GetVolumeApiCall(IHttpApiCall<VolumeResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
