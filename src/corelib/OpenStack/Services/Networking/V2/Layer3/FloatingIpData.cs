﻿namespace OpenStack.Services.Networking.V2.Layer3
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    public class FloatingIpData : ExtensibleJsonObject
    {
        [JsonProperty("floating_network_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NetworkId _networkId;

        [JsonProperty("port_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private PortId _portId;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatingIpData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected FloatingIpData()
        {
        }

        public FloatingIpData(NetworkId networkId, PortId portId)
        {
            _networkId = networkId;
            _portId = portId;
        }

        public FloatingIpData(NetworkId networkId, PortId portId, params JProperty[] extensionData)
            : base(extensionData)
        {
            _networkId = networkId;
            _portId = portId;
        }

        public FloatingIpData(NetworkId networkId, PortId portId, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _networkId = networkId;
            _portId = portId;
        }

        public NetworkId NetworkId
        {
            get
            {
                return _networkId;
            }
        }

        public PortId PortId
        {
            get
            {
                return _portId;
            }
        }
    }
}