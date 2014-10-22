namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of a resource descriptor within a stack template.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateResource : ExtensibleJsonObject, ITemplateResource
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceTypeName _type;

        /// <summary>
        /// This is the backing field for the <see cref="Properties"/> property.
        /// </summary>
        [JsonProperty("properties", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, JToken> _properties;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _metadata;

        /// <summary>
        /// This is the backing field for the <see cref="Dependencies"/> property.
        /// </summary>
        [JsonProperty("depends_on", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceName[] _dependencies;

        /// <summary>
        /// This is the backing field for the <see cref="UpdatePolicy"/> property.
        /// </summary>
        [JsonProperty("update_policy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _updatePolicy;

        /// <summary>
        /// This is the backing field for the <see cref="DeletionPolicy"/> property.
        /// </summary>
        [JsonProperty("deletion_policy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DeletionPolicy _deletionPolicy;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateResource"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TemplateResource()
        {
        }

        public ResourceTypeName Type
        {
            get
            {
                return _type;
            }
        }

        public ReadOnlyDictionary<string, JToken> Properties
        {
            get
            {
                if (_properties == null)
                    return null;

                return new ReadOnlyDictionary<string, JToken>(_properties);
            }
        }

        public ReadOnlyCollection<ResourceName> Dependencies
        {
            get
            {
                if (_dependencies == null)
                    return null;

                return new ReadOnlyCollection<ResourceName>(_dependencies);
            }
        }

        public JToken UpdatePolicy
        {
            get
            {
                return _updatePolicy;
            }
        }

        public DeletionPolicy DeletionPolicy
        {
            get
            {
                return _deletionPolicy;
            }
        }
    }
}
