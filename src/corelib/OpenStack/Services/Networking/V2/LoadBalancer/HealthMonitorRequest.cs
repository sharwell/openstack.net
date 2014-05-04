namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class HealthMonitorRequest : ExtensibleJsonObject
    {
        [JsonProperty("health_monitor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private HealthMonitorData _healthMonitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthMonitorRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected HealthMonitorRequest()
        {
        }

        public HealthMonitorRequest(HealthMonitorData healthMonitor)
        {
            _healthMonitor = healthMonitor;
        }

        public HealthMonitorRequest(HealthMonitorData healthMonitor, params JProperty[] extensionData)
            : base(extensionData)
        {
            _healthMonitor = healthMonitor;
        }

        public HealthMonitorRequest(HealthMonitorData healthMonitor, IDictionary<string, JToken> extensionData)
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
    }
}
