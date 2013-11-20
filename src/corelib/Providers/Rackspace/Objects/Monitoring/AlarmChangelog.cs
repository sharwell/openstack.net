﻿namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of a Changelog resource in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-changelogs.html">Changelogs (Rackspace Cloud Monitoring Developer Guide - API v1.0)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmChangelog
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private AlarmChangelogId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Timestamp"/> property.
        /// </summary>
        [JsonProperty("timestamp", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _timestamp;

        /// <summary>
        /// This is the backing field for the <see cref="EntityId"/> property.
        /// </summary>
        [JsonProperty("entity_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private EntityId _entityId;

        /// <summary>
        /// This is the backing field for the <see cref="AlarmId"/> property.
        /// </summary>
        [JsonProperty("alarm_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private AlarmId _alarmId;

        /// <summary>
        /// This is the backing field for the <see cref="CheckId"/> property.
        /// </summary>
        [JsonProperty("check_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private CheckId _checkId;

        /// <summary>
        /// This is the backing field for the <see cref="MonitoringZoneId"/> property.
        /// </summary>
        [JsonProperty("analyzed_by_monitoring_zone_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private MonitoringZoneId _analyzedByMonitoringZoneId;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmChangelog"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AlarmChangelog()
        {
        }

        /// <summary>
        /// Gets the unique identifier associated with the alarm changelog resource.
        /// </summary>
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
                return DateTimeOffsetExtensions.ToDateTimeOffset(_timestamp);
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
