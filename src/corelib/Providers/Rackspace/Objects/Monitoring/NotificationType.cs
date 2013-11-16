﻿namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationType
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private NotificationTypeId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Fields"/> property.
        /// </summary>
        [JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NotificationTypeField[] _fields;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationType"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationType()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the notification type.
        /// </summary>
        public NotificationTypeId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="NotificationTypeField"/> objects describing the fields
        /// of this notification type.
        /// </summary>
        public ReadOnlyCollection<NotificationTypeField> Fields
        {
            get
            {
                if (_fields == null)
                    return null;

                return new ReadOnlyCollection<NotificationTypeField>(_fields);
            }
        }
    }
}