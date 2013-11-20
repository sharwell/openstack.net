﻿namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Net;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of a monitoring agent connection.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agent-connection">Get Agent Connection (Rackspace Cloud Monitoring Developer Guide - API v1.0)</seealso>
    /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agent-connections">List Agent Connections (Rackspace Cloud Monitoring Developer Guide - API v1.0)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class AgentConnection
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private AgentConnectionId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Guid"/> property.
        /// </summary>
        [JsonProperty("guid")]
        private string _guid;

        /// <summary>
        /// This is the backing field for the <see cref="AgentId"/> property.
        /// </summary>
        [JsonProperty("agent_id")]
        private AgentId _agentId;

        /// <summary>
        /// This is the backing field for the <see cref="Endpoint"/> property.
        /// </summary>
        [JsonProperty("endpoint")]
        private string _endpoint;

        /// <summary>
        /// This is the backing field for the <see cref="ProcessVersion"/> property.
        /// </summary>
        [JsonProperty("process_version")]
        private string _processVersion;

        /// <summary>
        /// This is the backing field for the <see cref="BundleVersion"/> property.
        /// </summary>
        [JsonProperty("bundle_version")]
        private string _bundleVersion;

        /// <summary>
        /// This is the backing field for the <see cref="AgentIp"/> property.
        /// </summary>
        [JsonProperty("agent_ip")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _agentIp;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentConnection"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AgentConnection()
        {
        }

        /// <summary>
        /// Gets the unique identifier of the agent connection resource.
        /// </summary>
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

        /// <summary>
        /// Gets the unique identifier of the <see cref="Agent"/> for the connection.
        /// </summary>
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
