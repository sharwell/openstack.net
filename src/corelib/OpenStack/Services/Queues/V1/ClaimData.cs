namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation for the basic configurable information in
    /// a Claim resource in the <see cref="IQueuesService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ClaimData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="TimeToLive"/> property.
        /// </summary>
        [JsonProperty("ttl", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _ttl;

        /// <summary>
        /// This is the backing field for the <see cref="GracePeriod"/> property.
        /// </summary>
        [JsonProperty("grace", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _grace;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ClaimData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class with the
        /// specified time-to-live.
        /// </summary>
        /// <param name="timeToLive">The time to live of the claim.</param>
        public ClaimData(TimeSpan timeToLive)
        {
            _ttl = (long)timeToLive.TotalSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class with the
        /// specified time-to-live and message grace period.
        /// </summary>
        /// <param name="timeToLive">The time to live of the claim.</param>
        /// <param name="gracePeriod">The message grace period. Claimed messages are prevented from expiring until the claim's time-to-live has lapsed followed by this additional grace period.</param>
        public ClaimData(TimeSpan timeToLive, TimeSpan gracePeriod)
        {
            _ttl = (long)timeToLive.TotalSeconds;
            _grace = (long)gracePeriod.TotalSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class with the
        /// specified time-to-live, message grace period, and extension data.
        /// </summary>
        /// <param name="timeToLive">The time to live of the claim, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="gracePeriod">The message grace period. Claimed messages are prevented from expiring until the claim's time-to-live has lapsed followed by this additional grace period.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ClaimData(TimeSpan? timeToLive, TimeSpan? gracePeriod, params JProperty[] extensionData)
            : base(extensionData)
        {
            if (timeToLive.HasValue)
                _ttl = (long)timeToLive.Value.TotalSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class with the
        /// specified time-to-live, message grace period, and extension data.
        /// </summary>
        /// <param name="timeToLive">The time to live of the claim, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="gracePeriod">The message grace period. Claimed messages are prevented from expiring until the claim's time-to-live has lapsed followed by this additional grace period.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public ClaimData(TimeSpan? timeToLive, TimeSpan? gracePeriod, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            if (timeToLive.HasValue)
                _ttl = (long)timeToLive.Value.TotalSeconds;
        }

        /// <summary>
        /// Gets the Time To Live (TTL) of the claim.
        /// </summary>
        /// <value>
        /// The Time To Live (TTL) of the claim.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public TimeSpan? TimeToLive
        {
            get
            {
                if (_ttl == null)
                    return null;

                return TimeSpan.FromSeconds(_ttl.Value);
            }
        }

        /// <summary>
        /// Gets the message grace period of the claim.
        /// </summary>
        /// <remarks>
        /// Claimed messages are prevented from expiring until the claim's time-to-live has lapsed followed by this additional grace period.
        /// </remarks>
        /// <value>
        /// The message grace period.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public TimeSpan? GracePeriod
        {
            get
            {
                if (_grace == null)
                    return null;

                return TimeSpan.FromSeconds(_grace.Value);
            }
        }
    }
}