namespace OpenStack.Services.Images.V2
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the status of an image resource in the <see cref="IImageService"/>.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known image statuses,
    /// with added support for unknown statuses returned by an image extension.
    /// </remarks>
    /// <seealso cref="Image.Status"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageStatus.Converter))]
    public sealed class ImageStatus : ExtensibleEnum<ImageStatus>
    {
        private static readonly ConcurrentDictionary<string, ImageStatus> _values =
            new ConcurrentDictionary<string, ImageStatus>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ImageStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="ImageStatus"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new ImageStatus(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageStatus FromName(string name)
            {
                return ImageStatus.FromName(name);
            }
        }
    }
}
