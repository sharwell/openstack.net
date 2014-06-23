namespace OpenStack.Services.Queues.V1
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This models the JSON object used to represent statistics for a particular
    /// message in a message queue.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class MessageStatistics
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// The backing field for the <see cref="Href"/> property.
        /// </summary>
        [JsonProperty("href", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _href;

        /// <summary>
        /// The backing field for the <see cref="Age"/> property.
        /// </summary>
        [JsonProperty("age", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _age;

        /// <summary>
        /// The backing field for the <see cref="Created"/> property.
        /// </summary>
        [JsonProperty("created", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _created;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageStatistics"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MessageStatistics()
        {
        }

        /// <summary>
        /// Gets the absolute path portion of the URI to this message resource.
        /// </summary>
        /// <value>
        /// The absolute path portion of the URI to this message resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public Uri Href
        {
            get
            {
                if (_href == null)
                    return null;

                return new Uri(_href, UriKind.Relative);
            }
        }

        /// <summary>
        /// Gets the age of the message in the queue.
        /// </summary>
        /// <value>
        /// The age of the message in the queue.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public TimeSpan? Age
        {
            get
            {
                if (!_age.HasValue)
                    return null;

                return TimeSpan.FromSeconds(_age.Value);
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when this message was first added to the queue.
        /// </summary>
        /// <value>
        /// A timestamp indicating when this message was first added to the queue.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public DateTimeOffset? Created
        {
            get
            {
                return _created;
            }
        }
    }
}
