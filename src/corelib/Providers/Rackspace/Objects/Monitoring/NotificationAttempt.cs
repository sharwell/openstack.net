namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationAttempt
    {
        [JsonProperty("message")]
        private string _message;

        [JsonProperty("from")]
        private string _from;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationAttempt"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationAttempt()
        {
        }
    }
}
