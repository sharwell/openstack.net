namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Notification : NotificationConfiguration
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private NotificationId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Created"/> property.
        /// </summary>
        [JsonProperty("created_at")]
        private long? _createdAt;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated_at")]
        private long? _updatedAt;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Notification()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the notification resource.
        /// </summary>
        public NotificationId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the notification was first created.
        /// </summary>
        public DateTimeOffset? Created
        {
            get
            {
                return DateTimeOffsetExtensions.ToDateTimeOffset(_createdAt);
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the notification was last modified.
        /// </summary>
        public DateTimeOffset? LastModified
        {
            get
            {
                return DateTimeOffsetExtensions.ToDateTimeOffset(_updatedAt);
            }
        }
    }
}
