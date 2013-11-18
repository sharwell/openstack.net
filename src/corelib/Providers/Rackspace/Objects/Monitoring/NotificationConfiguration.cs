namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationConfiguration
    {
        [JsonProperty("label")]
        private string _label;

        [JsonProperty("type")]
        private NotificationTypeId _type;

        [JsonProperty("details")]
        private JObject _details;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationConfiguration()
        {
        }

        public NotificationConfiguration(string label, NotificationTypeId notificationTypeId, NotificationDetails details)
        {
            _label = label;
            _type = notificationTypeId;
            if (details != null)
            {
                if (!details.SupportsNotificationType(notificationTypeId))
                    throw new ArgumentException(string.Format("The notification details object does not support '{0}' notifications.", notificationTypeId), "details");

                _details = JObject.FromObject(details);
            }
        }

        public string Label
        {
            get
            {
                return _label;
            }
        }

        public NotificationTypeId Type
        {
            get
            {
                return _type;
            }
        }

        public NotificationDetails Details
        {
            get
            {
                return NotificationDetails.FromJObject(Type, _details);
            }
        }
    }
}
