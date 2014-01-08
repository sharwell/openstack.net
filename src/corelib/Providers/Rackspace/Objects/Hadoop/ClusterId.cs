namespace net.openstack.Providers.Rackspace.Objects.Hadoop
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
    [JsonConverter(typeof(ClusterId.Converter))]
    public sealed class ClusterId : ResourceIdentifier<ClusterId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClusterId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public ClusterId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ClusterId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ClusterId FromValue(string id)
            {
                return new ClusterId(id);
            }
        }
    }
}
