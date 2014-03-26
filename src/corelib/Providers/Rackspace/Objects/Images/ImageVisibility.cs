namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the visibility of an image in the <see cref="IImageService"/>.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known visibilities,
    /// with added support for unknown visibilities returned by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageVisibility.Converter))]
    public sealed class ImageVisibility : ExtensibleEnum<ImageVisibility>
    {
        private static readonly ConcurrentDictionary<string, ImageVisibility> _states =
            new ConcurrentDictionary<string, ImageVisibility>(StringComparer.OrdinalIgnoreCase);
        private static readonly ImageVisibility _public = FromName("public");
        private static readonly ImageVisibility _private = FromName("private");
        private static readonly ImageVisibility _shared = FromName("shared");

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageVisibility"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageVisibility(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ImageVisibility"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageVisibility FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new ImageVisibility(i));
        }

        /// <summary>
        /// Gets a <see cref="ImageVisibility"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageVisibility Public
        {
            get
            {
                return _public;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageVisibility"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageVisibility Private
        {
            get
            {
                return _private;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageVisibility"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageVisibility Shared
        {
            get
            {
                return _shared;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageVisibility"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageVisibility FromName(string name)
            {
                return ImageVisibility.FromName(name);
            }
        }
    }
}
