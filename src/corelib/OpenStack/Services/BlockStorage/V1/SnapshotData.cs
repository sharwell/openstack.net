namespace OpenStack.Services.BlockStorage.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SnapshotData : ExtensibleJsonObject
    {
        [JsonProperty("volume_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VolumeId _volumeId;

        [JsonProperty("display_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("display_description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SnapshotData()
        {
        }

        public SnapshotData(VolumeId volumeId, string name)
        {
            _volumeId = volumeId;
            _name = name;
        }

        public SnapshotData(VolumeId volumeId, string name, string description, params JProperty[] extensionData)
            : base(extensionData)
        {
            _volumeId = volumeId;
            _name = name;
            _description = description;
        }

        public SnapshotData(VolumeId volumeId, string name, string description, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _volumeId = volumeId;
            _name = name;
            _description = description;
        }

        public VolumeId VolumeId
        {
            get
            {
                return _volumeId;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }
    }
}
