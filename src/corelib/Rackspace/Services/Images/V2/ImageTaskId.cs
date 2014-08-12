namespace Rackspace.Services.Images.V2
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the unique identifier of an image.
    /// </summary>
    /// <seealso cref="ImageTask{TInput}.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageTaskId.Converter))]
    public sealed class ImageTaskId : ResourceIdentifier<ImageTaskId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTaskId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The image identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public ImageTaskId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageTaskId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageTaskId FromValue(string id)
            {
                return new ImageTaskId(id);
            }
        }
    }
}
