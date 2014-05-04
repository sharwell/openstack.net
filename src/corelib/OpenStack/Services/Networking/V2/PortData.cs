namespace OpenStack.Services.Networking.V2
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Identity;
    using PhysicalAddressSimpleConverter = net.openstack.Core.Domain.Converters.PhysicalAddressSimpleConverter;

#if PORTABLE
    using PhysicalAddress = System.String;
#else
    using PhysicalAddress = System.Net.NetworkInformation.PhysicalAddress;
#endif

    [JsonObject(MemberSerialization.OptIn)]
    public class PortData : ExtensibleJsonObject
    {
        [JsonProperty("network_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NetworkId _networkId;

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private PortStatus _status;

        [JsonProperty("admin_state_up", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _adminStateUp;

        [JsonProperty("tenant_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _projectId;

        [JsonProperty("mac_address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(PhysicalAddressSimpleConverter))]
        private PhysicalAddress _physicalAddress;

        [JsonProperty("fixed_ips", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private FixedIp[] _fixedIps;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PortData()
        {
        }

        public PortData(NetworkId networkId)
        {
            _networkId = networkId;
        }

        public PortData(NetworkId networkId, string name)
        {
            _networkId = networkId;
            _name = name;
        }

        public PortData(NetworkId networkId, string name, PortStatus status, bool? adminStateUp, ProjectId projectId, PhysicalAddress physicalAddress, IEnumerable<FixedIp> fixedIps, params JProperty[] extensionData)
            : base(extensionData)
        {
            _networkId = networkId;
            _name = name;
            _status = status;
            _adminStateUp = adminStateUp;
            _projectId = projectId;
            _physicalAddress = physicalAddress;
            if (fixedIps != null)
                _fixedIps = fixedIps.ToArray();
        }

        public PortData(NetworkId networkId, string name, PortStatus status, bool? adminStateUp, ProjectId projectId, PhysicalAddress physicalAddress, IEnumerable<FixedIp> fixedIps, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _networkId = networkId;
            _name = name;
            _status = status;
            _adminStateUp = adminStateUp;
            _projectId = projectId;
            _physicalAddress = physicalAddress;
            if (fixedIps != null)
                _fixedIps = fixedIps.ToArray();
        }

        public NetworkId NetworkId
        {
            get
            {
                return _networkId;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public PortStatus Status
        {
            get
            {
                return _status;
            }
        }

        public bool? AdminStateUp
        {
            get
            {
                return _adminStateUp;
            }
        }

        public ProjectId ProjectId
        {
            get
            {
                return _projectId;
            }
        }

        public PhysicalAddress PhysicalAddress
        {
            get
            {
                return _physicalAddress;
            }
        }

        public ReadOnlyCollection<FixedIp> FixedAddresses
        {
            get
            {
                if (_fixedIps == null)
                    return null;

                return new ReadOnlyCollection<FixedIp>(_fixedIps);
            }
        }
    }
}
