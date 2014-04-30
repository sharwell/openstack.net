﻿namespace OpenStack.Services.Queues.V1
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
    public class QueuedMessage
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// The backing field for the <see cref="Href"/> property.
        /// </summary>
        [JsonProperty("href")]
        private Uri _href;

        /// <summary>
        /// The backing field for the <see cref="TimeToLive"/> property.
        /// The value is stored in seconds.
        /// </summary>
        [JsonProperty("ttl")]
        private long _ttl;

        /// <summary>
        /// The backing field for the <see cref="Age"/> property.
        /// The value is stored in seconds.
        /// </summary>
        [JsonProperty("age")]
        private long _age;

        /// <summary>
        /// The backing field for the <see cref="Body"/> property.
        /// </summary>
        [JsonProperty("body")]
        private JObject _body;
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
                if (Href == null)
                    return null;

                Uri href = Href;
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
                return _href;
            }
        }

        /// <summary>
        /// Gets the time-to-live of the message.
        /// </summary>
        public TimeSpan TimeToLive
        {
            get
            {
                return TimeSpan.FromSeconds(_ttl);
            }
        }

        /// <summary>
        /// Gets the age of the message.
        /// </summary>
        public TimeSpan Age
        {
            get
            {
                return TimeSpan.FromSeconds(_age);
            }
        }

        /// <summary>
        /// Gets the JSON body of the message as a dynamic <see cref="JObject"/>.
        /// </summary>
        public JObject Body
        {
            get
            {
                return _body;
            }
        }
    }
}