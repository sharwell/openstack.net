﻿namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents <placeholder/> in the monitoring service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known agent check types,
    /// with added support for unknown types returned by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(AgentCheckType.Converter))]
    public sealed class AgentCheckType : ExtensibleEnum<AgentCheckType>
    {
        private static readonly ConcurrentDictionary<string, AgentCheckType> _types =
            new ConcurrentDictionary<string, AgentCheckType>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentCheckType"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private AgentCheckType(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="AgentCheckType"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static AgentCheckType FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _types.GetOrAdd(name, i => new AgentCheckType(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="AgentCheckType"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override AgentCheckType FromName(string name)
            {
                return AgentCheckType.FromName(name);
            }
        }
    }
}
