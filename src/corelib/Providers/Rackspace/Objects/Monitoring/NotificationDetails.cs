﻿namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class NotificationDetails
    {
        /// <summary>
        /// Deserializes a JSON object to a <see cref="NotificationDetails"/> instance of the proper type.
        /// </summary>
        /// <param name="notificationTypeId">The notification type ID.</param>
        /// <param name="obj">The JSON object representing the notification details.</param>
        /// <returns>A <see cref="NotificationDetails"/> object corresponding to the JSON object.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="notificationTypeId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="obj"/> is <c>null</c>.</para>
        /// </exception>
        public static NotificationDetails FromJObject(NotificationTypeId notificationTypeId, JObject obj)
        {
            if (notificationTypeId == null)
                throw new ArgumentNullException("notificationTypeId");
            if (obj == null)
                throw new ArgumentNullException("obj");

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
