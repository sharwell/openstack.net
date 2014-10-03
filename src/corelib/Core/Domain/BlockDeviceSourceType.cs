namespace net.openstack.Core.Domain
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the source resource type for a block device mapping used by the Compute Boot from Volume extension.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known source types,
    /// with added support for unknown types returned by an image extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    [JsonConverter(typeof(BlockDeviceSourceType.Converter))]
    public sealed class BlockDeviceSourceType : ExtensibleEnum<BlockDeviceSourceType>
    {
        private static readonly ConcurrentDictionary<string, BlockDeviceSourceType> _values =
            new ConcurrentDictionary<string, BlockDeviceSourceType>(StringComparer.OrdinalIgnoreCase);
        private static readonly BlockDeviceSourceType _volume = FromName("volume");
        private static readonly BlockDeviceSourceType _image = FromName("image");
        private static readonly BlockDeviceSourceType _snapshot = FromName("snapshot");
        private static readonly BlockDeviceSourceType _blank = FromName("blank");

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockDeviceSourceType"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private BlockDeviceSourceType(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="BlockDeviceSourceType"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="BlockDeviceSourceType"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static BlockDeviceSourceType FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new BlockDeviceSourceType(i));
        }

        /// <summary>
        /// Gets a <see cref="BlockDeviceSourceType"/> representing <placeholder/>.
        /// </summary>
        public static BlockDeviceSourceType Volume
        {
            get
            {
                return _volume;
            }
        }

        /// <summary>
        /// Gets a <see cref="BlockDeviceSourceType"/> representing <placeholder/>.
        /// </summary>
        public static BlockDeviceSourceType Image
        {
            get
            {
                return _image;
            }
        }

        /// <summary>
        /// Gets a <see cref="BlockDeviceSourceType"/> representing <placeholder/>.
        /// </summary>
        public static BlockDeviceSourceType Snapshot
        {
            get
            {
                return _snapshot;
            }
        }

        /// <summary>
        /// Gets a <see cref="BlockDeviceSourceType"/> representing <placeholder/>.
        /// </summary>
        public static BlockDeviceSourceType Blank
        {
            get
            {
                return _blank;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="BlockDeviceSourceType"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override BlockDeviceSourceType FromName(string name)
            {
                return BlockDeviceSourceType.FromName(name);
            }
        }
    }
}
