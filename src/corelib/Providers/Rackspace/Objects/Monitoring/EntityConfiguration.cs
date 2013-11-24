namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the configurable properties of the JSON representation of
    /// an entity resource in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <seealso cref="IMonitoringService.CreateEntityAsync"/>
    /// <see href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html">Entities (Rackspace Cloud Monitoring Developer Guide - API v1.0)</see>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
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
        private IDictionary<string, IPAddress> _ipAddresses;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata")]
        private IDictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected EntityConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfiguration"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="label">The name for the entity.</param>
        /// <param name="agentId">The agent which this entity is bound to. If this parameter is <c>null</c>, <placeholder>placeholder</placeholder>.</param>
        /// <param name="ipAddresses">The IP addresses which can be referenced by checks on this entity. If this parameter is <c>null</c>, <placeholder>placeholder</placeholder>.</param>
        /// <param name="metadata">A collection of metadata to associate with the entity. If this parameter is <c>null</c>, the entity is created without any custom metadata.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="label"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="label"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="ipAddresses"/> contains any <c>null</c> or empty keys.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="metadata"/> contains any <c>null</c> or empty keys.</para>
        /// </exception>
        public EntityConfiguration(string label, AgentId agentId, IDictionary<string, IPAddress> ipAddresses, IDictionary<string, string> metadata)
        {
            if (label == null)
                throw new ArgumentNullException("label");
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("label cannot be empty");

            _label = label;
            _agentId = agentId;
            _ipAddresses = ipAddresses;
            if (_ipAddresses != null)
            {
                if (_ipAddresses.ContainsKey(null) || _ipAddresses.ContainsKey(string.Empty))
                    throw new ArgumentException("ipAddresses cannot contain any null or empty keys", "ipAddresses");
            }

            _metadata = metadata;
            if (_metadata != null)
            {
                if (_metadata.ContainsKey(null) || _metadata.ContainsKey(string.Empty))
                    throw new ArgumentException("metadata cannot contain any null or empty keys", "metadata");
            }
        }

        /// <summary>
        /// Gets the ID of the agent which reports information from this entity.
        /// </summary>
        public AgentId AgentId
        {
            get
            {
                return _agentId;
            }
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
        }

        /// <summary>
        /// Gets a dictionary which maps target aliases to IP addresses associated with the entity.
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
        /// Gets a collection of custom metadata associated with the entity.
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
