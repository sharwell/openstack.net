namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationData
    {
        [JsonProperty("status")]
        private string _status;

        [JsonProperty("message")]
        private string _message;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationData()
        {
        }

        public string Status
        {
            get
            {
                return _status;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
