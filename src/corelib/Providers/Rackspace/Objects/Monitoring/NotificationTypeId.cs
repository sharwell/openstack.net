namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the unique identifier of a notification type in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <seealso cref="NotificationType.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(NotificationTypeId.Converter))]
    public sealed class NotificationTypeId : ResourceIdentifier<NotificationTypeId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationTypeId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public NotificationTypeId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="NotificationTypeId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override NotificationTypeId FromValue(string id)
            {
                return new NotificationTypeId(id);
            }
        }
    }
}
