namespace OpenStack.Services.Queues.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents a queue message with a strongly-typed body.
    /// </summary>
    /// <typeparam name="T">The type of the data stored in the message body.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Message<T> : ExtensibleJsonObject
    {
        /// <summary>
        /// The backing field for the <see cref="TimeToLive"/> property.
        /// This value is stored in seconds.
        /// </summary>
        [JsonProperty("ttl", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _timeToLive;

        /// <summary>
        /// The backing field for the <see cref="Body"/> property.
        /// </summary>
        [JsonProperty("body", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private T _body;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message{T}"/> class during
        /// JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Message()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message{T}"/> class using
        /// the specified time-to-live and strongly-typed message body.
        /// </summary>
        /// <param name="timeToLive">The time-to-live for the message.</param>
        /// <param name="body">The message data.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="timeToLive"/> is negative or <see cref="TimeSpan.Zero"/>.</exception>
        public Message(TimeSpan timeToLive, T body)
        {
            if (timeToLive <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("timeToLive");

            _timeToLive = (long)timeToLive.TotalSeconds;
            _body = body;
        }

        /// <summary>
        /// Gets the time-to-live for the message.
        /// </summary>
        /// <value>
        /// The time-to-live for the message.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public TimeSpan? TimeToLive
        {
            get
            {
                if (_timeToLive == null)
                    return null;

                return TimeSpan.FromSeconds(_timeToLive.Value);
            }
        }

        /// <summary>
        /// Gets the body of the message.
        /// </summary>
        /// <value>
        /// The body of the message.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public T Body
        {
            get
            {
                return _body;
            }
        }
    }
}
