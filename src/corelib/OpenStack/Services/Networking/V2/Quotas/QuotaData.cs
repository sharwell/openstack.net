﻿namespace OpenStack.Services.Networking.V2.Quotas
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class QuotaData : ExtensibleJsonObject
    {
        [JsonProperty("network", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _network;

        [JsonProperty("subnet", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _subnet;

        [JsonProperty("port", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _port;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotaData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QuotaData()
        {
        }

        public QuotaData(int? network, int? subnet, int? port)
        {
            _network = network;
            _subnet = subnet;
            _port = port;
        }

        public QuotaData(int? network, int? subnet, int? port, params JProperty[] extensionData)
            : base(extensionData)
        {
            _network = network;
            _subnet = subnet;
            _port = port;
        }

        public QuotaData(int? network, int? subnet, int? port, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _network = network;
            _subnet = subnet;
            _port = port;
        }

        public int? Network
        {
            get
            {
                return _network;
            }
        }

        public int? Subnet
        {
            get
            {
                return _subnet;
            }
        }

        public int? Port
        {
            get
            {
                return _port;
            }
        }
    }
}
