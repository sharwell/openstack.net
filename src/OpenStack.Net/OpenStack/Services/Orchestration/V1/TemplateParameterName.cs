namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the name of a <see cref="TemplateParameter"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="StackTemplate.Parameters"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(TemplateParameterName.Converter))]
    public sealed class TemplateParameterName : ResourceIdentifier<TemplateParameterName>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateParameterName"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public TemplateParameterName(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Defines an explicit conversion of a string to a <see cref="TemplateParameterName"/> resource identifier.
        /// </summary>
        /// <remarks>
        /// <para>This method supports the of <see cref="TemplateParameterName"/> as dictionary keys for Json.NET.
        /// While other conversion mechanisms exist, the only simple conversion mechanism supported by all target
        /// versions of the .NET framework is the cast operators.</para>
        /// </remarks>
        /// <param name="value">The string value.</param>
        /// <returns>
        /// <para>A <see cref="TemplateParameterName"/> for the specified string <paramref name="value"/>.</para>
        /// <para>-or-</para>
        /// <para><see langword="null"/>, if <paramref name="value"/> is <see langword="null"/> or empty.</para>
        /// </returns>
        public static explicit operator TemplateParameterName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new TemplateParameterName(value);
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="TemplateParameterName"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override TemplateParameterName FromValue(string id)
            {
                return new TemplateParameterName(id);
            }
        }
    }
}
