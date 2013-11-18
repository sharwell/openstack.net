namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmExampleField
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("name")]
        private string _name;

        [JsonProperty("description")]
        private string _description;

        [JsonProperty("type")]
        private string _type;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmExampleField"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AlarmExampleField()
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

        public string Type
        {
            get
            {
                return _type;
            }
        }
    }
}
