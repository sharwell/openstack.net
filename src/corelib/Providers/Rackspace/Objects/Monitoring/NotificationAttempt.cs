namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationAttempt
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("message")]
        private string _message;

        [JsonProperty("from")]
        private string _from;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationAttempt"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationAttempt()
        {
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public string From
        {
            get
            {
                return _from;
            }
        }
    }
}
