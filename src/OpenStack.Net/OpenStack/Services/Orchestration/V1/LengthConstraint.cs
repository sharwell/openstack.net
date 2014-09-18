namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of a length constraint in the schema for a resource type in the
    /// OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="SchemaConstraint.Length"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class LengthConstraint : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="MinLength"/> property.
        /// </summary>
        [JsonProperty("min", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _min;

        /// <summary>
        /// This is the backing field for the <see cref="MaxLength"/> property.
        /// </summary>
        [JsonProperty("max", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _max;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="LengthConstraint"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected LengthConstraint()
        {
        }

        /// <summary>
        /// Gets the minimum allowed length for the property.
        /// </summary>
        /// <value>
        /// <para>The minimum allowed length for the property.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public int? MinLength
        {
            get
            {
                return _min;
            }
        }

        /// <summary>
        /// Gets the maximum allowed length for the property.
        /// </summary>
        /// <value>
        /// <para>The maximum allowed length for the property.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public int? MaxLength
        {
            get
            {
                return _max;
            }
        }
    }
}
