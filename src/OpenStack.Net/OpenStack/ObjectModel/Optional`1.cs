namespace OpenStack.ObjectModel
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel.Converters;

    /// <summary>
    /// This structure allows object models representing external data to distinguish between values which are missing
    /// from the underlying data, and values which are present in the data but set to <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of the underlying data.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(OptionalConverter))]
    public struct Optional<T> : IEquatable<Optional<T>>
    {
        /// <summary>
        /// Gets an <see cref="Optional{T}"/> which is initialized with the default value of type
        /// <typeparamref name="T"/>.
        /// </summary>
        public static readonly Optional<T> Default = new Optional<T>(default(T));

        /// <summary>
        /// Gets an <see cref="Optional{T}"/> which does not have a value set.
        /// </summary>
        public static readonly Optional<T> Unset = default(Optional<T>);

        /// <summary>
        /// This is the backing field for the <see cref="HasValue"/> property.
        /// </summary>
        private readonly bool _hasValue;

        /// <summary>
        /// This is the backing field for the <see cref="TryGetValue"/> method.
        /// </summary>
        private readonly T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Optional{T}"/> structure with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public Optional(T value)
        {
            _hasValue = true;
            _value = value;
        }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="Optional{T}"/> was initialized with a value.
        /// </summary>
        /// <remarks>
        /// <para>When used in an object model representing external data, an <see cref="Optional{T}"/> which has a
        /// value, even if that value is <see langword="null"/>, typically represents a value which is "present" or
        /// "set" in the underlying data model. An <see cref="Optional{T}"/> which does not have a value typically
        /// represents a value which is "missing", "unset", or perhaps "not supported" in the underlying data
        /// model.</para>
        /// </remarks>
        /// <value>
        /// <para><see langword="true"/> if the current <see cref="Optional{T}"/> was initialized by a value.</para>
        /// <para>-or-</para>
        /// <para><see langword="false"/> if the current <see cref="Optional{T}"/> does not have a value.</para>
        /// </value>
        public bool HasValue
        {
            get
            {
                return _hasValue;
            }
        }

        /// <summary>
        /// Converts an <see cref="Optional{T}"/> to a value of type <typeparamref name="T"/> by extracting its value.
        /// </summary>
        /// <param name="optional">The <see cref="Optional{T}"/> to unwrap.</param>
        /// <exception cref="InvalidOperationException">If the specified <paramref name="optional"/> does not have a
        /// value.</exception>
        public static explicit operator T(Optional<T> optional)
        {
            return optional.GetValue();
        }

        /// <summary>
        /// Creates an <see cref="Optional{T}"/> wrapper around a specific value of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator Optional<T>(T value)
        {
            return new Optional<T>(value);
        }

        /// <summary>
        /// Gets the value held in the <see cref="Optional{T}"/>.
        /// </summary>
        /// <returns>The value stored in the <see cref="Optional{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">If the current <see cref="Optional{T}"/> does not have a
        /// value.</exception>
        public T GetValue()
        {
            T value;
            if (!TryGetValue(out value))
                throw new InvalidOperationException("The optional object does not have a value.");

            return value;
        }

        /// <summary>
        /// Gets the value held in the <see cref="Optional{T}"/>, or the default value of <typeparamref name="T"/> if
        /// the <see cref="Optional{T}"/> does not have a value.
        /// </summary>
        /// <returns>
        /// <para>The value wrapped in the <see cref="Optional{T}"/>.</para>
        /// <para>-or-</para>
        /// <para>The default value of <typeparamref name="T"/>, if the <see cref="Optional{T}"/> does not have a
        /// value.</para>
        /// </returns>
        public T GetValueOrDefault()
        {
            T value;
            if (!TryGetValue(out value))
                return default(T);

            return value;
        }

        /// <summary>
        /// Gets the value held in the <see cref="Optional{T}"/>, or a specified default value if the current
        /// <see cref="Optional{T}"/> does not have a value.
        /// </summary>
        /// <param name="defaultValue">The value to return if the <see cref="Optional{T}"/> does not have a
        /// value.</param>
        /// <returns>
        /// <para>The value wrapped in the <see cref="Optional{T}"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="defaultValue"/>, if the <see cref="Optional{T}"/> does not have a value.</para>
        /// </returns>
        public T GetValueOrDefault(T defaultValue)
        {
            T value;
            if (!TryGetValue(out value))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Gets the value, if any, which is help in the <see cref="Optional{T}"/>.
        /// </summary>
        /// <param name="value">
        /// <para>The value wrapped in the <see cref="Optional{T}"/>.</para>
        /// <para>-or-</para>
        /// <para>The default value of <typeparamref name="T"/>, if the <see cref="Optional{T}"/> does not have a
        /// value.</para>
        /// </param>
        /// <returns>
        /// <para><see langword="true"/> if the <see cref="Optional{T}"/> has a value.</para>
        /// <para>-or-</para>
        /// <para><see langword="false"/> if the <see cref="Optional{T}"/> does not have a value.</para>
        /// </returns>
        public bool TryGetValue(out T value)
        {
            if (!_hasValue)
            {
                value = default(T);
                return false;
            }

            value = _value;
            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            if (!_hasValue)
                return typeof(T).GetHashCode();

            if (_value == null)
                return ~typeof(T).GetHashCode();

            return _value.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Optional<T>)
                return Equals((Optional<T>)obj);

            // Including this code will cause the JSON serialization to fail because it will detect the wrapped value as
            // a recursive reference.
            //if (obj is T)
            //    return Equals(new Optional<T>((T)obj));

            if (obj == null && default(T) == null)
                return Equals(Default);

            return false;
        }

        /// <inheritdoc/>
        public bool Equals(Optional<T> other)
        {
            if (_hasValue != other._hasValue)
                return false;

            if (!_hasValue)
                return true;

            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        /// <summary>
        /// This class serves as the <see cref="JsonConverter"/> implementation for a specific type argument
        /// <typeparamref name="T"/>.
        /// </summary>
        internal sealed class Converter : JsonConverter
        {
            /// <inheritdoc/>
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Optional<T>);
            }

            /// <inheritdoc/>
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (objectType != typeof(Optional<T>))
                    throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "Expected target type {0}, found {1}.", typeof(Optional<T>), objectType));

                T value = serializer.Deserialize<T>(reader);
                return new Optional<T>(value);
            }

            /// <inheritdoc/>
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                Optional<T> optionalValue = (Optional<T>)value;
                T wrappedValue;
                if (!optionalValue.TryGetValue(out wrappedValue))
                    return;

                serializer.Serialize(writer, wrappedValue);
            }
        }
    }
}
