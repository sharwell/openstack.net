namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class CheckConfiguration
    {
        [JsonProperty("label")]
        private string _label;

        [JsonProperty("type")]
        private CheckTypeId _type;

        [JsonProperty("details")]
        private JObject _details;

        [JsonProperty("monitoring_zones_poll")]
        private MonitoringZoneId[] _monitoringZonesPoll;

        [JsonProperty("timeout")]
        private int? _timeout;

        [JsonProperty("period")]
        private int? _period;

        [JsonProperty("target_alias")]
        private string _targetAlias;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CheckConfiguration()
        {
        }

        public CheckConfiguration(string label, CheckTypeId checkTypeId, CheckDetails details, IEnumerable<MonitoringZoneId> monitoringZonesPoll, TimeSpan? timeout, int? period, string targetAlias)
        {
            if (label == null)
                throw new ArgumentNullException("label");
            if (checkTypeId == null)
                throw new ArgumentNullException("checkTypeId");
            if (details == null)
                throw new ArgumentNullException("details");
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("label cannot be empty");
            if (!details.SupportsCheckType(checkTypeId))
                throw new ArgumentException(string.Format("The check details object does not support '{0}' checks.", checkTypeId), "details");

            _label = label;
            _type = checkTypeId;
            _details = JObject.FromObject(details);
            _monitoringZonesPoll = monitoringZonesPoll != null ? monitoringZonesPoll.ToArray() : null;
            _timeout = timeout.HasValue ? (int?)timeout.Value.TotalSeconds : null;
            _period = period;
            _targetAlias = targetAlias;
        }

        public string Label
        {
            get
            {
                return _label;
            }
        }

        public CheckTypeId CheckTypeId
        {
            get
            {
                return _type;
            }
        }

        public CheckDetails Details
        {
            get
            {
                if (_details == null)
                    return null;

                return CheckDetails.FromJObject(CheckTypeId, _details);
            }
        }

        public ReadOnlyCollection<MonitoringZoneId> MonitoringZonesPoll
        {
            get
            {
                if (_monitoringZonesPoll == null)
                    return null;

                return new ReadOnlyCollection<MonitoringZoneId>(_monitoringZonesPoll);
            }
        }

        public TimeSpan? Timeout
        {
            get
            {
                if (_timeout == null)
                    return null;

                return TimeSpan.FromSeconds(_timeout.Value);
            }
        }

        public TimeSpan? Period
        {
            get
            {
                if (_period == null)
                    return null;

                return TimeSpan.FromSeconds(_period.Value);
            }
        }

        public string TargetAlias
        {
            get
            {
                return _targetAlias;
            }
        }
    }
}
