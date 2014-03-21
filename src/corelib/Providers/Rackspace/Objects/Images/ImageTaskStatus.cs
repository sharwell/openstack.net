namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    [JsonConverter(typeof(ImageTaskStatus.Converter))]
    public sealed class ImageTaskStatus : ExtensibleEnum<ImageTaskStatus>
    {
        private static readonly ConcurrentDictionary<string, ImageTaskStatus> _states =
            new ConcurrentDictionary<string, ImageTaskStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly ImageTaskStatus _pending = FromName("pending");
        private static readonly ImageTaskStatus _processing = FromName("processing");
        private static readonly ImageTaskStatus _success = FromName("success");
        private static readonly ImageTaskStatus _failure = FromName("failure");

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTaskStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageTaskStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ImageTaskStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageTaskStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new ImageTaskStatus(i));
        }

        /// <summary>
        /// Gets an <see cref="ImageTaskStatus"/> instance representing <placeholder>placeholder</placeholder>.
        /// </summary>
        public static ImageTaskStatus Pending
        {
            get
            {
                return _pending;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageTaskStatus"/> instance representing <placeholder>placeholder</placeholder>.
        /// </summary>
        public static ImageTaskStatus Processing
        {
            get
            {
                return _processing;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageTaskStatus"/> instance representing <placeholder>placeholder</placeholder>.
        /// </summary>
        public static ImageTaskStatus Success
        {
            get
            {
                return _success;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageTaskStatus"/> instance representing <placeholder>placeholder</placeholder>.
        /// </summary>
        public static ImageTaskStatus Failure
        {
            get
            {
                return _failure;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageTaskStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageTaskStatus FromName(string name)
            {
                return ImageTaskStatus.FromName(name);
            }
        }
    }
}
