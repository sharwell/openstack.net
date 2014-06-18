namespace OpenStack.Services.BlockStorage.V1
{
    using OpenStack.Net;

    public class CreateVolumeApiCall : DelegatingHttpApiCall<VolumeResponse>
    {
        public CreateVolumeApiCall(IHttpApiCall<VolumeResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
