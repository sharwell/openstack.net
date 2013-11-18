namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Agent
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
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
#pragma warning restore 649

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
                return DateTimeOffsetExtensions.ToDateTimeOffset(_lastConnected);
            }
        }
    }
}
