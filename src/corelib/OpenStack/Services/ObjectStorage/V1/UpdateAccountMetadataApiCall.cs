namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class UpdateAccountMetadataApiCall : DelegatingHttpApiCall<string>
    {
        public UpdateAccountMetadataApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
