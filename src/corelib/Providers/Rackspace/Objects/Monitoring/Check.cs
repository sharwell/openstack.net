namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Check : CheckConfiguration
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("id")]
        private CheckId _id;
#pragma warning restore 649

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
