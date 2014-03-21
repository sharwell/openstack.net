﻿namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an image tag in the <see cref="IImageService"/>.
    /// </summary>
    /// <seealso cref="Image.Tags"/>
    /// <seealso cref="IImageService.AddImageTagAsync"/>
    /// <seealso cref="IImageService.RemoveImageTagAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ImageTag.Converter))]
    public sealed class ImageTag : ResourceIdentifier<ImageTag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTag"/> class
        /// with the specified tag value.
        /// </summary>
        /// <param name="id">The tag value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <c>null</c>.</exception>
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