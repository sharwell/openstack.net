namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class GetObjectMetadataApiCall : DelegatingHttpApiCall<ObjectMetadata>
    {
        public GetObjectMetadataApiCall(IHttpApiCall<ObjectMetadata> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
