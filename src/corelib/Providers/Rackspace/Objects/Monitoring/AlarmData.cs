namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmData
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Timestamp"/> property.
        /// </summary>
        [JsonProperty("timestamp")]
        private long? _timestamp;

        /// <summary>
        /// This is the backing field for the <see cref="State"/> property.
        /// </summary>
        [JsonProperty("state")]
        private AlarmState _state;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("status")]
        private string _status;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AlarmData()
        {
        }

        public DateTimeOffset? Timestamp
        {
            get
            {
                return DateTimeOffsetExtensions.ToDateTimeOffset(_timestamp);
            }
        }

        public AlarmState State
        {
            get
            {
                return _state;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
        }
    }
}
