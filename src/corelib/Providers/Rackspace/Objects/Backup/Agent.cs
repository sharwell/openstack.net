namespace net.openstack.Providers.Rackspace.Objects.Backup
{
    using System.Net;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Agent
    {
        /// <summary>
        /// This is the backing field for the <see cref="AgentVersion"/> property.
        /// </summary>
        [JsonProperty("AgentVersion")]
        private string _agentVersion;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("Architecture")]
        private string _architecture;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("Flavor")]
        private string _flavor;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("BackupVaultSize")]
        private string _backupVaultSize;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("CleanupAllowed")]
        private bool? _cleanupAllowed;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("Datacenter")]
        private string _datacenter;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("IPAddress")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _ipAddress;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("IsDisabled")]
        private bool? _isDisabled;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("IsEncrypted")]
        private bool? _isEncrypted;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("MachineAgentId")]
        private MachineAgentId _machineAgentId;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("MachineName")]
        private string _machineName;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("OperatingSystem")]
        private string _operatingSystem;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("OperatingSystemVersion")]
        private string _operatingSystemVersion;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("PublicKey")]
        private AgentPublicKey _publicKey;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("Status")]
        private AgentStatus _status;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("TimeOfLastSuccessfulBackup")]
        private string _timeOfLastSuccessfulBackup;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("UseServiceNet")]
        private bool? _useServiceNet;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("HostServerId")]
        private ServerId _hostServerId;
    }
}
