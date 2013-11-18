namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class HostInformation<T>
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("timestamp")]
        private long? _timestamp;

        [JsonProperty("info")]
        private T[] _info;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="HostInformation{T}"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected HostInformation()
        {
        }

        public DateTimeOffset? Timestamp
        {
            get
            {
                return DateTimeOffsetExtensions.ToDateTimeOffset(_timestamp);
            }
        }

        public ReadOnlyCollection<T> Info
        {
            get
            {
                if (_info == null)
                    return null;

                return new ReadOnlyCollection<T>(_info);
            }
        }
    }
}
