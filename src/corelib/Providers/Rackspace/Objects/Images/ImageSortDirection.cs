namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the direction used for sorting image lists returned by the <see cref="IImageService"/>.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known sort directions,
    /// with added support for unknown directions returned by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageSortDirection.Converter))]
    public sealed class ImageSortDirection : ExtensibleEnum<ImageSortDirection>
    {
        private static readonly ConcurrentDictionary<string, ImageSortDirection> _states =
            new ConcurrentDictionary<string, ImageSortDirection>(StringComparer.OrdinalIgnoreCase);
        private static readonly ImageSortDirection _asc = FromName("asc");
        private static readonly ImageSortDirection _desc = FromName("desc");

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSortDirection"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageSortDirection(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ImageSortDirection"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageSortDirection FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new ImageSortDirection(i));
        }

        /// <summary>
        /// Gets a <see cref="ImageSortDirection"/> instance representing values sorted from low to high.
        /// </summary>
        public static ImageSortDirection Ascending
        {
            get
            {
                return _asc;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageSortDirection"/> instance representing values sorted from high to low.
        /// </summary>
        public static ImageSortDirection Descending
        {
            get
            {
                return _desc;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageSortDirection"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageSortDirection FromName(string name)
            {
                return ImageSortDirection.FromName(name);
            }
        }
    }
}
