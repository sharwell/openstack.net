namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    /// <summary>
    /// This class extends <see cref="StorageMetadata"/> to represent metadata associated
    /// with an object in the <see cref="IObjectStorageService"/>.
    /// </summary>
    public class ObjectMetadata : StorageMetadata
    {
        /// <summary>
        /// The prefix for HTTP headers representing custom object metadata.
        /// </summary>
        public static readonly string ObjectMetadataPrefix = "X-Object-Meta-";

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectMetadata"/> class from
        /// the specified <see cref="HttpResponseMessage"/>.
        /// </summary>
        /// <param name="responseMessage">The <see cref="HttpResponseMessage"/> from which to extract the object metadata information.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="responseMessage"/> is <see langword="null"/>.</exception>
        public ObjectMetadata(HttpResponseMessage responseMessage)
            : base(responseMessage, ObjectMetadataPrefix)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectMetadata"/> class with
        /// the specified HTTP headers and custom metadata.
        /// </summary>
        /// <param name="headers">A collection of HTTP headers associated with the object.</param>
        /// <param name="metadata">A collection of custom metadata associated with the object.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="headers"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If<paramref name="metadata"/> is <see langword = "null" />.</para>
        /// </exception>
        public ObjectMetadata(IDictionary<string, string> headers, IDictionary<string, string> metadata)
            : base(headers, metadata)
        {
        }
    }
}
