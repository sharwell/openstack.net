namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class GroupConfiguration
    {
        [JsonProperty("name")]
        private string _name;

        [JsonProperty("cooldown")]
        private long? _cooldown;

        [JsonProperty("minEntities")]
        private long? _minEntities;

        [JsonProperty("maxEntities", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _maxEntities;

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

        public GroupConfiguration(string name, TimeSpan cooldown, int minEntities, int? maxEntities, JObject metadata)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (cooldown < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("cooldown");
            if (minEntities < 0)
                throw new ArgumentOutOfRangeException("minEntities");
            if (maxEntities < 0)
                throw new ArgumentOutOfRangeException("maxEntities");
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            _name = name;
            _cooldown = (int)cooldown.TotalSeconds;
            _minEntities = minEntities;
            _maxEntities = maxEntities;
            _metadata = metadata;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public TimeSpan? Cooldown
        {
            get
            {
                if (_cooldown == null)
                    return null;

                return TimeSpan.FromSeconds(_cooldown.Value);
            }
        }

        public long? MinEntities
        {
            get
            {
                return _minEntities;
            }
        }

        public long? MaxEntities
        {
            get
            {
                return _maxEntities;
            }
        }

        public JObject Metadata
        {
            get
            {
                return _metadata;
            }
        }
    }
}
