namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class PoolHealthMonitorRequest : ExtensibleJsonObject
    {
        [JsonProperty("health_monitor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private HealthMonitorData _healthMonitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="PoolHealthMonitorRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PoolHealthMonitorRequest()
        {
        }

        public PoolHealthMonitorRequest(HealthMonitorData healthMonitor)
        {
            _healthMonitor = healthMonitor;
        }

        public PoolHealthMonitorRequest(HealthMonitorData healthMonitor, params JProperty[] extensionData)
            : base(extensionData)
        {
            _healthMonitor = healthMonitor;
        }

        public PoolHealthMonitorRequest(HealthMonitorData healthMonitor, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _healthMonitor = healthMonitor;
        }

        public HealthMonitorData HealthMonitor
        {
            get
            {
                return _healthMonitor;
            }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class HealthMonitorData : ExtensibleJsonObject
        {
            [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
            private HealthMonitorId _id;

            /// <summary>
            /// Initializes a new instance of the <see cref="HealthMonitorData"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected HealthMonitorData()
            {
            }

            public HealthMonitorData(HealthMonitorId healthMonitorId)
            {
                _id = healthMonitorId;
            }

            public HealthMonitorData(HealthMonitorId healthMonitorId, params JProperty[] extensionData)
                : base(extensionData)
            {
                _id = healthMonitorId;
            }

            public HealthMonitorData(HealthMonitorId healthMonitorId, IDictionary<string, JToken> extensionData)
                : base(extensionData)
            {
                _id = healthMonitorId;
            }

            public HealthMonitorId HealthMonitorId
            {
                get
                {
                    return _id;
                }
            }
        }
    }
}
