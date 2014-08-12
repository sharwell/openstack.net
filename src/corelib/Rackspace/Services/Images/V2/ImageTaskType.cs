namespace Rackspace.Services.Images.V2
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the type of an <see cref="ImageTask{TInput}"/> resource.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known task types,
    /// with added support for unknown types returned by a server extension.
    /// </remarks>
    /// <seealso cref="ImageTaskData{TInput}.Type"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageTaskType.Converter))]
    public sealed class ImageTaskType : ExtensibleEnum<ImageTaskType>
    {
        private static readonly ConcurrentDictionary<string, ImageTaskType> _values =
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
        /// <returns>The unique <see cref="ImageTaskType"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageTaskType FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new ImageTaskType(i));
        }

        /// <summary>
        /// Gets a <see cref="ImageTaskType"/> instance representing an asynchronous operation to import an image.
        /// </summary>
        public static ImageTaskType Import
        {
            get
            {
                return _import;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageTaskType"/> instance representing an asynchronous operation to export an image.
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
