namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the state of a compute image.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known image statuses,
    /// with added support for unknown statuses returned by an image extension.
    /// </remarks>
    /// <seealso cref="Image.Status"/>
    /// <threadsafety static="true" instance="false"/>
    [JsonConverter(typeof(ImageStatus.Converter))]
    public sealed class ImageStatus : ExtensibleEnum<ImageStatus>
    {
        private static readonly ConcurrentDictionary<string, ImageStatus> _states =
            new ConcurrentDictionary<string, ImageStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly ImageStatus _active = FromName("ACTIVE");
        private static readonly ImageStatus _saving = FromName("SAVING");
        private static readonly ImageStatus _deleted = FromName("DELETED");
        private static readonly ImageStatus _error = FromName("ERROR");
        private static readonly ImageStatus _unknown = FromName("UNKNOWN");

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

            return _states.GetOrAdd(name, i => new ImageStatus(i));
        }

        /// <summary>
        /// Gets an <see cref="ImageStatus"/> representing an image which is active and ready to use.
        /// </summary>
        public static ImageStatus Active
        {
            get
            {
                return _active;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageStatus"/> representing an image currently being saved.
        /// </summary>
        public static ImageStatus Saving
        {
            get
            {
                return _saving;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageStatus"/> representing an image which has been deleted.
        /// </summary>
        /// <remarks>
        /// By default, the <see cref="IComputeService.PrepareListImagesAsync"/> API does not
        /// return images which have been deleted. To list deleted images, use the
        /// <see cref="ComputeServiceExtensions.WithChangesSince(Task{ListImagesApiCall}, DateTimeOffset)"/>
        /// method to modify the API call by adding the <c>changes-since</c> parameter.
        /// </remarks>
        public static ImageStatus Deleted
        {
            get
            {
                return _deleted;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageStatus"/> representing an image which failed to perform
        /// an operation and is now in an error state.
        /// </summary>
        public static ImageStatus Error
        {
            get
            {
                return _error;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageStatus"/> for an image that is currently in an unknown state.
        /// </summary>
        public static ImageStatus Unknown
        {
            get
            {
                return _unknown;
            }
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
