﻿namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Immutable;
    using System.Net.Http;

    /// <summary>
    /// This class extends <see cref="StorageMetadata"/> to represent metadata associated with an account in the Object
    /// Storage service.
    /// </summary>
    /// <threadsafety static="true" instance="true"/>
    /// <preliminary/>
    public class AccountMetadata : StorageMetadata
    {
        /// <summary>
        /// The prefix to apply to HTTP headers representing custom metadata associated with an account.
        /// </summary>
        public static readonly string AccountMetadataPrefix = "X-Account-Meta-";

        /// <summary>
        /// An empty, immutable instance of <see cref="AccountMetadata"/>. This is the backing
        /// field for the <see cref="Empty"/> property.
        /// </summary>
        private static readonly AccountMetadata _emptyMetadata =
            new AccountMetadata(ImmutableDictionary<string, string>.Empty, ImmutableDictionary<string, string>.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountMetadata"/> class using the metadata present in the
        /// specified response message.
        /// </summary>
        /// <param name="responseMessage">The HTTP response to extract the account metadata from.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="responseMessage"/> is <see langword="null"/>.
        /// </exception>
        public AccountMetadata(HttpResponseMessage responseMessage)
            : base(responseMessage, AccountMetadataPrefix)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountMetadata"/> class with the specified HTTP headers and
        /// custom account metadata.
        /// </summary>
        /// <param name="headers">The custom HTTP headers associated with the account.</param>
        /// <param name="metadata">The custom metadata associated with the account.</param>
        /// <exception cref="ArgumentNullException">
        /// <para>If <paramref name="headers"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="metadata"/> is <see langword="null"/>.</para>
        /// </exception>
        public AccountMetadata(ImmutableDictionary<string, string> headers, ImmutableDictionary<string, string> metadata)
            : base(headers, metadata)
        {
        }

        /// <summary>
        /// Gets an empty <see cref="AccountMetadata"/> instance.
        /// </summary>
        /// <value>
        /// An empty <see cref="AccountMetadata"/> instance.
        /// </value>
        public static AccountMetadata Empty
        {
            get
            {
                return _emptyMetadata;
            }
        }

        /// <inheritdoc/>
        public override string MetadataPrefix
        {
            get
            {
                return AccountMetadataPrefix;
            }
        }
    }
}