namespace net.openstack.Providers.Rackspace.Objects.Backup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AgentBackupBehavior
    {
        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("BackupDatacenter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _backupDatacenter;

        /// <summary>
        /// This is the backing field for the <see cref=""/> property.
        /// </summary>
        [JsonProperty("UseServiceNet", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _useServiceNet;
    }
}
