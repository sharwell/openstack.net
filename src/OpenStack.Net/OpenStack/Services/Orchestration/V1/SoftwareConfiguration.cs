namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of a software configuration resource in the OpenStack Orchestration
    /// Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class SoftwareConfiguration : SoftwareConfigurationData
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SoftwareConfigurationId _id;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SoftwareConfiguration()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the software configuration resource.
        /// </summary>
        /// <value>
        /// <para>The unique identifier for the software configuration resource.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public SoftwareConfigurationId Id
        {
            get
            {
                return _id;
            }
        }
    }
}
