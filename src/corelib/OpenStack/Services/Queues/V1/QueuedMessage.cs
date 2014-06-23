namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Compat;

    /// <summary>
    /// Represents a message which is queued in the <see cref="IQueuesService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class QueuedMessage : Message<JObject>
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// The backing field for the <see cref="Href"/> property.
        /// </summary>
        [JsonProperty("href", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _href;

        /// <summary>
        /// The backing field for the <see cref="Age"/> property.
        /// The value is stored in seconds.
        /// </summary>
        [JsonProperty("age", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _age;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="QueuedMessage"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QueuedMessage()
        {
        }

        /// <summary>
        /// Gets the ID of the message.
        /// </summary>
        public MessageId Id
        {
            get
            {
                Uri href = Href;
                if (href == null)
                    return null;

                // make sure we have an absolute URI, or Segments will throw an InvalidOperationException
                if (!href.IsAbsoluteUri)
                    href = new Uri(new Uri("http://example.com"), href);

                string[] segments = href.GetSegments();
                if (segments.Length == 0)
                    return null;

                return new MessageId(segments.Last());
            }
        }

        /// <summary>
        /// Gets the URI of the message resource.
        /// </summary>
        public Uri Href
        {
            get
            {
                if (_href == null)
                    return null;

                return new Uri(_href, UriKind.RelativeOrAbsolute);
            }
        }

        /// <summary>
        /// Gets the age of the message.
        /// </summary>
        public TimeSpan? Age
        {
            get
            {
                if (_age == null)
                    return null;

                return TimeSpan.FromSeconds(_age.Value);
            }
        }
    }
}
