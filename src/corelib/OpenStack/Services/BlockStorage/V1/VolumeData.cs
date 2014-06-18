namespace OpenStack.Services.BlockStorage.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

#if !NET45PLUS
    using OpenStack.Collections;
#endif

    [JsonObject(MemberSerialization.OptIn)]
    public class VolumeData : ExtensibleJsonObject
    {
        [JsonProperty("volume_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VolumeTypeId _volumeTypeId;

        [JsonProperty("display_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("display_description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _size;

        [JsonProperty("availability_zone", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _availabilityZone;

        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, JToken> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected VolumeData()
        {
        }

        public VolumeData(VolumeTypeId volumeTypeId, string name, int size)
        {
            _volumeTypeId = volumeTypeId;
            _name = name;
            _size = size;
        }

        public VolumeData(VolumeTypeId volumeTypeId, string name, string description, int size, string availabilityZone, IDictionary<string, JToken> metadata, params JProperty[] extensionData)
            : base(extensionData)
        {
            _volumeTypeId = volumeTypeId;
            _name = name;
            _description = description;
            _size = size;
            _availabilityZone = availabilityZone;
            _metadata = metadata;
        }

        public VolumeData(VolumeTypeId volumeTypeId, string name, string description, int size, string availabilityZone, IDictionary<string, JToken> metadata, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _volumeTypeId = volumeTypeId;
            _name = name;
            _description = description;
            _size = size;
            _availabilityZone = availabilityZone;
            _metadata = metadata;
        }

        public VolumeTypeId VolumeTypeId
        {
            get
            {
                return _volumeTypeId;
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

        public int? Size
        {
            get
            {
                return _size;
            }
        }

        public string AvailabilityZone
        {
            get
            {
                return _availabilityZone;
            }
        }

        public ReadOnlyDictionary<string, JToken> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyDictionary<string, JToken>(_metadata);
            }
        }
    }
}
