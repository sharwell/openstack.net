namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an HTTP API response containing metadata associated with a
    /// <see cref="Deployment"/> object.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class DeploymentMetadataResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DeploymentMetadata[] _metadata;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentMetadataResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DeploymentMetadataResponse()
        {
        }

        /// <summary>
        /// Gets the metadata associated with the <see cref="Deployment"/> object.
        /// </summary>
        /// <value>
        /// <para>A collection of <see cref="DeploymentMetadata"/> objects describing the metadata associated with the
        /// deployment.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyCollection<DeploymentMetadata> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyCollection<DeploymentMetadata>(_metadata);
            }
        }
    }
}