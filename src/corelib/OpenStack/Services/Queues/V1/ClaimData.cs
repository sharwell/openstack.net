namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ClaimData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="TimeToLive"/> property.
        /// </summary>
        [JsonProperty("ttl", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _ttl;

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
        /// specified time-to-live and extension data.
        /// </summary>
        /// <param name="timeToLive">The time to live of the claim, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ClaimData(TimeSpan? timeToLive, params JProperty[] extensionData)
            : base(extensionData)
        {
            if (timeToLive.HasValue)
                _ttl = (long)timeToLive.Value.TotalSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimData"/> class with the
        /// specified time-to-live and extension data.
        /// </summary>
        /// <param name="timeToLive">The time to live of the claim, or <see langword="null"/> to not set the underlying property.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public ClaimData(TimeSpan? timeToLive, IDictionary<string, JToken> extensionData)
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
    }
}