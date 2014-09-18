namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents status of a stack <see cref="Resource"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known status values, with added support for unknown
    /// statuses returned or supported by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ResourceStatus.Converter))]
    public sealed class ResourceStatus : ExtensibleEnum<ResourceStatus>
    {
        private static readonly ConcurrentDictionary<string, ResourceStatus> _values =
            new ConcurrentDictionary<string, ResourceStatus>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ResourceStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ResourceStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="ResourceStatus"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ResourceStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new ResourceStatus(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ResourceStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ResourceStatus FromName(string name)
            {
                return ResourceStatus.FromName(name);
            }
        }
    }
}
