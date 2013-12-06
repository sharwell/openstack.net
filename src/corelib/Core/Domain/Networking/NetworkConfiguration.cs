namespace net.openstack.Core.Domain.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class NetworkConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="AdminStateUp"/> property.
        /// </summary>
        [JsonProperty("admin_state_up", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _adminStateUp;

        /// <summary>
        /// This is the backing field for the <see cref="Shared"/> property.
        /// </summary>
        [JsonProperty("shared", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _shared;

        /// <summary>
        /// This is the backing field for the <see cref="TenantId"/> property.
        /// </summary>
        [JsonProperty("tenant_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _tenantId;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NetworkConfiguration()
        {
        }

        protected NetworkConfiguration(string name, bool? adminStateUp, bool? shared, ProjectId tenantId)
        {
            _name = name;
            _adminStateUp = adminStateUp;
            _shared = shared;
            _tenantId = tenantId;
        }

        /// <summary>
        /// Gets the symbolic name of the network.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets a value indicating the administrative status of the network.
        /// </summary>
        public bool? AdminStateUp
        {
            get
            {
                return _adminStateUp;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the network is shared across all tenants.
        /// </summary>
        /// <remarks>
        /// <note>The default policy setting restricts usage of this attribute to administrative users only.</note>
        /// </remarks>
        public bool? Shared
        {
            get
            {
                return _shared;
            }
        }

        /// <summary>
        /// Gets the unique identifier of the tenant who owns the network.
        /// </summary>
        /// <remarks>
        /// Only administrative users can set the tenant identifier. This policy cannot be changed using authorization policies.
        /// </remarks>
        public ProjectId TenantId
        {
            get
            {
                return _tenantId;
            }
        }
    }
}
