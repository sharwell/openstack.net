namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents status of a <see cref="Stack"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known status values, with added support for unknown
    /// statuses returned or supported by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(StackStatus.Converter))]
    public sealed class StackStatus : ExtensibleEnum<StackStatus>
    {
        private static readonly ConcurrentDictionary<string, StackStatus> _values =
            new ConcurrentDictionary<string, StackStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly StackStatus _createInProgress = FromName("CREATE_IN_PROGRESS");
        private static readonly StackStatus _createComplete = FromName("CREATE_COMPLETE");
        private static readonly StackStatus _deleteInProgress = FromName("DELETE_IN_PROGRESS");
        private static readonly StackStatus _deleteComplete = FromName("DELETE_COMPLETE");

        /// <summary>
        /// Initializes a new instance of the <see cref="StackStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private StackStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="StackStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="StackStatus"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static StackStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new StackStatus(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="StackStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override StackStatus FromName(string name)
            {
                return StackStatus.FromName(name);
            }
        }
    }
}
