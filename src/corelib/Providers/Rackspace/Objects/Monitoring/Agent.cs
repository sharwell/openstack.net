namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Agent
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private AgentId _id;

        /// <summary>
        /// This is the backing field for the <see cref="LastConnected"/> property.
        /// </summary>
        [JsonProperty("last_connected", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _lastConnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Agent()
        {
        }

        /// <summary>
        /// Gets the unique identifier of the agent.
        /// </summary>
        public AgentId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the time when the agent last connected to the account.
        /// </summary>
        public DateTimeOffset? LastConnected
        {
            get
            {
                if (_lastConnected == null)
                    return null;

                return new DateTimeOffset(new DateTime(1970, 1, 1), TimeSpan.Zero).AddMilliseconds(_lastConnected.Value);
            }
        }
    }
}
