namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationResult
    {
        [JsonProperty("notification_id")]
        private NotificationId _notificationId;

        [JsonProperty("notification_type")]
        private NotificationTypeId _notificationTypeId;

        [JsonProperty("notification_details")]
        private NotificationDetails _notificationDetails;

        [JsonProperty("in_progress")]
        private bool? _inProgress;

        [JsonProperty("message")]
        private string _message;

        [JsonProperty("success")]
        private bool? _success;

        [JsonProperty("attempts")]
        private NotificationAttempt[] _attempts;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationResult"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationResult()
        {
        }
    }
}
