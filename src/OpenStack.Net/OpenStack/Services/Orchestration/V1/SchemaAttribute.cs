namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an attribute in a resource type schema in the OpenStack
    /// Orchestration Service.
    /// </summary>
    /// <seealso cref="ResourceTypeSchema.Attributes"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class SchemaAttribute : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaAttribute"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SchemaAttribute()
        {
        }

        /// <summary>
        /// Gets a description of the schema attribute.
        /// </summary>
        /// <value>
        /// <para>A description of the schema attribute.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string Description
        {
            get
            {
                return _description;
            }
        }
    }
}
