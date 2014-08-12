namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResizeVolumeData : ExtensibleJsonObject
    {
        [JsonProperty("volume", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VolumeData _volume;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeVolumeData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResizeVolumeData()
        {
        }

        public ResizeVolumeData(VolumeData volume)
        {
            _volume = volume;
        }

        public ResizeVolumeData(VolumeData volume, params JProperty[] extensionData)
            : base(extensionData)
        {
            _volume = volume;
        }

        public ResizeVolumeData(VolumeData volume, IDictionary<string, JToken> extensionData)
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

        [JsonObject(MemberSerialization.OptIn)]
        public class VolumeData : ExtensibleJsonObject
        {
            [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
            private int? _size;

            /// <summary>
            /// Initializes a new instance of the <see cref="VolumeData"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected VolumeData()
            {
            }

            public VolumeData(int? size)
            {
                _size = size;
            }

            public VolumeData(int? size, params JProperty[] extensionData)
                : base(extensionData)
            {
                _size = size;
            }

            public VolumeData(int? size, IDictionary<string, JToken> extensionData)
                : base(extensionData)
            {
                _size = size;
            }

            public int? Size
            {
                get
                {
                    return _size;
                }
            }
        }
    }
}