namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.Generic;
    using System.Net;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    public class EntityConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="AgentId"/> property.
        /// </summary>
        [JsonProperty("agent_id")]
        private AgentId _agentId;

        /// <summary>
        /// This is the backing field for the <see cref="Label"/> property.
        /// </summary>
        [JsonProperty("label")]
        private string _label;

        /// <summary>
        /// This is the backing field for the <see cref="IPAddresses"/> property.
        /// </summary>
        [JsonProperty("ip_addresses", ItemConverterType = typeof(IPAddressSimpleConverter))]
        private Dictionary<string, IPAddress> _ipAddresses;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata")]
        private Dictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected EntityConfiguration()
        {
        }

        public AgentId AgentId
        {
            get
            {
                return _agentId;
            }
        }

        public string Label
        {
            get
            {
                return _label;
            }
        }

        public ReadOnlyDictionary<string, IPAddress> IPAddresses
        {
            get
            {
                if (_ipAddresses == null)
                    return null;

                return new ReadOnlyDictionary<string, IPAddress>(_ipAddresses);
            }
        }

        public ReadOnlyDictionary<string, string> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_metadata);
            }
        }
    }
}
