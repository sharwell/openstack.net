namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

#if !NET45PLUS
    using OpenStack.Collections;
#endif

    public abstract class StorageMetadata
    {
        /// <summary>
        /// This is the backing field for the <see cref="Headers"/> property.
        /// </summary>
        private readonly IDictionary<string, string> _headers;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        private readonly IDictionary<string, string> _metadata;

        protected StorageMetadata(HttpResponseMessage responseMessage, string metadataPrefix)
        {
            if (responseMessage == null)
                throw new ArgumentNullException("responseMessage");
            if (metadataPrefix == null)
                throw new ArgumentNullException("metadataPrefix");
            if (string.IsNullOrEmpty(metadataPrefix))
                throw new ArgumentException("metadataPrefix cannot be empty");

            IDictionary<string, string> headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            IDictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, IEnumerable<string>> header in responseMessage.Headers)
            {
                if (header.Key.StartsWith(metadataPrefix))
                    metadata.Add(header.Key.Substring(metadataPrefix.Length), string.Join(", ", header.Value.ToArray()));
                else
                    headers.Add(header.Key, string.Join(", ", header.Value.ToArray()));
            }

            if (responseMessage.Content != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in responseMessage.Content.Headers)
                {
                    if (header.Key.StartsWith(metadataPrefix))
                        metadata.Add(header.Key.Substring(metadataPrefix.Length), string.Join(", ", header.Value.ToArray()));
                    else
                        headers.Add(header.Key, string.Join(", ", header.Value.ToArray()));
                }
            }

            _headers = headers;
            _metadata = metadata;
        }

        protected StorageMetadata(IDictionary<string, string> headers, IDictionary<string, string> metadata)
        {
            if (headers == null)
                throw new ArgumentNullException("headers");
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            _headers = headers;
            _metadata = metadata;
        }

        public ReadOnlyDictionary<string, string> Headers
        {
            get
            {
                return new ReadOnlyDictionary<string, string>(_headers);
            }
        }

        public ReadOnlyDictionary<string, string> Metadata
        {
            get
            {
                return new ReadOnlyDictionary<string, string>(_metadata);
            }
        }
    }
}
