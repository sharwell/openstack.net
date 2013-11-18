namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class EmailNotificationDetails : NotificationDetails
    {
        /// <summary>
        /// This is the backing field for the <see cref="Address"/> property.
        /// </summary>
        [JsonProperty("address")]
        private string _address;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected EmailNotificationDetails()
        {
        }

        public EmailNotificationDetails(string address)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("address cannot be empty");

            _address = address;
        }

        /// <summary>
        /// Gets the email address notifications will be sent to.
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }
        }

        /// <inheritdoc/>
        protected internal override bool SupportsNotificationType(NotificationTypeId notificationTypeId)
        {
            return notificationTypeId == NotificationTypeId.Email;
        }
    }
}
