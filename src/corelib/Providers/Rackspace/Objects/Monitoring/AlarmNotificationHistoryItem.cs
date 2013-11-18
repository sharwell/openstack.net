namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmNotificationHistoryItem
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("id")]
        private AlarmNotificationHistoryItemId _id;

        [JsonProperty("timestamp")]
        private long? _timestamp;

        [JsonProperty("notification_plan_id")]
        private NotificationPlanId _notificationPlanId;

        [JsonProperty("transaction_id")]
        private TransactionId _transactionId;

        [JsonProperty("status")]
        private string _status;

        [JsonProperty("state")]
        private string _state;

        [JsonProperty("previous_state")]
        private string _previousState;

        [JsonProperty("notification_results")]
        private NotificationResult[] _results;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmNotificationHistoryItem"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AlarmNotificationHistoryItem()
        {
        }

        public AlarmNotificationHistoryItemId Id
        {
            get
            {
                return _id;
            }
        }

        public DateTimeOffset? Timestamp
        {
            get
            {
                if (_timestamp == null)
                    return null;

                return new DateTimeOffset(new DateTime(1970, 1, 1), TimeSpan.Zero).AddMilliseconds(_timestamp.Value);
            }
        }

        public NotificationPlanId NotificationPlanId
        {
            get
            {
                return _notificationPlanId;
            }
        }

        public TransactionId TransactionId
        {
            get
            {
                return _transactionId;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
        }

        public string State
        {
            get
            {
                return _state;
            }
        }

        public string PreviousState
        {
            get
            {
                return _previousState;
            }
        }

        public ReadOnlyCollection<NotificationResult> Results
        {
            get
            {
                if (_results == null)
                    return null;

                return new ReadOnlyCollection<NotificationResult>(_results);
            }
        }
    }
}
