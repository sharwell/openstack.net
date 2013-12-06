namespace net.openstack.Providers.Rackspace.Objects.Backup
{
    using System;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the unique identifier of a <placeholder>item placeholder</placeholder> in the <see cref="PlaceholderService"/>.
    /// </summary>
    /// <seealso cref="SomeItem.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(RestoreConfigurationId.Converter))]
    public sealed class RestoreConfigurationId : ResourceIdentifier<RestoreConfigurationId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreConfigurationId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public RestoreConfigurationId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="RestoreConfigurationId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override RestoreConfigurationId FromValue(string id)
            {
                return new RestoreConfigurationId(id);
            }
        }
    }
}
