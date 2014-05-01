namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class GetAccountMetadataApiCall : DelegatingHttpApiCall<AccountMetadata>
    {
        public GetAccountMetadataApiCall(IHttpApiCall<AccountMetadata> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
