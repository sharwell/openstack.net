namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmNotificationHistoryItem
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmNotificationHistoryItem"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AlarmNotificationHistoryItem()
        {
        }
    }
}
