namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class CheckData
    {
        [JsonProperty("timestamp")]
        private long? _timestamp;

        [JsonProperty("monitoring_zone_id")]
        private MonitoringZoneId _monitoringZoneId;

        [JsonProperty("available")]
        private bool? _available;

        [JsonProperty("status")]
        private string _status;

        [JsonProperty("metrics")]
        private Dictionary<string, CheckMetric> _metrics;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CheckData()
        {
        }

        public DateTimeOffset? Timestamp
        {
            get
            {
                if (_timestamp == null)
                    return null;

                return new DateTimeOffset(new DateTime(1970, 1, 1), TimeSpan.Zero).AddMilliseconds(_timestamp.Value);
            }
        }

        public MonitoringZoneId MonitoringZoneId
        {
            get
            {
                return _monitoringZoneId;
            }
        }

        public bool? Available
        {
            get
            {
                return _available;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
        }

        public ReadOnlyDictionary<string, CheckMetric> Metrics
        {
            get
            {
                if (_metrics == null)
                    return null;

                return new ReadOnlyDictionary<string, CheckMetric>(_metrics);
            }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class CheckMetric
        {
            [JsonProperty("type")]
            private CheckMetricType _type;

            [JsonProperty("data")]
            private string _data;

            /// <summary>
            /// Initializes a new instance of the <see cref="CheckMetric"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected CheckMetric()
            {
            }

            public CheckMetricType Type
            {
                get
                {
                    return _type;
                }
            }

            public string Data
            {
                get
                {
                    return _data;
                }
            }
        }
    }
}
