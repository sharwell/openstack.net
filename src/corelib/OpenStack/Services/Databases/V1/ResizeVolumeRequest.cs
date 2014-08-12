namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResizeVolumeRequest : ExtensibleJsonObject
    {
        [JsonProperty("resize", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResizeVolumeData _resizeVolumeData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeVolumeRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResizeVolumeRequest()
        {
        }

        public ResizeVolumeRequest(ResizeVolumeData resizeVolumeData)
        {
            _resizeVolumeData = resizeVolumeData;
        }

        public ResizeVolumeRequest(ResizeVolumeData resizeVolumeData, params JProperty[] extensionData)
            : base(extensionData)
        {
            _resizeVolumeData = resizeVolumeData;
        }

        public ResizeVolumeRequest(ResizeVolumeData resizeVolumeData, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _resizeVolumeData = resizeVolumeData;
        }

        public ResizeVolumeData ResizeVolumeData
        {
            get
            {
                return _resizeVolumeData;
            }
        }
    }
}
