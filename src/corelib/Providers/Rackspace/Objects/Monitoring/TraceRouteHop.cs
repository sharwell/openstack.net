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
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("number")]
        private int? _number;

        [JsonProperty("ip")]
        [JsonConverter(typeof(IPAddressConverter))]
        private IPAddress _ip;

        [JsonProperty("hostname")]
        private string _hostname;

        [JsonProperty("rtts")]
        private double[] _responseTimes;
#pragma warning restore 649

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

        public string Hostname
        {
            get
            {
                return _hostname;
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

        /// <summary>
        /// This implementation of <see cref="JsonConverter"/> allows for JSON serialization
        /// and deserialization of <see cref="IPAddress"/> objects using a simple string
        /// representation. Serialization is performed using <see cref="IPAddress.ToString"/>,
        /// and deserialization is performed using <see cref="IPAddress.Parse"/>.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        protected class IPAddressConverter : IPAddressSimpleConverter
        {
            /// <remarks>
            /// If <paramref name="str"/> is an empty string or equal to <c>*</c>, this method returns <c>null</c>.
            /// Otherwise, this method uses <see cref="IPAddress.Parse"/> for deserialization.
            /// </remarks>
            /// <inheritdoc/>
            protected override IPAddress ConvertToObject(string str)
            {
                if (string.IsNullOrEmpty(str))
                    return null;

                if (str == "*")
                    return null;

                return base.ConvertToObject(str);
            }
        }
    }
}
