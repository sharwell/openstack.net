namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class CheckData
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
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

        [JsonProperty("debug_info")]
        private DebugInformation _debugInfo;
#pragma warning restore 649

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
                return DateTimeOffsetExtensions.ToDateTimeOffset(_timestamp);
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

        public DebugInformation DebugInfo
        {
            get
            {
                return _debugInfo;
            }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class CheckMetric
        {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
            [JsonProperty("type")]
            private CheckMetricType _type;

            [JsonProperty("data")]
            private string _data;
#pragma warning restore 649

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

        [JsonObject(MemberSerialization.OptIn)]
        public class DebugInformation
        {
            [JsonProperty("body")]
            private string _body;

            /// <summary>
            /// Initializes a new instance of the <see cref="DebugInformation"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected DebugInformation()
            {
            }

            public string Body
            {
                get
                {
                    return _body;
                }
            }
        }
    }
}
