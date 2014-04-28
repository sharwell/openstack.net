namespace OpenStack.Services.ObjectStorage.V1
{
    using System.Collections.Generic;
    using System.Net.Http;

    public class ObjectMetadata : StorageMetadata
    {
        public static readonly string ObjectMetadataPrefix = "X-Object-Meta-";

        public ObjectMetadata(HttpResponseMessage responseMessage)
            : base(responseMessage, ObjectMetadataPrefix)
        {
        }

        public ObjectMetadata(IDictionary<string, string> headers, IDictionary<string, string> metadata)
            : base(headers, metadata)
        {
        }
    }
}
