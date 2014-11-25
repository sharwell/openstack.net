namespace OpenStack.ObjectModel.Converters
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json;

    /// <summary>
    /// This class serves as a general <see cref="JsonConverter"/> implementation for all instances of
    /// <see cref="Optional{T}"/>. Serialization and deserialization of actual values is dispatched to an implementation
    /// of <see cref="Optional{T}.Converter"/> corresponding to the specific type of value.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    internal sealed class OptionalConverter : JsonConverter
    {
        /// <summary>
        /// A cache of <see cref="JsonConverter"/> instances to avoid using reflection for every serialization request.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, JsonConverter> _converters =
            new ConcurrentDictionary<Type, JsonConverter>();

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            JsonConverter converter = GetOrCreateConverter(objectType);
            return converter.CanConvert(objectType);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JsonConverter converter = GetOrCreateConverter(objectType);
            return converter.ReadJson(reader, objectType, existingValue, serializer);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JsonConverter converter = GetOrCreateConverter(value.GetType());
            converter.WriteJson(writer, value, serializer);
        }

        /// <summary>
        /// Get a <see cref="JsonConverter"/> which matches a specific closed generic type of <see cref="Optional{T}"/>.
        /// </summary>
        /// <param name="objectType">The closed generic type of <see cref="Optional{T}"/> to get a converter
        /// for.</param>
        /// <returns>An instance of <see cref="Optional{T}.Converter"/> corresponding to the same generic arguments as
        /// <paramref name="objectType"/>.</returns>
        private static JsonConverter GetOrCreateConverter(Type objectType)
        {
            return _converters.GetOrAdd(objectType, CreateConverter);
        }

        private static JsonConverter CreateConverter(Type objectType)
        {
            Type genericTypeDefinition = objectType.GetGenericTypeDefinition();
            Type converterType = typeof(Optional<>.Converter).MakeGenericType(GetGenericArguments(objectType));
            return (JsonConverter)Activator.CreateInstance(converterType);
        }

        private static Type[] GetGenericArguments(Type objectType)
        {
#if NET45PLUS && PORTABLE
            return objectType.GenericTypeArguments;
#else
            return objectType.GetGenericArguments();
#endif
        }
    }
}
