namespace net.openstack.Providers.Rackspace.Objects.Hadoop
{
    using System;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the unique identifier of a cluster type in the <see cref="IHadoopService"/>.
    /// </summary>
    /// <seealso cref="ClusterType.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ClusterTypeId.Converter))]
    public sealed class ClusterTypeId : ResourceIdentifier<ClusterTypeId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClusterTypeId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public ClusterTypeId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ClusterTypeId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ClusterTypeId FromValue(string id)
            {
                return new ClusterTypeId(id);
            }
        }
    }
}
