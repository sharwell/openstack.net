namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationDetails
    {
        [JsonProperty("url")]
        private string _url;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationDetails()
        {
        }
    }
}
