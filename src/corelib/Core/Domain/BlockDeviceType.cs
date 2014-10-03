namespace net.openstack.Core.Domain
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the destination resource type for a block device mapping used by the Compute Boot from Volume
    /// extension.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known destination types,
    /// with added support for unknown types returned by an image extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    [JsonConverter(typeof(BlockDeviceType.Converter))]
    public sealed class BlockDeviceType : ExtensibleEnum<BlockDeviceType>
    {
        private static readonly ConcurrentDictionary<string, BlockDeviceType> _values =
            new ConcurrentDictionary<string, BlockDeviceType>(StringComparer.OrdinalIgnoreCase);
        private static readonly BlockDeviceType _local = FromName("local");
        private static readonly BlockDeviceType _volume = FromName("volume");

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockDeviceType"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private BlockDeviceType(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="BlockDeviceType"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="BlockDeviceType"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static BlockDeviceType FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new BlockDeviceType(i));
        }

        /// <summary>
        /// Gets a <see cref="BlockDeviceType"/> representing <placeholder/>.
        /// </summary>
        public static BlockDeviceType Local
        {
            get
            {
                return _local;
            }
        }

        /// <summary>
        /// Gets a <see cref="BlockDeviceType"/> representing <placeholder/>.
        /// </summary>
        public static BlockDeviceType Volume
        {
            get
            {
                return _volume;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="BlockDeviceType"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override BlockDeviceType FromName(string name)
            {
                return BlockDeviceType.FromName(name);
            }
        }
    }
}
