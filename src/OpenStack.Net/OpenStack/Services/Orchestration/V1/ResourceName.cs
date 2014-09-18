namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the name of a <see cref="Resource"/> resource in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="Resource.Name"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ResourceName.Converter))]
    public sealed class ResourceName : ResourceIdentifier<ResourceName>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceName"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public ResourceName(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ResourceName"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ResourceName FromValue(string id)
            {
                return new ResourceName(id);
            }
        }
    }
}
