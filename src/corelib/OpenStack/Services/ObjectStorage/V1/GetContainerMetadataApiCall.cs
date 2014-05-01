namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class GetContainerMetadataApiCall : DelegatingHttpApiCall<ContainerMetadata>
    {
        public GetContainerMetadataApiCall(IHttpApiCall<ContainerMetadata> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
