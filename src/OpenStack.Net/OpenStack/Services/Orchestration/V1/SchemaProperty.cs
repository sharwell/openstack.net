namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of a property in a resource type schema in the OpenStack Orchestration
    /// Service.
    /// </summary>
    /// <seealso cref="ResourceTypeSchema.Properties"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class SchemaProperty : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// This is the backing field for the <see cref="Required"/> property.
        /// </summary>
        [JsonProperty("required", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _required;

        /// <summary>
        /// This is the backing field for the <see cref="UpdateAllowed"/> property.
        /// </summary>
        [JsonProperty("update_allowed", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _updateAllowed;

        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SchemaPropertyType _type;

        /// <summary>
        /// This is the backing field for the <see cref="Immutable"/> property.
        /// </summary>
        [JsonProperty("immutable", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _immutable;

        /// <summary>
        /// This is the backing field for the <see cref="Constraints"/> property.
        /// </summary>
        [JsonProperty("constraints", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SchemaConstraint[] _constraints;

        /// <summary>
        /// This is the backing field for the <see cref="DefaultValue"/> property.
        /// </summary>
        [JsonProperty("default", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _default;

        /// <summary>
        /// This is the backing field for the <see cref="Schema"/> property.
        /// </summary>
        [JsonProperty("schema", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, SchemaProperty> _schema;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaProperty"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SchemaProperty()
        {
        }

        /// <summary>
        /// Gets a description of the schema property.
        /// </summary>
        /// <value>
        /// <para>A description of the schema property.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string Description
        {
            get
            {
                return _description;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the property is required.
        /// </summary>
        /// <value>
        /// <para><see langword="true"/> if the property is required.</para>
        /// <para>-or-</para>
        /// <para><see langword="false"/> if the property is optional.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public bool? Required
        {
            get
            {
                return _required;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the property value may be updated.
        /// </summary>
        /// <value>
        /// <para><see langword="true"/> if the property value may be updated.</para>
        /// <para>-or-</para>
        /// <para><see langword="false"/> if the property value cannot be updated.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public bool? UpdateAllowed
        {
            get
            {
                return _updateAllowed;
            }
        }

        /// <summary>
        /// Gets the property type.
        /// </summary>
        /// <value>
        /// <para>The property type.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public SchemaPropertyType Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the property is immutable.
        /// </summary>
        /// <value>
        /// <para><see langword="true"/> if the property is immutable.</para>
        /// <para>-or-</para>
        /// <para><see langword="false"/> if the property is not immutable.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public bool? Immutable
        {
            get
            {
                return _immutable;
            }
        }

        /// <summary>
        /// Gets a collection of additional constraints which apply to the property.
        /// </summary>
        /// <value>
        /// <para>A collection of <see cref="SchemaConstraint"/> objects describing additional constraints which apply
        /// to the property value.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyCollection<SchemaConstraint> Constraints
        {
            get
            {
                if (_constraints == null)
                    return null;

                return new ReadOnlyCollection<SchemaConstraint>(_constraints);
            }
        }

        /// <summary>
        /// Gets the JSON representation of the default value for the property.
        /// </summary>
        /// <value>
        /// <para>The JSON representation of the default value for the property.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public JToken DefaultValue
        {
            get
            {
                return _default;
            }
        }

        /// <summary>
        /// Gets the sub-schema for values in a <see cref="SchemaPropertyType.List"/> or
        /// <see cref="SchemaPropertyType.Map"/> property.
        /// </summary>
        /// <value>
        /// <para>The sub-schema for values in a <see cref="SchemaPropertyType.List"/> or
        /// <see cref="SchemaPropertyType.Map"/> property.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        /// <seealso cref="Type"/>
        public ReadOnlyDictionary<string, SchemaProperty> Schema
        {
            get
            {
                if (_schema == null)
                    return null;

                return new ReadOnlyDictionary<string, SchemaProperty>(_schema);
            }
        }
    }
}
