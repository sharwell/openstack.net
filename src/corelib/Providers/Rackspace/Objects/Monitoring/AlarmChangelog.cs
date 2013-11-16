﻿namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmChangelog
    {
        [JsonProperty("id")]
        private AlarmChangelogId _id;

        [JsonProperty("timestamp", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _timestamp;

        [JsonProperty("entity_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private EntityId _entityId;

        [JsonProperty("alarm_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private AlarmId _alarmId;

        [JsonProperty("check_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private CheckId _checkId;

        [JsonProperty("analyzed_by_monitoring_zone_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private MonitoringZoneId _analyzedByMonitoringZoneId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmChangelog"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AlarmChangelog()
        {
        }

        public AlarmChangelogId Id
        {
            get
            {
                return _id;
            }
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

        public EntityId EntityId
        {
            get
            {
                return _entityId;
            }
        }

        public AlarmId AlarmId
        {
            get
            {
                return _alarmId;
            }
        }

        public CheckId CheckId
        {
            get
            {
                return _checkId;
            }
        }

        public MonitoringZoneId AnalyzedByMonitoringZoneId
        {
            get
            {
                return _analyzedByMonitoringZoneId;
            }
        }
    }
}