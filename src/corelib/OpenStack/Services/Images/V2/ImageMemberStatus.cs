namespace OpenStack.Services.Images.V2
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the status of an image member resource in the <see cref="IImageService"/>.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known image member statuses,
    /// with added support for unknown statuses returned by an image extension.
    /// </remarks>
    /// <seealso cref="Image.Status"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageMemberStatus.Converter))]
    public sealed class ImageMemberStatus : ExtensibleEnum<ImageMemberStatus>
    {
        private static readonly ConcurrentDictionary<string, ImageMemberStatus> _values =
            new ConcurrentDictionary<string, ImageMemberStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly ImageMemberStatus _pending = FromName("pending");
        private static readonly ImageMemberStatus _accepted = FromName("accepted");
        private static readonly ImageMemberStatus _rejected = FromName("rejected");

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMemberStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageMemberStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets a <see cref="ImageMemberStatus"/> instance representing an image member which has been
        /// created, but has not yet been accepted or rejected. The image is not visible in the member's
        /// image list, but the member can still boot instances from the image.
        /// </summary>
        public static ImageMemberStatus Pending
        {
            get
            {
                return _pending;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageMemberStatus"/> instance representing an image member which has been
        /// accepted. The image is visible in the member's image list, and the member can boot instances
        /// from the image.
        /// </summary>
        public static ImageMemberStatus Accepted
        {
            get
            {
                return _accepted;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageMemberStatus"/> instance representing an image member which has been
        /// rejected. The image is not visible in the member's image list, but the member can still boot
        /// instances from the image.
        /// </summary>
        public static ImageMemberStatus Rejected
        {
            get
            {
                return _rejected;
            }
        }

        /// <summary>
        /// Gets the <see cref="ImageMemberStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="ImageMemberStatus"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageMemberStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new ImageMemberStatus(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageMemberStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageMemberStatus FromName(string name)
            {
                return ImageMemberStatus.FromName(name);
            }
        }
    }
}
