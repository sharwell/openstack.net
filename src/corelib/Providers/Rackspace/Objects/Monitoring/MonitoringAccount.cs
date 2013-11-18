namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class MonitoringAccount : AccountConfiguration
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("id")]
        private MonitoringAccountId _id;
#pragma warning restore 649

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
