namespace OpenStack.Services.BlockStorage.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class VolumeResponse : ExtensibleJsonObject
    {
        [JsonProperty("volume", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Volume _volume;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected VolumeResponse()
        {
        }

        public Volume Volume
        {
            get
            {
                return _volume;
            }
        }
    }
}
