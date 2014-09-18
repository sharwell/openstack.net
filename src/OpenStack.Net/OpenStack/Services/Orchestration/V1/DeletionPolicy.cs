namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents a deletion policy for a stack <see cref="TemplateResource"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known deletion policies, with added support for unknown
    /// policies returned or supported by a server extension.
    /// </remarks>
    /// <seealso href="http://docs.rackspace.com/orchestration/api/v1/orchestration-templates-devguide/content/Defining_Template_dle245.html#resources_d1e142">Resources (Rackspace Cloud Orchestration Templates Developer Guide - API v1.0)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(DeletionPolicy.Converter))]
    public sealed class DeletionPolicy : ExtensibleEnum<DeletionPolicy>
    {
        private static readonly ConcurrentDictionary<string, DeletionPolicy> _values =
            new ConcurrentDictionary<string, DeletionPolicy>(StringComparer.OrdinalIgnoreCase);
        private static readonly DeletionPolicy _delete = FromName("Delete");
        private static readonly DeletionPolicy _retain = FromName("Retain");
        private static readonly DeletionPolicy _snapshot = FromName("Snapshot");

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletionPolicy"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private DeletionPolicy(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets a deletion policy which specifies a resource should be removed when the stack it belongs to is removed.
        /// </summary>
        public DeletionPolicy Delete
        {
            get
            {
                return _delete;
            }
        }

        /// <summary>
        /// Gets a deletion policy which specifies a resource should be retained when the stack it belongs to is
        /// removed.
        /// </summary>
        public DeletionPolicy Retain
        {
            get
            {
                return _retain;
            }
        }

        /// <summary>
        /// Gets a deletion policy which specifies a snapshot should be taken of a resource when the stack it belongs to
        /// is removed.
        /// </summary>
        public DeletionPolicy Snapshot
        {
            get
            {
                return _snapshot;
            }
        }

        /// <summary>
        /// Gets the <see cref="DeletionPolicy"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="DeletionPolicy"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static DeletionPolicy FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new DeletionPolicy(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="DeletionPolicy"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override DeletionPolicy FromName(string name)
            {
                return DeletionPolicy.FromName(name);
            }
        }
    }
}
