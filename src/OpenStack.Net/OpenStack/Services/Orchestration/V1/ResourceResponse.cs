namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an HTTP API response containing a <see cref="V1.Resource"/> object.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Resource"/> property.
        /// </summary>
        [JsonProperty("resource", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Resource _resource;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceResponse()
        {
        }

        /// <summary>
        /// Gets the <see cref="V1.Resource"/> object.
        /// </summary>
        /// <value>
        /// <para>The <see cref="V1.Resource"/> object</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public Resource Resource
        {
            get
            {
                return _resource;
            }
        }
    }
}
