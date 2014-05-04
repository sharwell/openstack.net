namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using OpenStack.ObjectModel.Converters;

#if PORTABLE
    using TAddressFamily = System.String;
#else
    using TAddressFamily = System.Nullable<System.Net.Sockets.AddressFamily>;
#endif

    [JsonObject(MemberSerialization.OptIn)]
    public class SecurityGroupRuleData : ExtensibleJsonObject
    {
        [JsonProperty("security_group_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SecurityGroupId _securityGroupId;

        [JsonProperty("direction", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private RuleDirection _direction;

        [JsonProperty("port_range_min", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _portRangeMin;

        [JsonProperty("port_range_max", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _portRangeMax;

        [JsonProperty("protocol", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private RuleProtocol _protocol;

        [JsonProperty("remote_group_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SecurityGroupId _remoteGroupId;

        [JsonProperty("remote_ip_prefix", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _remoteIpPrefix;

        [JsonProperty("ethertype", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(AddressFamilySimpleConverter))]
        private TAddressFamily _addressFamily;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityGroupRuleData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SecurityGroupRuleData()
        {
        }

        public SecurityGroupRuleData(SecurityGroupId securityGroupId, RuleDirection direction)
        {
            _securityGroupId = securityGroupId;
            _direction = direction;
        }

        public SecurityGroupRuleData(SecurityGroupId securityGroupId, RuleDirection direction, int? portRangeMin, int? portRangeMax, RuleProtocol protocol, SecurityGroupId remoteGroupId, string remoteIpPrefix, TAddressFamily addressFamily, params JProperty[] extensionData)
            : base(extensionData)
        {
            _securityGroupId = securityGroupId;
            _direction = direction;
            _portRangeMin = portRangeMin;
            _portRangeMax = portRangeMax;
            _protocol = protocol;
            _remoteGroupId = remoteGroupId;
            _remoteIpPrefix = remoteIpPrefix;
            _addressFamily = addressFamily;
        }

        public SecurityGroupRuleData(SecurityGroupId securityGroupId, RuleDirection direction, int? portRangeMin, int? portRangeMax, RuleProtocol protocol, SecurityGroupId remoteGroupId, string remoteIpPrefix, TAddressFamily addressFamily, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _securityGroupId = securityGroupId;
            _direction = direction;
            _portRangeMin = portRangeMin;
            _portRangeMax = portRangeMax;
            _protocol = protocol;
            _remoteGroupId = remoteGroupId;
            _remoteIpPrefix = remoteIpPrefix;
            _addressFamily = addressFamily;
        }

        public SecurityGroupId SecurityGroupId
        {
            get
            {
                return _securityGroupId;
            }
        }

        public RuleDirection Direction
        {
            get
            {
                return _direction;
            }
        }

        public int? PortRangeMin
        {
            get
            {
                return _portRangeMin;
            }
        }

        public int? PortRangeMax
        {
            get
            {
                return _portRangeMax;
            }
        }

        public RuleProtocol Protocol
        {
            get
            {
                return _protocol;
            }
        }

        public SecurityGroupId RemoteGroupId
        {
            get
            {
                return _remoteGroupId;
            }
        }

        public string RemoteIpPrefix
        {
            get
            {
                return _remoteIpPrefix;
            }
        }

        public TAddressFamily AddressFamily
        {
            get
            {
                return _addressFamily;
            }
        }
    }
}
