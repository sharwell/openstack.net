namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the unique identifier of a <see cref="SoftwareConfiguration"/> resource in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="SoftwareConfiguration.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(SoftwareConfigurationId.Converter))]
    public sealed class SoftwareConfigurationId : ResourceIdentifier<SoftwareConfigurationId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareConfigurationId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public SoftwareConfigurationId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="SoftwareConfigurationId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override SoftwareConfigurationId FromValue(string id)
            {
                return new SoftwareConfigurationId(id);
            }
        }
    }
}
