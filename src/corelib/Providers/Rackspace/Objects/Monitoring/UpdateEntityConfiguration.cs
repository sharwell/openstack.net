namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of a request to update the properties
    /// of an <see cref="Entity"/> resource in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class UpdateEntityConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="AgentId"/> property.
        /// </summary>
        [JsonProperty("agent_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private AgentId _agentId;

        /// <summary>
        /// This is the backing field for the <see cref="Label"/> property.
        /// </summary>
        [JsonProperty("label", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _label;

        /// <summary>
        /// This is the backing field for the <see cref="IPAddresses"/> property.
        /// </summary>
        [JsonProperty("ip_addresses", ItemConverterType = typeof(IPAddressSimpleConverter), DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, IPAddress> _ipAddresses;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEntityConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UpdateEntityConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEntityConfiguration"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="label">The name for the entity. If this parameter is <c>null</c>, the name for the entity is not changed.</param>
        /// <param name="agentId">The agent which this entity is bound to. If this parameter is <c>null</c>, the agent for the entity is not changed.</param>
        /// <param name="ipAddresses">The IP addresses which can be referenced by checks on this entity. If this parameter is <c>null</c>, the IP addresses for the entity are not changed.</param>
        /// <param name="metadata">A collection of metadata to associate with the entity. If this parameter is <c>null</c>, the metadata for the entity is not changed.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="label"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="ipAddresses"/> contains any <c>null</c> or empty keys, or any <c>null</c> values.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="metadata"/> contains any <c>null</c> or empty keys, or any <c>null</c> values.</para>
        /// </exception>
        public UpdateEntityConfiguration(string label = null, AgentId agentId = null, IDictionary<string, IPAddress> ipAddresses = null, IDictionary<string, string> metadata = null)
        {
            if (label == string.Empty)
                throw new ArgumentException("label cannot be empty", "label");

            _label = label;
            _agentId = agentId;
            _ipAddresses = ipAddresses;
            _metadata = metadata;
        }

        /// <summary>
        /// Gets the ID of the agent associated with the monitoring entity.
        /// </summary>
        public AgentId AgentId
        {
            get
            {
                return _agentId;
            }
        }

        /// <summary>
        /// Gets the label for the entity.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
        }

        /// <summary>
        /// Gets a collection of IP addresses which can be referenced by checks on this entity.
        /// </summary>
        public ReadOnlyDictionary<string, IPAddress> IPAddresses
        {
            get
            {
                if (_ipAddresses == null)
                    return null;

                return new ReadOnlyDictionary<string, IPAddress>(_ipAddresses);
            }
        }

        /// <summary>
        /// Gets a collection of custom metadata to associate with the entity.
        /// </summary>
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
