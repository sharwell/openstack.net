namespace OpenStack.Services.BlockStorage.V1
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class Volume : VolumeData
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VolumeId _id;

        [JsonProperty("snapshot_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SnapshotId _snapshotId;

        [JsonProperty("attachments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _attachments;

        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _created;

        /// <summary>
        /// Initializes a new instance of the <see cref="Volume"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Volume()
        {
        }

        public VolumeId Id
        {
            get
            {
                return _id;
            }
        }

        public SnapshotId SnapshotId
        {
            get
            {
                return _snapshotId;
            }
        }

        public JToken Attachments
        {
            get
            {
                return _attachments;
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
