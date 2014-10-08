namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class DeploymentMetadata : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DeploymentMetadataId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Group"/> property.
        /// </summary>
        [JsonProperty("group", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _group;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Inputs"/> property.
        /// </summary>
        [JsonProperty("inputs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DeploymentInput[] _inputs;

        /// <summary>
        /// This is the backing field for the <see cref="Outputs"/> property.
        /// </summary>
        [JsonProperty("outputs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DeploymentOutput[] _outputs;

        /// <summary>
        /// This is the backing field for the <see cref="Options"/> property.
        /// </summary>
        [JsonProperty("options", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _options;

        /// <summary>
        /// This is the backing field for the <see cref="Configuration"/> property.
        /// </summary>
        [JsonProperty("config", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentMetadata"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DeploymentMetadata()
        {
        }

        public DeploymentMetadataId Id
        {
            get
            {
                return _id;
            }
        }

        public string Group
        {
            get
            {
                return _group;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ReadOnlyCollection<DeploymentInput> Inputs
        {
            get
            {
                if (_inputs == null)
                    return null;

                return new ReadOnlyCollection<DeploymentInput>(_inputs);
            }
        }

        public ReadOnlyCollection<DeploymentOutput> Outputs
        {
            get
            {
                if (_outputs == null)
                    return null;

                return new ReadOnlyCollection<DeploymentOutput>(_outputs);
            }
        }

        public JToken Options
        {
            get
            {
                return _options;
            }
        }

        public string Configuration
        {
            get
            {
                return _config;
            }
        }
    }
}
