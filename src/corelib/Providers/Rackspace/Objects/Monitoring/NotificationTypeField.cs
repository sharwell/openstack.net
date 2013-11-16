namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationTypeField
    {
        [JsonProperty("name")]
        private string _name;

        [JsonProperty("description")]
        private string _description;

        [JsonProperty("optional")]
        private bool? _optional;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationTypeField"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationTypeField()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public bool? Optional
        {
            get
            {
                return _optional;
            }
        }
    }
}
