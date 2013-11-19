namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ConnectionCheckDetails : CheckDetails
    {
        [JsonProperty("port", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _port;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ConnectionCheckDetails()
        {
        }

        protected ConnectionCheckDetails(int? port)
        {
            if (port <= 0 || port > 65535)
                throw new ArgumentOutOfRangeException("port");

            _port = port;
        }

        public int? Port
        {
            get
            {
                return _port;
            }
        }
    }
}
