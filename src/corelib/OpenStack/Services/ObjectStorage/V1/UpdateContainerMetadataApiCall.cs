namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class UpdateContainerMetadataApiCall : DelegatingHttpApiCall<string>
    {
        public UpdateContainerMetadataApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
