namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class WebhookNotificationDetails : NotificationDetails
    {
        [JsonProperty("url")]
        private string _url;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookNotificationDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected WebhookNotificationDetails()
        {
        }

        public WebhookNotificationDetails(Uri url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            _url = url.ToString();
        }

        /// <summary>
        /// Gets the URI a POST request will be sent to for this notification.
        /// </summary>
        public Uri Url
        {
            get
            {
                if (_url == null)
                    return null;

                return new Uri(_url);
            }
        }

        /// <inheritdoc/>
        protected internal override bool SupportsNotificationType(NotificationTypeId notificationTypeId)
        {
            return notificationTypeId == NotificationTypeId.Webhook;
        }
    }
}
