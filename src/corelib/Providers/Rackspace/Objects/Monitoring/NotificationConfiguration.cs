namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationConfiguration
    {
        [JsonProperty("label")]
        private string _label;

        [JsonProperty("type")]
        private NotificationTypeId _type;

        [JsonProperty("details")]
        private NotificationDetails _details;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationConfiguration()
        {
        }

        public NotificationConfiguration(string label, NotificationTypeId notificationTypeId, NotificationDetails details)
        {
            _label = label;
            _type = notificationTypeId;
            _details = details;
        }

        public string Label
        {
            get
            {
                return _label;
            }
        }

        public NotificationTypeId Type
        {
            get
            {
                return _type;
            }
        }

        public NotificationDetails Details
        {
            get
            {
                return _details;
            }
        }
    }
}
