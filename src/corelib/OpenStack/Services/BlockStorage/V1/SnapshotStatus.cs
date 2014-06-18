namespace OpenStack.Services.BlockStorage.V1
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the status of a <see cref="Snapshot"/> resource in the Block Storage Service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known snapshot statuses,
    /// with added support for unknown statuses returned by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    [JsonConverter(typeof(SnapshotStatus.Converter))]
    public sealed class SnapshotStatus : ExtensibleEnum<SnapshotStatus>
    {
        private static readonly ConcurrentDictionary<string, SnapshotStatus> _values =
            new ConcurrentDictionary<string, SnapshotStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly SnapshotStatus _creating = FromName("CREATING");
        private static readonly SnapshotStatus _available = FromName("AVAILABLE");
        private static readonly SnapshotStatus _deleting = FromName("DELETING");
        private static readonly SnapshotStatus _error = FromName("ERROR");
        private static readonly SnapshotStatus _errorDeleting = FromName("ERROR_DELETING");

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private SnapshotStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="SnapshotStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="SnapshotStatus"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static SnapshotStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new SnapshotStatus(i));
        }

        /// <summary>
        /// Gets a <see cref="SnapshotStatus"/> indicating the snapshot is being created.
        /// </summary>
        public static SnapshotStatus Creating
        {
            get
            {
                return _creating;
            }
        }

        /// <summary>
        /// Gets a <see cref="SnapshotStatus"/> indicating the snapshot is ready to be used.
        /// </summary>
        public static SnapshotStatus Available
        {
            get
            {
                return _available;
            }
        }

        /// <summary>
        /// Gets a <see cref="SnapshotStatus"/> indicating the snapshot is being deleted.
        /// </summary>
        public static SnapshotStatus Deleting
        {
            get
            {
                return _deleting;
            }
        }

        /// <summary>
        /// Gets a <see cref="SnapshotStatus"/> indicating an error occurred with the snapshot.
        /// </summary>
        public static SnapshotStatus Error
        {
            get
            {
                return _error;
            }
        }

        /// <summary>
        /// Gets a <see cref="SnapshotStatus"/> indicating an error occurred during snapshot deletion.
        /// </summary>
        public static SnapshotStatus ErrorDeleting
        {
            get
            {
                return _errorDeleting;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="SnapshotStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override SnapshotStatus FromName(string name)
            {
                return SnapshotStatus.FromName(name);
            }
        }
    }
}
