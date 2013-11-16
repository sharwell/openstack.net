namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Agent tokens are used to authenticate monitoring agents to the monitoring
    /// service. Multiple agents can share a single token.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AgentToken : AgentTokenConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private AgentTokenId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Token"/> property.
        /// </summary>
        [JsonProperty("token")]
        private string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentToken"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AgentToken()
        {
        }

        public AgentTokenId Id
        {
            get
            {
                return _id;
            }
        }

        public string Token
        {
            get
            {
                return _token;
            }
        }
    }
}
