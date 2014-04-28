namespace OpenStack.Services.ObjectStorage.V1
{
    using System.Collections.Generic;
    using System.Net.Http;

    public class ContainerMetadata : StorageMetadata
    {
        public static readonly string ContainerMetadataPrefix = "X-Container-Meta-";

        public ContainerMetadata(HttpResponseMessage responseMessage)
            : base(responseMessage, ContainerMetadataPrefix)
        {
        }

        public ContainerMetadata(IDictionary<string, string> headers, IDictionary<string, string> metadata)
            : base(headers, metadata)
        {
        }
    }
}
