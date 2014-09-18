namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of a schema for a resource type in the OpenStack Orchestration
    /// Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceTypeSchema : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="ResourceType"/> property.
        /// </summary>
        [JsonProperty("resource_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceTypeName _resourceType;

        /// <summary>
        /// This is the backing field for the <see cref="Attributes"/> property.
        /// </summary>
        [JsonProperty("attributes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, SchemaAttribute> _attributes;

        /// <summary>
        /// This is the backing field for the <see cref="Properties"/> property.
        /// </summary>
        [JsonProperty("properties", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, SchemaProperty> _properties;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTypeSchema"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceTypeSchema()
        {
        }

        /// <summary>
        /// Gets the name of the resource type.
        /// </summary>
        /// <value>
        /// <para>A <see cref="ResourceTypeName"/> identifying the resource type.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ResourceTypeName ResourceType
        {
            get
            {
                return _resourceType;
            }
        }

        /// <summary>
        /// Gets a map from attribute names to <see cref="SchemaAttribute"/> descriptors of the attributes.
        /// </summary>
        /// <value>
        /// <para>A map from attribute names to <see cref="SchemaAttribute"/> descriptors of the attributes.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyDictionary<string, SchemaAttribute> Attributes
        {
            get
            {
                if (_attributes == null)
                    return null;

                return new ReadOnlyDictionary<string, SchemaAttribute>(_attributes);
            }
        }

        /// <summary>
        /// Gets a map from property names to <see cref="SchemaProperty"/> descriptors of the properties.
        /// </summary>
        /// <value>
        /// <para>A map from property names to <see cref="SchemaProperty"/> descriptors of the properties.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyDictionary<string, SchemaProperty> Properties
        {
            get
            {
                if (_properties == null)
                    return null;

                return new ReadOnlyDictionary<string, SchemaProperty>(_properties);
            }
        }
    }
}
