namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Check : CheckConfiguration
    {
        [JsonProperty("id")]
        private CheckId _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Check"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Check()
        {
        }

        public CheckId Id
        {
            get
            {
                return _id;
            }
        }
    }
}
