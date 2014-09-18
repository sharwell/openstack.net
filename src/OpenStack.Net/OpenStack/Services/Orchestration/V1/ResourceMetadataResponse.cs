namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an HTTP API response containing metadata associated with a
    /// <see cref="Resource"/> object.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceMetadataResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, JToken> _metadata;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceMetadataResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceMetadataResponse()
        {
        }

        /// <summary>
        /// Gets the metadata associated with the <see cref="Resource"/> object.
        /// </summary>
        /// <value>
        /// <para>A collection of key-value pairs describing the metadata associated with the resource.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyDictionary<string, JToken> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyDictionary<string, JToken>(_metadata);
            }
        }
    }
}