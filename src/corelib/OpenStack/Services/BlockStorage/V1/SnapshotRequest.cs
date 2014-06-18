namespace OpenStack.Services.BlockStorage.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SnapshotRequest : ExtensibleJsonObject
    {
        [JsonProperty("snapshot", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SnapshotData _snapshot;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SnapshotRequest()
        {
        }

        public SnapshotRequest(SnapshotData snapshot)
        {
            _snapshot = snapshot;
        }

        public SnapshotRequest(SnapshotData snapshot, params JProperty[] extensionData)
            : base(extensionData)
        {
            _snapshot = snapshot;
        }

        public SnapshotRequest(SnapshotData snapshot, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _snapshot = snapshot;
        }

        public SnapshotData Snapshot
        {
            get
            {
                return _snapshot;
            }
        }
    }
}
