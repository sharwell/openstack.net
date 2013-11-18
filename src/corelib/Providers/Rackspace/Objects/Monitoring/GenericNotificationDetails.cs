namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class GenericNotificationDetails : NotificationDetails
    {
        [JsonExtensionData]
        private IDictionary<string, JToken> _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericNotificationDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected GenericNotificationDetails()
        {
        }

        public GenericNotificationDetails(IDictionary<string, JToken> properties)
        {
            _properties = properties;
        }

        public ReadOnlyDictionary<string, JToken> AdditionalProperties
        {
            get
            {
                return new ReadOnlyDictionary<string, JToken>(_properties);
            }
        }

        /// <inheritdoc/>
        protected internal override bool SupportsNotificationType(NotificationTypeId notificationTypeId)
        {
            return true;
        }
    }
}
