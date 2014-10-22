namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the type of a stack template parameter in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known types, with added support for unknown types
    /// returned or supported by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(TemplateParameterType.Converter))]
    public sealed class TemplateParameterType : ExtensibleEnum<TemplateParameterType>, ITemplateParameterType
    {
        private static readonly ConcurrentDictionary<string, TemplateParameterType> _values =
            new ConcurrentDictionary<string, TemplateParameterType>(StringComparer.OrdinalIgnoreCase);
        private static readonly TemplateParameterType _string = FromName("string");
        private static readonly TemplateParameterType _number = FromName("number");
        private static readonly TemplateParameterType _commaDelimitedList = FromName("comma_delimited_list");
        private static readonly TemplateParameterType _json = FromName("json");

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateParameterType"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private TemplateParameterType(string name)
            : base(name)
        {
        }

        public static TemplateParameterType String
        {
            get
            {
                return _string;
            }
        }

        public static TemplateParameterType Number
        {
            get
            {
                return _number;
            }
        }

        public static TemplateParameterType CommaDelimitedList
        {
            get
            {
                return _commaDelimitedList;
            }
        }

        public static TemplateParameterType Json
        {
            get
            {
                return _json;
            }
        }

        /// <summary>
        /// Gets the <see cref="TemplateParameterType"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="TemplateParameterType"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static TemplateParameterType FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new TemplateParameterType(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="TemplateParameterType"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override TemplateParameterType FromName(string name)
            {
                return TemplateParameterType.FromName(name);
            }
        }
    }
}
