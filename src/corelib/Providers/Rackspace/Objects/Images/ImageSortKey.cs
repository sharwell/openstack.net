namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    [JsonConverter(typeof(ImageSortKey.Converter))]
    public sealed class ImageSortKey : ExtensibleEnum<ImageSortKey>
    {
        private static readonly ConcurrentDictionary<string, ImageSortKey> _states =
            new ConcurrentDictionary<string, ImageSortKey>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSortKey"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageSortKey(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ImageSortKey"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageSortKey FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new ImageSortKey(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageSortKey"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageSortKey FromName(string name)
            {
                return ImageSortKey.FromName(name);
            }
        }
    }
}
