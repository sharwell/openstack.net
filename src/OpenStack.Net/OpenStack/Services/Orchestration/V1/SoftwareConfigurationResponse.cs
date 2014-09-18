namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an HTTP API response containing a
    /// <see cref="V1.SoftwareConfiguration"/> object.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class SoftwareConfigurationResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="SoftwareConfiguration"/> property.
        /// </summary>
        [JsonProperty("software_config", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SoftwareConfiguration _softwareConfiguration;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareConfigurationResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SoftwareConfigurationResponse()
        {
        }

        /// <summary>
        /// Gets the <see cref="V1.SoftwareConfiguration"/> object.
        /// </summary>
        /// <value>
        /// <para>The <see cref="V1.SoftwareConfiguration"/> object</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public SoftwareConfiguration SoftwareConfiguration
        {
            get
            {
                return _softwareConfiguration;
            }
        }
    }
}
