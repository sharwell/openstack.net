namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class MonitoringZone
    {
        [JsonProperty("id")]
        private MonitoringZoneId _id;

        [JsonProperty("label")]
        private string _label;

        [JsonProperty("country_code")]
        private string _countryCode;

        [JsonProperty("source_ips")]
        private string[] _sourceAddresses;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringZone"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MonitoringZone()
        {
        }

        public MonitoringZoneId Id
        {
            get
            {
                return _id;
            }
        }

        public string Label
        {
            get
            {
                return _label;
            }
        }

        public string CountryCode
        {
            get
            {
                return _countryCode;
            }
        }

        public ReadOnlyCollection<string> SourceAddresses
        {
            get
            {
                if (_sourceAddresses == null)
                    return null;

                return new ReadOnlyCollection<string>(_sourceAddresses);
            }
        }
    }
}
