namespace OpenStack.Services.BlockStorage.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SnapshotResponse : ExtensibleJsonObject
    {
        [JsonProperty("snapshot", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Snapshot _snapshot;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SnapshotResponse()
        {
        }

        public Snapshot Snapshot
        {
            get
            {
                return _snapshot;
            }
        }
    }
}
