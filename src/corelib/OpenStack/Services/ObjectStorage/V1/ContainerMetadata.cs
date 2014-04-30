namespace OpenStack.Services.ObjectStorage.V1
{
    using System.Collections.Generic;
    using System.Net.Http;

    public class ContainerMetadata : StorageMetadata
    {
        public static readonly string ContainerMetadataPrefix = "X-Container-Meta-";

        private static readonly ContainerMetadata _emptyMetadata = new ContainerMetadata(new Dictionary<string, string>(), new Dictionary<string, string>());

        public ContainerMetadata(HttpResponseMessage responseMessage)
            : base(responseMessage, ContainerMetadataPrefix)
        {
        }

        public ContainerMetadata(IDictionary<string, string> headers, IDictionary<string, string> metadata)
            : base(headers, metadata)
        {
        }

        public static ContainerMetadata Empty
        {
            get
            {
                return _emptyMetadata;
            }
        }
    }
}
