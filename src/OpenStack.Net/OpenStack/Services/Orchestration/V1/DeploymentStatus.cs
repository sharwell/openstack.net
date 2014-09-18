namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents status of a <see cref="Deployment"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known status values, with added support for unknown
    /// statuses returned or supported by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(DeploymentStatus.Converter))]
    public sealed class DeploymentStatus : ExtensibleEnum<DeploymentStatus>
    {
        private static readonly ConcurrentDictionary<string, DeploymentStatus> _values =
            new ConcurrentDictionary<string, DeploymentStatus>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private DeploymentStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="DeploymentStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="DeploymentStatus"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static DeploymentStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new DeploymentStatus(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="DeploymentStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override DeploymentStatus FromName(string name)
            {
                return DeploymentStatus.FromName(name);
            }
        }
    }
}
