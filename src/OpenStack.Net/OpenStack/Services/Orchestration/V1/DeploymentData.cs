namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.Services.Identity;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Compute;

    [JsonObject(MemberSerialization.OptIn)]
    public class DeploymentData : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DeploymentStatus _status;

        [JsonProperty("status_reason", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _statusReason;

        [JsonProperty("server_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ServerId _serverId;

        [JsonProperty("config_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SoftwareConfigurationId _configurationId;

        [JsonProperty("action", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _action;

        [JsonProperty("stack_user_project_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _projectId;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DeploymentData()
        {
        }

        public DeploymentData(ServerId serverId, SoftwareConfigurationId configurationId)
        {
        }
    }
}
