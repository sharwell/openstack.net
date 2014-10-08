namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SoftwareConfigurationData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Inputs"/> property.
        /// </summary>
        [JsonProperty("inputs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _inputs;

        /// <summary>
        /// This is the backing field for the <see cref="Group"/> property.
        /// </summary>
        [JsonProperty("group", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _group;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Outputs"/> property.
        /// </summary>
        [JsonProperty("outputs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _outputs;

        /// <summary>
        /// This is the backing field for the <see cref="Configuration"/> property.
        /// </summary>
        [JsonProperty("config", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _config;

        /// <summary>
        /// This is the backing field for the <see cref="Options"/> property.
        /// </summary>
        [JsonProperty("options", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareConfigurationData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SoftwareConfigurationData()
        {
        }

        /// <summary>
        /// Gets the name of the software configuration.
        /// </summary>
        /// <value>
        /// <para>The name of the software configuration.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
