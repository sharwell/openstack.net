namespace OpenStack.Services.Networking.V2.Metering
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using RuleDirection = OpenStack.Services.Networking.V2.SecurityGroups.RuleDirection;

    [JsonObject(MemberSerialization.OptIn)]
    public class MeteringLabelRuleData : ExtensibleJsonObject
    {
        [JsonProperty("metering_label_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private MeteringLabelId _meteringLabelId;

#warning this (and similar) should be a CIDR object
        [JsonProperty("remote_ip_prefix", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _remoteIpPrefix;

        [JsonProperty("direction", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private RuleDirection _direction;

        [JsonProperty("excluded", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _excluded;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeteringLabelRuleData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MeteringLabelRuleData()
        {
        }

        public MeteringLabelRuleData(MeteringLabelId meteringLabelId, string remoteIpPrefix)
        {
            _meteringLabelId = meteringLabelId;
            _remoteIpPrefix = remoteIpPrefix;
        }

        public MeteringLabelRuleData(MeteringLabelId meteringLabelId, string remoteIpPrefix, RuleDirection direction, bool? excluded, params JProperty[] extensionData)
            : base(extensionData)
        {
            _meteringLabelId = meteringLabelId;
            _remoteIpPrefix = remoteIpPrefix;
            _direction = direction;
            _excluded = excluded;
        }

        public MeteringLabelRuleData(MeteringLabelId meteringLabelId, string remoteIpPrefix, RuleDirection direction, bool? excluded, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _meteringLabelId = meteringLabelId;
            _remoteIpPrefix = remoteIpPrefix;
            _direction = direction;
            _excluded = excluded;
        }

        public MeteringLabelId MeteringLabelId
        {
            get
            {
                return _meteringLabelId;
            }
        }

        public string RemoteIpPrefix
        {
            get
            {
                return _remoteIpPrefix;
            }
        }

        public RuleDirection Direction
        {
            get
            {
                return _direction;
            }
        }

        public bool? Excluded
        {
            get
            {
                return _excluded;
            }
        }
    }
}
