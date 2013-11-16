namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Notification : NotificationConfiguration
    {
        [JsonProperty("id")]
        private NotificationId _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Notification()
        {
        }

        public NotificationId Id
        {
            get
            {
                return _id;
            }
        }
    }
}
