namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
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
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationConfiguration()
        {
        }

        public NotificationConfiguration(string label, NotificationTypeId notificationTypeId, NotificationDetails details, IDictionary<string, string> metadata = null)
        {
            _label = label;
            _type = notificationTypeId;
            if (details != null)
            {
                if (!details.SupportsNotificationType(notificationTypeId))
                    throw new ArgumentException(string.Format("The notification details object does not support '{0}' notifications.", notificationTypeId), "details");

                _details = JObject.FromObject(details);
            }

            _metadata = metadata;
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

        /// <summary>
        /// Gets a collection of metadata associated with the notification plan.
        /// </summary>
        public ReadOnlyDictionary<string, string> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_metadata);
            }
        }
    }
}
