namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class TraceRouteHop
    {
        [JsonProperty("number")]
        private int? _number;

        [JsonProperty("ip")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _ip;

        [JsonProperty("rtts")]
        private double[] _responseTimes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceRouteHop"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TraceRouteHop()
        {
        }

        public int? Number
        {
            get
            {
                return _number;
            }
        }

        public IPAddress IPAddress
        {
            get
            {
                return _ip;
            }
        }

        public ReadOnlyCollection<TimeSpan> ResponseTimes
        {
            get
            {
                if (_responseTimes == null)
                    return null;

                return new ReadOnlyCollection<TimeSpan>(Array.ConvertAll(_responseTimes, TimeSpan.FromSeconds));
            }
        }
    }
}
