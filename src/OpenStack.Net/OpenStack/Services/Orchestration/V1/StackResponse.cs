namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an HTTP API response containing a <see cref="V1.Stack"/> object.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class StackResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Stack"/> property.
        /// </summary>
        [JsonProperty("stack", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Stack _stack;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="StackResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected StackResponse()
        {
        }

        /// <summary>
        /// Gets the <see cref="V1.Stack"/> object.
        /// </summary>
        /// <value>
        /// <para>The <see cref="V1.Stack"/> object</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public Stack Stack
        {
            get
            {
                return _stack;
            }
        }
    }
}
