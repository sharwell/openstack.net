namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the key used for sorting image lists returned by the <see cref="IImageService"/>.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known sort keys,
    /// with added support for unknown keys returned by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageSortKey.Converter))]
    public sealed class ImageSortKey : ExtensibleEnum<ImageSortKey>
    {
        private static readonly ConcurrentDictionary<string, ImageSortKey> _states =
            new ConcurrentDictionary<string, ImageSortKey>(StringComparer.OrdinalIgnoreCase);
        private static readonly ImageSortKey _id = FromName("id");
        private static readonly ImageSortKey _name = FromName("name");
        private static readonly ImageSortKey _status = FromName("status");
        private static readonly ImageSortKey _visibility = FromName("visibility");
        private static readonly ImageSortKey _size = FromName("size");
        private static readonly ImageSortKey _createdAt = FromName("createdAt");
        private static readonly ImageSortKey _updatedAt = FromName("updatedAt");

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSortKey"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageSortKey(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets an <see cref="ImageSortKey"/> used to sort a list of images by <see cref="Image.Id"/>.
        /// </summary>
        public static ImageSortKey Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageSortKey"/> used to sort a list of images by <see cref="Image.Name"/>.
        /// </summary>
        public static ImageSortKey ImageName
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageSortKey"/> used to sort a list of images by <see cref="Image.Status"/>.
        /// </summary>
        public static ImageSortKey Status
        {
            get
            {
                return _status;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageSortKey"/> used to sort a list of images by <see cref="Image.Visibility"/>.
        /// </summary>
        public static ImageSortKey Visibility
        {
            get
            {
                return _visibility;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageSortKey"/> used to sort a list of images by <see cref="Image.Size"/>.
        /// </summary>
        public static ImageSortKey Size
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageSortKey"/> used to sort a list of images by <see cref="Image.Created"/>.
        /// </summary>
        public static ImageSortKey Created
        {
            get
            {
                return _createdAt;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageSortKey"/> used to sort a list of images by <see cref="Image.LastModified"/>.
        /// </summary>
        public static ImageSortKey LastModified
        {
            get
            {
                return _updatedAt;
            }
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
