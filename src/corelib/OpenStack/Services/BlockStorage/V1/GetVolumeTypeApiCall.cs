namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Net;

    public class GetVolumeTypeApiCall : DelegatingHttpApiCall<VolumeTypeResponse>
    {
        public GetVolumeTypeApiCall(IHttpApiCall<VolumeTypeResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
