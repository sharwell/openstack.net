namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    public class AgentConnection
    {
        [JsonProperty("id")]
        private AgentConnectionId _id;

        [JsonProperty("guid")]
        private string _guid;

        [JsonProperty("agent_id")]
        private AgentId _agentId;

        [JsonProperty("endpoint")]
        private string _endpoint;

        [JsonProperty("process_version")]
        private string _processVersion;

        [JsonProperty("bundle_version")]
        private string _bundleVersion;

        [JsonProperty("agent_ip")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _agentIp;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentConnection"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AgentConnection()
        {
        }

        public AgentConnectionId Id
        {
            get
            {
                return _id;
            }
        }

        public Guid Guid
        {
            get
            {
                return Guid.Parse(_guid);
            }
        }

        public AgentId AgentId
        {
            get
            {
                return _agentId;
            }
        }

        public string Endpoint
        {
            get
            {
                return _endpoint;
            }
        }

        public string ProcessVersion
        {
            get
            {
                return _processVersion;
            }
        }

        public string BundleVersion
        {
            get
            {
                return _bundleVersion;
            }
        }

        public IPAddress AgentAddress
        {
            get
            {
                return _agentIp;
            }
        }
    }
}
