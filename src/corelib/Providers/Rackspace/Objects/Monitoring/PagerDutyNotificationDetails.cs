namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class PagerDutyNotificationDetails : NotificationDetails
    {
        [JsonProperty("service_key")]
        private string _serviceKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerDutyNotificationDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PagerDutyNotificationDetails()
        {
        }

        public PagerDutyNotificationDetails(string serviceKey)
        {
            if (serviceKey == null)
                throw new ArgumentNullException("serviceKey");
            if (string.IsNullOrEmpty(serviceKey))
                throw new ArgumentException("serviceKey cannot be empty");

            _serviceKey = serviceKey;
        }

        /// <summary>
        /// Gets the PagerDuty service key to use for sending notifications.
        /// </summary>
        public string ServiceKey
        {
            get
            {
                return _serviceKey;
            }
        }

        /// <inheritdoc/>
        protected internal override bool SupportsNotificationType(NotificationTypeId notificationTypeId)
        {
            return notificationTypeId == NotificationTypeId.PagerDuty;
        }
    }
}
