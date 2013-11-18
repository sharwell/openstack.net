namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class NotificationDetails
    {
        /// <summary>
        /// Deserializes a JSON object to a <see cref="HealthMonitor"/> instance of the proper type.
        /// </summary>
        /// <param name="jsonObject">The JSON object representing the health monitor.</param>
        /// <returns>A <see cref="HealthMonitor"/> object corresponding to the JSON object.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="jsonObject"/> is <c>null</c>.</exception>
        public static NotificationDetails FromJObject(NotificationTypeId notificationTypeId, JObject obj)
        {
            if (notificationTypeId == null)
                throw new ArgumentNullException("notificationTypeId");

            if (obj == null)
                return null;

            if (notificationTypeId == NotificationTypeId.Webhook)
                return obj.ToObject<WebhookNotificationDetails>();
            else if (notificationTypeId == NotificationTypeId.Email)
                return obj.ToObject<EmailNotificationDetails>();
            else if (notificationTypeId == NotificationTypeId.PagerDuty)
                return obj.ToObject<PagerDutyNotificationDetails>();
            else
                return obj.ToObject<GenericNotificationDetails>();
        }

        protected internal abstract bool SupportsNotificationType(NotificationTypeId notificationTypeId);
    }
}
