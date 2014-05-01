namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class SetObjectMetadataApiCall : DelegatingHttpApiCall<string>
    {
        public SetObjectMetadataApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
