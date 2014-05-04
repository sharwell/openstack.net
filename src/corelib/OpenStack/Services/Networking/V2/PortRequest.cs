namespace OpenStack.Services.Networking.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class PortRequest : ExtensibleJsonObject
    {
        [JsonProperty("port", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private PortData _port;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PortRequest()
        {
        }

        public PortRequest(PortData port)
        {
            _port = port;
        }

        public PortRequest(PortData port, params JProperty[] extensionData)
            : base(extensionData)
        {
            _port = port;
        }

        public PortRequest(PortData port, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _port = port;
        }

        public PortData Port
        {
            get
            {
                return _port;
            }
        }
    }
}
