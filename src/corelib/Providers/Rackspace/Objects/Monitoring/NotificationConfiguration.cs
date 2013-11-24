namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class models the configurable properties of the JSON representation of
    /// a notification resource in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <seealso cref="IMonitoringService.CreateNotificationAsync"/>
    /// <see href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html">Notifications (Rackspace Cloud Monitoring Developer Guide - API v1.0)</see>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Label"/> property.
        /// </summary>
        [JsonProperty("label")]
        private string _label;

        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type")]
        private NotificationTypeId _type;

        /// <summary>
        /// This is the backing field for the <see cref="Details"/> property.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationConfiguration"/> class
        /// with the specified properties.
        /// </summary>
        /// <param name="label">The friendly name of the notification.</param>
        /// <param name="notificationTypeId">The notification type ID. This is obtained from <see cref="NotificationType.Id">NotificationType.Id</see>, or from the predefined values in <see cref="NotificationTypeId"/>.</param>
        /// <param name="details">A <see cref="NotificationDetails"/> object containing the detailed configuration properties for the specified notification type.</param>
        /// <param name="metadata">A collection of metadata to associate with the notification. If the value is <c>null</c>, no custom metadata is associated with the notification.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="label"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="notificationTypeId"/> is <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="details"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="label"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="details"/> does not support notifications of type <paramref name="notificationTypeId"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="metadata"/> contains any <c>null</c> or empty keys.</para>
        /// </exception>
        public NotificationConfiguration(string label, NotificationTypeId notificationTypeId, NotificationDetails details, IDictionary<string, string> metadata = null)
        {
            if (label == null)
                throw new ArgumentNullException("label");
            if (notificationTypeId == null)
                throw new ArgumentNullException("notificationTypeId");
            if (details == null)
                throw new ArgumentNullException("details");
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("label cannot be empty");
            if (!details.SupportsNotificationType(notificationTypeId))
                throw new ArgumentException(string.Format("The notification details object does not support '{0}' notifications.", notificationTypeId), "details");

            _label = label;
            _type = notificationTypeId;
            _details = JObject.FromObject(details);
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
        /// Gets a collection of metadata associated with the notification.
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
