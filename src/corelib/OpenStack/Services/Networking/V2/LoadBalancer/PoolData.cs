namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Identity;

    [JsonObject(MemberSerialization.OptIn)]
    public class PoolData : ExtensibleJsonObject
    {
        [JsonProperty("protocol", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private LoadBalancerProtocol _protocol;

        [JsonProperty("lb_method", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private LoadBalancerAlgorithm _algorithm;

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        [JsonProperty("subnet_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SubnetId _subnetId;

        [JsonProperty("health_monitors", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private HealthMonitorId[] _healthMonitors;

        [JsonProperty("admin_state_up", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _adminStateUp;

        [JsonProperty("tenant_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _projectId;

        /// <summary>
        /// Initializes a new instance of the <see cref="PoolData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PoolData()
        {
        }

        public PoolData(LoadBalancerProtocol protocol, LoadBalancerAlgorithm algorithm, string name)
        {
            _protocol = protocol;
            _algorithm = algorithm;
            _name = name;
        }

        public PoolData(LoadBalancerProtocol protocol, LoadBalancerAlgorithm algorithm, string name, string description, SubnetId subnetId, IEnumerable<HealthMonitorId> healthMonitors, bool? adminStateUp, ProjectId projectId, params JProperty[] extensionData)
            : base(extensionData)
        {
            _protocol = protocol;
            _algorithm = algorithm;
            _name = name;
            _description = description;
            _subnetId = subnetId;
            if (healthMonitors != null)
                _healthMonitors = healthMonitors.ToArray();
            _adminStateUp = adminStateUp;
            _projectId = projectId;
        }

        public PoolData(LoadBalancerProtocol protocol, LoadBalancerAlgorithm algorithm, string name, string description, SubnetId subnetId, IEnumerable<HealthMonitorId> healthMonitors, bool? adminStateUp, ProjectId projectId, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _protocol = protocol;
            _algorithm = algorithm;
            _name = name;
            _description = description;
            _subnetId = subnetId;
            if (healthMonitors != null)
                _healthMonitors = healthMonitors.ToArray();
            _adminStateUp = adminStateUp;
            _projectId = projectId;
        }

        public LoadBalancerProtocol Protocol
        {
            get
            {
                return _protocol;
            }
        }

        public LoadBalancerAlgorithm Algorithm
        {
            get
            {
                return _algorithm;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public SubnetId SubnetId
        {
            get
            {
                return _subnetId;
            }
        }

        public ReadOnlyCollection<HealthMonitorId> HealthMonitors
        {
            get
            {
                if (_healthMonitors == null)
                    return null;

                return new ReadOnlyCollection<HealthMonitorId>(_healthMonitors);
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
    }
}
