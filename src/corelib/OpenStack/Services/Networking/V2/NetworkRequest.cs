﻿namespace OpenStack.Services.Networking.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class NetworkRequest : ExtensibleJsonObject
    {
        [JsonProperty("network", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NetworkData _network;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NetworkRequest()
        {
        }

        public NetworkRequest(NetworkData network)
        {
            _network = network;
        }

        public NetworkRequest(NetworkData network, params JProperty[] extensionData)
            : base(extensionData)
        {
            _network = network;
        }

        public NetworkRequest(NetworkData network, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _network = network;
        }

        public NetworkData Network
        {
            get
            {
                return _network;
            }
        }
    }
}
