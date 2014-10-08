namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the type of a resource type schema parameter in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known types, with added support for unknown types
    /// returned or supported by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(SchemaPropertyType.Converter))]
    public sealed class SchemaPropertyType : ExtensibleEnum<SchemaPropertyType>
    {
        private static readonly ConcurrentDictionary<string, SchemaPropertyType> _values =
            new ConcurrentDictionary<string, SchemaPropertyType>(StringComparer.OrdinalIgnoreCase);
        private static readonly SchemaPropertyType _string = FromName("string");
        private static readonly SchemaPropertyType _number = FromName("number");
        private static readonly SchemaPropertyType _integer = FromName("integer");
        private static readonly SchemaPropertyType _boolean = FromName("boolean");
        private static readonly SchemaPropertyType _list = FromName("list");
        private static readonly SchemaPropertyType _map = FromName("map");

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaPropertyType"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private SchemaPropertyType(string name)
            : base(name)
        {
        }

        public static SchemaPropertyType String
        {
            get
            {
                return _string;
            }
        }

        public static SchemaPropertyType Number
        {
            get
            {
                return _number;
            }
        }

        public static SchemaPropertyType Integer
        {
            get
            {
                return _integer;
            }
        }

        public static SchemaPropertyType Boolean
        {
            get
            {
                return _boolean;
            }
        }

        public static SchemaPropertyType List
        {
            get
            {
                return _list;
            }
        }

        public static SchemaPropertyType Map
        {
            get
            {
                return _map;
            }
        }

        /// <summary>
        /// Gets the <see cref="SchemaPropertyType"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="SchemaPropertyType"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static SchemaPropertyType FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _values.GetOrAdd(name, i => new SchemaPropertyType(i));
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="SchemaPropertyType"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override SchemaPropertyType FromName(string name)
            {
                return SchemaPropertyType.FromName(name);
            }
        }
    }
}
