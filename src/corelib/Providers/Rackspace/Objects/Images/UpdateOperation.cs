namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an image update operation.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known operations,
    /// with added support for unknown operations returned by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(UpdateOperation.Converter))]
    public sealed class UpdateOperation : ExtensibleEnum<UpdateOperation>
    {
        private static readonly ConcurrentDictionary<string, UpdateOperation> _states =
            new ConcurrentDictionary<string, UpdateOperation>(StringComparer.OrdinalIgnoreCase);
        private static readonly UpdateOperation _add = FromName("add");
        private static readonly UpdateOperation _remove = FromName("remove");
        private static readonly UpdateOperation _replace = FromName("replace");

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOperation"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private UpdateOperation(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="UpdateOperation"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static UpdateOperation FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new UpdateOperation(i));
        }

        /// <summary>
        /// Gets an <see cref="UpdateOperation"/> instance representing <placeholder>placeholder</placeholder>.
        /// </summary>
        public static UpdateOperation Add
        {
            get
            {
                return _add;
            }
        }

        /// <summary>
        /// Gets an <see cref="UpdateOperation"/> instance representing <placeholder>placeholder</placeholder>.
        /// </summary>
        public static UpdateOperation Remove
        {
            get
            {
                return _remove;
            }
        }

        /// <summary>
        /// Gets an <see cref="UpdateOperation"/> instance representing <placeholder>placeholder</placeholder>.
        /// </summary>
        public static UpdateOperation Replace
        {
            get
            {
                return _replace;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="UpdateOperation"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override UpdateOperation FromName(string name)
            {
                return UpdateOperation.FromName(name);
            }
        }
    }
}
