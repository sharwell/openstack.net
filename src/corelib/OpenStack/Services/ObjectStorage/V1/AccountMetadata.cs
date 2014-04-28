namespace OpenStack.Services.ObjectStorage.V1
{
    using System.Collections.Generic;
    using System.Net.Http;

    public class AccountMetadata : StorageMetadata
    {
        public static readonly string AccountMetadataPrefix = "X-Account-Meta-";

        public AccountMetadata(HttpResponseMessage responseMessage)
            : base(responseMessage, AccountMetadataPrefix)
        {
        }

        public AccountMetadata(IDictionary<string, string> headers, IDictionary<string, string> metadata)
            : base(headers, metadata)
        {
        }
    }
}
