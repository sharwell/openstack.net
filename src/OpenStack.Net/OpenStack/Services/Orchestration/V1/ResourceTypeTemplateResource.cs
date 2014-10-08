namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceTypeTemplateResource : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("Type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceTypeName _type;

        /// <summary>
        /// This is the backing field for the <see cref="Properties"/> property.
        /// </summary>
        [JsonProperty("Properties", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, ResourceTypeTemplateResourceProperty> _properties;
#pragma warning restore 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTypeTemplateResource"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceTypeTemplateResource()
        {
        }

        public ResourceTypeName Type
        {
            get
            {
                return _type;
            }
        }

        public ReadOnlyDictionary<string, ResourceTypeTemplateResourceProperty> Properties
        {
            get
            {
                if (_properties == null)
                    return null;

                return new ReadOnlyDictionary<string, ResourceTypeTemplateResourceProperty>(_properties);
            }
        }
    }
}
