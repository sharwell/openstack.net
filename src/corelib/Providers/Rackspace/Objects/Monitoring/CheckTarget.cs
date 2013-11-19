namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class CheckTarget
    {
        [JsonProperty("id")]
        private CheckTargetId _id;

        [JsonProperty("label")]
        private string _label;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckTarget"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CheckTarget()
        {
        }

        public CheckTargetId Id
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
    }
}
