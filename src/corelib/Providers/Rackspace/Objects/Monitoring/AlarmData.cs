namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmData
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("timestamp")]
        private long? _timestamp;

        [JsonProperty("state")]
        private string _state;

        [JsonProperty("status")]
        private string _status;
#pragma warning restore 649

        public DateTimeOffset? Timestamp
        {
            get
            {
                if (_timestamp == null)
                    return null;

                return new DateTimeOffset(new DateTime(1970, 1, 1), TimeSpan.Zero).AddMilliseconds(_timestamp.Value);
            }
        }

#warning this should be an extensible enumeration
        public string State
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
