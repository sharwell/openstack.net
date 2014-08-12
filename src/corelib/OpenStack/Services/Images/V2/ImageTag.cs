namespace OpenStack.Services.Images.V2
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the unique identifier of an image tag.
    /// </summary>
    /// <seealso cref="IImageService.PrepareCreateImageTagAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageTag.Converter))]
    public sealed class ImageTag : ResourceIdentifier<ImageTag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTag"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The image tag identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public ImageTag(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageTag"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageTag FromValue(string id)
            {
                return new ImageTag(id);
            }
        }
    }
}
