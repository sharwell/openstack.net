namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class Claim : ClaimData
    {
        /// <summary>
        /// The backing field for the <see cref="Age"/> property.
        /// </summary>
        [JsonProperty("age", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _age;

        /// <summary>
        /// The backing field for the <see cref="Messages"/> property.
        /// </summary>
        [JsonProperty("messages", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QueuedMessage[] _messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="Claim"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Claim()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class with the
        /// specified messages, and an initial age of 0.
        /// </summary>
        /// <param name="messages">A collection of claimed messages, or <see langword="null"/> to not set the underlying property.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="messages"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="messages"/> contains any <see langword="null"/> values.</exception>
        public Claim(IEnumerable<QueuedMessage> messages)
            : this(null, null, TimeSpan.Zero, messages)
        {
            if (messages == null)
                throw new ArgumentNullException("messages");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class with the
        /// specified time-to-live, message grace period, age, messages, and extension data.
        /// </summary>
        /// <param name="timeToLive">The time to live of the claim, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="gracePeriod">The message grace period. Claimed messages are prevented from expiring until the claim's time-to-live has lapsed followed by this additional grace period.</param>
        /// <param name="age">The age of the claim, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="messages">A collection of claimed messages, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public Claim(TimeSpan? timeToLive, TimeSpan? gracePeriod, TimeSpan? age, IEnumerable<QueuedMessage> messages, params JProperty[] extensionData)
            : base(timeToLive, gracePeriod, extensionData)
        {
            if (age.HasValue)
                _age = (long)age.Value.TotalSeconds;
            if (messages != null)
                _messages = messages.ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class with the
        /// specified time-to-live, message grace period, age, messages, and extension data.
        /// </summary>
        /// <param name="timeToLive">The time to live of the claim, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="gracePeriod">The message grace period. Claimed messages are prevented from expiring until the claim's time-to-live has lapsed followed by this additional grace period.</param>
        /// <param name="age">The age of the claim, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="messages">A collection of claimed messages, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public Claim(TimeSpan? timeToLive, TimeSpan? gracePeriod, TimeSpan? age, IEnumerable<QueuedMessage> messages, IDictionary<string, JToken> extensionData)
            : base(timeToLive, gracePeriod, extensionData)
        {
            if (age.HasValue)
                _age = (long)age.Value.TotalSeconds;
            if (messages != null)
                _messages = messages.ToArray();
        }

        /// <summary>
        /// Gets the age of the claim as returned by the server.
        /// </summary>
        /// <remarks>
        /// This value does not automatically update. To obtain the age of a claim after a period of time elapses,
        /// use <see cref="IQueuesService.QueryClaimAsync"/>.
        /// </remarks>
        /// <value>
        /// The age of the claim.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public TimeSpan? Age
        {
            get
            {
                if (_age == null)
                    return null;

                return TimeSpan.FromSeconds(_age.Value);
            }
        }

        /// <summary>
        /// Gets the messages which are included in this claim.
        /// </summary>
        /// <value>
        /// A collection of <see cref="QueuedMessage"/> objects describing the claimed messages.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ReadOnlyCollection<QueuedMessage> Messages
        {
            get
            {
                if (_messages == null)
                    return null;

                return new ReadOnlyCollection<QueuedMessage>(_messages);
            }
        }
    }
}
