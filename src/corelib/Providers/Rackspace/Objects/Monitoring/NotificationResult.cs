namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationResult
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
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
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationResult"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationResult()
        {
        }

        public NotificationId NotificationId
        {
            get
            {
                return _notificationId;
            }
        }

        public NotificationTypeId NotificationTypeId
        {
            get
            {
                return _notificationTypeId;
            }
        }

        public NotificationDetails NotificationDetails
        {
            get
            {
                return _notificationDetails;
            }
        }

        public bool? InProgress
        {
            get
            {
                return _inProgress;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public bool? Success
        {
            get
            {
                return _success;
            }
        }

        public ReadOnlyCollection<NotificationAttempt> Attempts
        {
            get
            {
                if (_attempts == null)
                    return null;

                return new ReadOnlyCollection<NotificationAttempt>(_attempts);
            }
        }
    }
}
