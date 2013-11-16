namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class MonitoringAccount : AccountConfiguration
    {
        [JsonProperty("id")]
        private MonitoringAccountId _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringAccount"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MonitoringAccount()
        {
        }

        public MonitoringAccountId Id
        {
            get
            {
                return _id;
            }
        }
    }
}
