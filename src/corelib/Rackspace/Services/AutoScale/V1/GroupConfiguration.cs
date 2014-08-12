namespace Rackspace.Services.AutoScale.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class represents the configuration for a scaling group in the <see cref="IAutoScaleService"/>.
    /// </summary>
    /// <remarks>
    /// The scaling group configuration specifies the basic elements of the Auto Scale configuration.
    /// It manages how many servers can participate in the scaling group. It also specifies information
    /// related to load balancers.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class GroupConfiguration : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Cooldown"/> property.
        /// </summary>
        [JsonProperty("cooldown", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _cooldown;

        /// <summary>
        /// This is the backing field for the <see cref="MinEntities"/> property.
        /// </summary>
        [JsonProperty("minEntities", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _minEntities;

        /// <summary>
        /// This is the backing field for the <see cref="MaxEntities"/> property.
        /// </summary>
        [JsonProperty("maxEntities", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _maxEntities;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JObject _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected GroupConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupConfiguration"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="name">The name of the scaling group.</param>
        /// <param name="cooldown">The cooldown period of the scaling group.</param>
        /// <param name="minEntities">The minimum number of servers to include in the scaling group.</param>
        /// <param name="maxEntities">The maximum number of servers to include in the scaling group.</param>
        /// <param name="metadata">The metadata to associate with the scaling group.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="name"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="maxEntities"/> is less than <paramref name="minEntities"/>.</para>
        /// </exception>
        public GroupConfiguration(string name, TimeSpan? cooldown, int? minEntities, int? maxEntities, JObject metadata)
        {
            Initialize(name, cooldown, minEntities, maxEntities, metadata);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupConfiguration"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="name">The name of the scaling group.</param>
        /// <param name="cooldown">The cooldown period of the scaling group.</param>
        /// <param name="minEntities">The minimum number of servers to include in the scaling group.</param>
        /// <param name="maxEntities">The maximum number of servers to include in the scaling group.</param>
        /// <param name="metadata">The metadata to associate with the scaling group.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="name"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="maxEntities"/> is less than <paramref name="minEntities"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="extensionData"/> contains any <see langword="null"/> values.</para>
        /// </exception>
        public GroupConfiguration(string name, TimeSpan? cooldown, int? minEntities, int? maxEntities, JObject metadata, params JProperty[] extensionData)
            : base(extensionData)
        {
            Initialize(name, cooldown, minEntities, maxEntities, metadata);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupConfiguration"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="name">The name of the scaling group.</param>
        /// <param name="cooldown">The cooldown period of the scaling group.</param>
        /// <param name="minEntities">The minimum number of servers to include in the scaling group.</param>
        /// <param name="maxEntities">The maximum number of servers to include in the scaling group.</param>
        /// <param name="metadata">The metadata to associate with the scaling group.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="name"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="maxEntities"/> is less than <paramref name="minEntities"/>.</para>
        /// </exception>
        public GroupConfiguration(string name, TimeSpan? cooldown, int? minEntities, int? maxEntities, JObject metadata, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            Initialize(name, cooldown, minEntities, maxEntities, metadata);
        }

        /// <summary>
        /// Gets the name of the scaling group.
        /// </summary>
        /// <value>
        /// The name of the scaling group.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the cooldown time of the scaling group, which specifies the time the group must wait
        /// after a scaling policy is triggered before another request for scaling is accepted.
        /// </summary>
        /// <remarks>
        /// Applies mainly to event-based policies.
        /// </remarks>
        /// <value>
        /// The cooldown time of the scaling group.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public TimeSpan? Cooldown
        {
            get
            {
                if (_cooldown == null)
                    return null;

                return TimeSpan.FromSeconds(_cooldown.Value);
            }
        }

        /// <summary>
        /// Gets the minimum amount of entities that are allowed in this group. You cannot scale down
        /// below this value. Increasing this value can cause an immediate addition to the scaling
        /// group.
        /// </summary>
        /// <value>
        /// The minimum amount of entities that are allowed in this group.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public long? MinEntities
        {
            get
            {
                return _minEntities;
            }
        }

        /// <summary>
        /// Gets the maximum amount of entities that are allowed in this group. You cannot scale up
        /// above this value. Decreasing this value can cause an immediate reduction of the scaling
        /// group.
        /// </summary>
        /// <value>
        /// The maximum amount of entities that are allowed in this group.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public long? MaxEntities
        {
            get
            {
                return _maxEntities;
            }
        }

        /// <summary>
        /// Gets the metadata associated with the scaling group resource.
        /// </summary>
        /// <value>
        /// The metadata associated with the scaling group resource.
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupConfiguration"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="name">The name of the scaling group.</param>
        /// <param name="cooldown">The cooldown period of the scaling group.</param>
        /// <param name="minEntities">The minimum number of servers to include in the scaling group.</param>
        /// <param name="maxEntities">The maximum number of servers to include in the scaling group.</param>
        /// <param name="metadata">The metadata to associate with the scaling group.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="name"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="maxEntities"/> is less than <paramref name="minEntities"/>.</para>
        /// </exception>
        private void Initialize(string name, TimeSpan? cooldown, int? minEntities, int? maxEntities, JObject metadata)
        {
            if (name != null && string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");
            if (maxEntities < minEntities)
                throw new ArgumentException("maxEntities cannot be less than minEntities");

            _name = name;
            if (cooldown.HasValue)
                _cooldown = (int)cooldown.Value.TotalSeconds;
            _minEntities = minEntities;
            _maxEntities = maxEntities;
            _metadata = metadata;
        }
    }
}
