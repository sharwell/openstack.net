namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an HTTP API response containing an <see cref="V1.Event"/> object.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class EventResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Event"/> property.
        /// </summary>
        [JsonProperty("event", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Event _event;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="EventResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected EventResponse()
        {
        }

        /// <summary>
        /// Gets the <see cref="V1.Event"/> object.
        /// </summary>
        /// <value>
        /// <para>The <see cref="V1.Event"/> object</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public Event Event
        {
            get
            {
                return _event;
            }
        }
    }
}
