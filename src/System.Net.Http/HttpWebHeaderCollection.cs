namespace System.Net.Http
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    /// <summary>
    /// Contains protocol headers associated with a REST request or response.
    /// </summary>
    /// <remarks>
    /// This collection does restrict headers which are exposed through properties,
    /// allowing users to explicitly construct a complete set of headers.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    [Serializable]
    internal class HttpWebHeaderCollection : WebHeaderCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebHeaderCollection"/> class.
        /// </summary>
        public HttpWebHeaderCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebHeaderCollection"/> class from the specified
        /// instances of the <see cref="SerializationInfo"/> and <see cref="StreamingContext"/> classes.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> containing the information required to serialize the <see cref="HttpWebHeaderCollection"/>.</param>
        /// <param name="context">A <see cref="StreamingContext"/> containing the source of the serialized stream associated with the new <see cref="HttpWebHeaderCollection"/>.</param>
        /// <remarks>
        /// This constructor implements the <see cref="ISerializable"/> interface for the <see cref="HttpWebHeaderCollection"/> class.
        /// </remarks>
        /// <exception cref="ArgumentException">If a serialized header name contains invalid characters.</exception>
        /// <exception cref="ArgumentNullException">If a serialized header name is a null reference or <see cref="string.Empty"/>.</exception>
        protected HttpWebHeaderCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Inserts a header with the specified name and value into the collection.
        /// </summary>
        /// <remarks>
        /// If the header specified in <paramref name="name"/> does not exist, the <see cref="Add"/> method
        /// inserts a new header into the list of header name/value pairs.
        ///
        /// <para>If the header specified in <paramref name="name"/> is already present, <paramref name="value"/>
        /// is added to the existing comma-separated list of values associated with <paramref name="name"/>.</para>
        /// </remarks>
        /// <param name="name">The header to add to the collection.</param>
        /// <param name="value">The content of the header.</param>
        /// <exception cref="ArgumentException">
        /// A <paramref name="name"/> is <see langword="null"/>, <see cref="string.Empty"/>, or contains invalid characters.
        /// <para>-or-</para>
        /// <para>A <paramref name="value"/> contains invalid characters.</para>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">The length of <paramref name="value"/> is greater than 65535.</exception>
        public override void Add(string name, string value)
        {
            AddWithoutValidate(name, value);
        }
    }
}
