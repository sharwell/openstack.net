namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class models the JSON representation of a request to update the properties
    /// of a <see cref="Notification"/> resource in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <seealso cref="IMonitoringService.UpdateNotificationAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class UpdateNotificationConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Label"/> property.
        /// </summary>
        [JsonProperty("label", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _label;

        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NotificationTypeId _type;

        /// <summary>
        /// This is the backing field for the <see cref="Details"/> property.
        /// </summary>
        [JsonProperty("details", DefaultValueHandling = DefaultValueHandling.Ignore)]
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
        protected UpdateNotificationConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationConfiguration"/> class
        /// with the specified properties.
        /// </summary>
        /// <param name="label">The friendly name of the notification. If this value is <c>null</c>, the existing value for the notification is not changed.</param>
        /// <param name="notificationTypeId">The notification type ID. This is obtained from <see cref="NotificationType.Id">NotificationType.Id</see>, or from the predefined values in <see cref="NotificationTypeId"/>. If this value is <c>null</c>, the existing value for the notification is not changed.</param>
        /// <param name="details">A <see cref="NotificationDetails"/> object containing the detailed configuration properties for the specified notification type. If this value is <c>null</c>, the existing value for the notification is not changed.</param>
        /// <param name="metadata">A collection of metadata to associate with the notification. If this value is <c>null</c>, the existing value for the notification is not changed.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="label"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="details"/> does not support notifications of type <paramref name="notificationTypeId"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="metadata"/> contains any <c>null</c> or empty keys.</para>
        /// </exception>
        public UpdateNotificationConfiguration(string label = null, NotificationTypeId notificationTypeId = null, NotificationDetails details = null, IDictionary<string, string> metadata = null)
        {
            if (label == string.Empty)
                throw new ArgumentException("label cannot be empty");

            _label = label;
            _type = notificationTypeId;
            if (details != null)
            {
                if (!details.SupportsNotificationType(notificationTypeId))
                    throw new ArgumentException(string.Format("The notification details object does not support '{0}' notifications.", notificationTypeId), "details");

                _details = JObject.FromObject(details);
            }

            _metadata = metadata;
            if (_metadata != null)
            {
                if (_metadata.ContainsKey(null) || _metadata.ContainsKey(string.Empty))
                    throw new ArgumentException("metadata cannot contain any null or empty keys", "metadata");
            }
        }

        /// <summary>
        /// Gets the friendly name of the notification.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
        }

        /// <summary>
        /// Gets the ID of the notification type to send.
        /// </summary>
        public NotificationTypeId Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets the detailed configuration properties for the notification.
        /// </summary>
        public NotificationDetails Details
        {
            get
            {
                if (_details == null)
                    return null;

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
