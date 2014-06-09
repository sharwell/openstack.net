namespace OpenStack.Services.Compute.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation used for the result of the API calls
    /// which return an <seealso cref="V2.Extension"/> resource.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ExtensionResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Extension"/> property.
        /// </summary>
        [JsonProperty("extension", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Extension _extension;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExtensionResponse()
        {
        }

        /// <summary>
        /// Gets an <see cref="V2.Extension"/> object describing the API extension.
        /// </summary>
        /// <value>
        /// An <see cref="V2.Extension"/> object describing the API extension.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public Extension Extension
        {
            get
            {
                return _extension;
            }
        }
    }
}
