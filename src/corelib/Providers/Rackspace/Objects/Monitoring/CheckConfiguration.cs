namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class CheckConfiguration
    {
        [JsonProperty("label")]
        private string _label;

        [JsonProperty("type")]
        private CheckTypeId _type;

        [JsonProperty("details")]
        private CheckDetails _details;

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
            _label = label;
            _type = checkTypeId;
            _details = details;
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
                return _details;
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
