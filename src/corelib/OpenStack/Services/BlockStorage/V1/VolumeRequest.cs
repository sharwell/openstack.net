namespace OpenStack.Services.BlockStorage.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class VolumeRequest : ExtensibleJsonObject
    {
        [JsonProperty("volume", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VolumeData _volume;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected VolumeRequest()
        {
        }

        public VolumeRequest(VolumeData volume)
        {
            _volume = volume;
        }

        public VolumeRequest(VolumeData volume, params JProperty[] extensionData)
            : base(extensionData)
        {
            _volume = volume;
        }

        public VolumeRequest(VolumeData volume, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _volume = volume;
        }

        public VolumeData Volume
        {
            get
            {
                return _volume;
            }
        }
    }
}
