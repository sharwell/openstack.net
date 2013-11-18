namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class BoundAlarmExample
    {
        [JsonProperty("bound_criteria")]
        private string _boundCriteria;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundAlarmExample"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected BoundAlarmExample()
        {
        }

        public string BoundCriteria
        {
            get
            {
                return _boundCriteria;
            }
        }
    }
}
