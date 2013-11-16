namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the unique identifier of a <placeholder>item placeholder</placeholder> in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <seealso cref="CheckType.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(CheckTypeId.Converter))]
    public sealed class CheckTypeId : ResourceIdentifier<CheckTypeId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckTypeId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public CheckTypeId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="CheckTypeId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override CheckTypeId FromValue(string id)
            {
                return new CheckTypeId(id);
            }
        }
    }
}
