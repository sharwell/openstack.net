namespace OpenStack.Services.Compute.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

#if !NET45
    using OpenStack.Collections;
#else
    using System.Collections.ObjectModel;
#endif

    /// <summary>
    /// This class models the JSON representation used by various calls returning metadata
    /// for resources in the <see cref="IComputeService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class MetadataResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _metadata;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MetadataResponse()
        {
        }

        /// <summary>
        /// Gets the metadata associated with a resource in the <see cref="IComputeService"/>.
        /// </summary>
        /// <value>
        /// A read-only dictionary of metadata associated with a resource in the <see cref="IComputeService"/>.
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
