namespace OpenStack.Services.Queues.V1
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents a named queue in the <see cref="IQueuesService"/>.
    /// </summary>
    /// <seealso cref="IQueuesService"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Queue : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// The backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QueueName _name;

        /// <summary>
        /// The backing field for the <see cref="Href"/> property.
        /// </summary>
        [JsonProperty("href", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Uri _href;

        /// <summary>
        /// The backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JObject _metadata;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Queue()
        {
        }

        /// <summary>
        /// Gets the name of the queue.
        /// </summary>
        /// <value>
        /// 
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public QueueName Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the URI of the queue resource.
        /// </summary>
        /// <value>
        /// The URI of the queue resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public Uri Href
        {
            get
            {
                return _href;
            }
        }

        /// <summary>
        /// Gets a dynamic object containing the metadata associated with the queue.
        /// </summary>
        /// <value>
        /// A dynamic <see cref="JObject"/> containing the metadata associated with the queue.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public JObject Metadata
        {
            get
            {
                return _metadata;
            }
        }
    }
}
