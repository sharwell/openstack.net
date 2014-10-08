namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the version of a stack template in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known versions, with added support for unknown
    /// versions returned or supported by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(TemplateVersion.Converter))]
    public sealed class TemplateVersion : ExtensibleEnum<TemplateVersion>
    {
        private static readonly ConcurrentDictionary<string, TemplateVersion> _values =
            new ConcurrentDictionary<string, TemplateVersion>(StringComparer.OrdinalIgnoreCase);
        private static readonly TemplateVersion _v2013_05_23 = FromName("2013-05-23");

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateVersion"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private TemplateVersion(string name)
            : base(name)
        {
        }

        public static TemplateVersion V2013_05_23
        {
            get
            {
                return _v2013_05_23;
            }
        }

        /// <summary>
        /// Gets the <see cref="TemplateVersion"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="TemplateVersion"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static TemplateVersion FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new TemplateVersion(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="TemplateVersion"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override TemplateVersion FromName(string name)
            {
                return TemplateVersion.FromName(name);
            }
        }
    }
}
