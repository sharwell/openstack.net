﻿namespace OpenStack.Services.Networking.V2.Layer3
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExternalGateway : ExtensibleJsonObject
    {
        [JsonProperty("network_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NetworkId _networkId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalGateway"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExternalGateway()
        {
        }

        public ExternalGateway(NetworkId networkId)
        {
            _networkId = networkId;
        }

        public ExternalGateway(NetworkId networkId, params JProperty[] extensionData)
            : base(extensionData)
        {
            _networkId = networkId;
        }

        public ExternalGateway(NetworkId networkId, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _networkId = networkId;
        }

        public NetworkId NetworkId
        {
            get
            {
                return _networkId;
            }
        }
    }
}