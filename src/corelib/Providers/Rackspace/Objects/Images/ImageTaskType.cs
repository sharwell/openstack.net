namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    [JsonConverter(typeof(ImageTaskType.Converter))]
    public sealed class ImageTaskType : ExtensibleEnum<ImageTaskType>
    {
        private static readonly ConcurrentDictionary<string, ImageTaskType> _states =
            new ConcurrentDictionary<string, ImageTaskType>(StringComparer.OrdinalIgnoreCase);
        private static readonly ImageTaskType _import = FromName("import");
        private static readonly ImageTaskType _export = FromName("export");

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTaskType"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageTaskType(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ImageTaskType"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageTaskType FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new ImageTaskType(i));
        }

        /// <summary>
        /// Gets an <see cref="ImageTaskType"/> instance representing an image import task.
        /// </summary>
        public static ImageTaskType Import
        {
            get
            {
                return _import;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageTaskType"/> instance representing an image export task.
        /// </summary>
        public static ImageTaskType Export
        {
            get
            {
                return _export;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageTaskType"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageTaskType FromName(string name)
            {
                return ImageTaskType.FromName(name);
            }
        }
    }
}
