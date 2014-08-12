namespace Rackspace.Services.Databases.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class BackupResponse : ExtensibleJsonObject
    {
        [JsonProperty("backup", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Backup _backup;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected BackupResponse()
        {
        }

        public Backup Backup
        {
            get
            {
                return _backup;
            }
        }
    }
}