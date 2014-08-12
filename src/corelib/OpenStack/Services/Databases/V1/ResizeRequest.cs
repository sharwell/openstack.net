namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResizeRequest : ExtensibleJsonObject
    {
        [JsonProperty("resize", DefaultValueHandling = DefaultValueHandling.Include)]
        private ResizeData _resizeData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResizeRequest()
        {
        }

        public ResizeRequest(ResizeData resizeData)
        {
            _resizeData = resizeData;
        }

        public ResizeRequest(ResizeData resizeData, params JProperty[] extensionData)
            : base(extensionData)
        {
            _resizeData = resizeData;
        }

        public ResizeRequest(ResizeData resizeData, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _resizeData = resizeData;
        }

        public ResizeData ResizeData
        {
            get
            {
                return _resizeData;
            }
        }
    }
}
