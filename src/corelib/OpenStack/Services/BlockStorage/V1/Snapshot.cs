namespace OpenStack.Services.BlockStorage.V1
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Snapshot : SnapshotData
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SnapshotId _id;

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SnapshotStatus _status;

        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _size;

        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _created;

        /// <summary>
        /// Initializes a new instance of the <see cref="Snapshot"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Snapshot()
        {
        }

        public SnapshotId Id
        {
            get
            {
                return _id;
            }
        }

        public SnapshotStatus Status
        {
            get
            {
                return _status;
            }
        }

        public int? Size
        {
            get
            {
                return _size;
            }
        }

        public DateTimeOffset? Created
        {
            get
            {
                return _created;
            }
        }
    }
}
