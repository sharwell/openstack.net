namespace OpenStack.Services.BlockStorage.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class VolumeTypeResponse : ExtensibleJsonObject
    {
        [JsonProperty("volume_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VolumeType _volumeType;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeTypeResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected VolumeTypeResponse()
        {
        }

        public VolumeType VolumeType
        {
            get
            {
                return _volumeType;
            }
        }
    }
}
