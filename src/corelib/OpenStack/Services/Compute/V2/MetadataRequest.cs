namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

#if !NET45
    using OpenStack.Collections;
#else
    using System.Collections.ObjectModel;
#endif

    /// <summary>
    /// This class models the JSON representation for the request body used by various calls
    /// to update metadata associated with resources in the <see cref="IComputeService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class MetadataRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MetadataRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRequest"/> class
        /// with the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        public MetadataRequest(IDictionary<string, string> metadata)
        {
            _metadata = metadata;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRequest"/> class
        /// with the specified metadata and extension data.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public MetadataRequest(IDictionary<string, string> metadata, params JProperty[] extensionData)
            : base(extensionData)
        {
            _metadata = metadata;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRequest"/> class
        /// with the specified metadata and extension data.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public MetadataRequest(IDictionary<string, string> metadata, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _metadata = metadata;
        }

        /// <summary>
        /// Gets the metadata to associate with a resource in the <see cref="IComputeService"/>.
        /// </summary>
        /// <value>
        /// A read-only dictionary of metadata to associate with a resource in the <see cref="IComputeService"/>.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ReadOnlyDictionary<string, string> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_metadata);
            }
        }
    }
}
