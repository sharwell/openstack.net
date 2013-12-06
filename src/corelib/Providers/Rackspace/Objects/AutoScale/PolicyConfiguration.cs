namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class PolicyConfiguration
    {
        [JsonProperty("name")]
        private string _name;

        [JsonProperty("desiredCapacity", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _desiredCapacity;

        [JsonProperty("cooldown")]
        private long? _cooldown;

        [JsonProperty("change", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _change;

        [JsonProperty("changePercent", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private double? _changePercent;

        [JsonProperty("args", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private object _args;

        [JsonProperty("type")]
        private PolicyType _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PolicyConfiguration()
        {
        }

        protected PolicyConfiguration(string name, PolicyType type, long? desiredCapacity, TimeSpan? cooldown, long? change, double? changePercent, object arguments)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (type == null)
                throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");
            if (desiredCapacity < 0)
                throw new ArgumentOutOfRangeException("desiredCapacity");
            if (cooldown < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("cooldown");

            _name = name;
            _type = type;
            _desiredCapacity = desiredCapacity;
            _cooldown = cooldown != null ? (long?)cooldown.Value.TotalSeconds : null;
            _change = change;
            _changePercent = changePercent;
            _args = arguments;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public long? DesiredCapacity
        {
            get
            {
                return _desiredCapacity;
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

        public long? Change
        {
            get
            {
                return _change;
            }
        }

        public double? ChangePercent
        {
            get
            {
                return _changePercent;
            }
        }

        public object Arguments
        {
            get
            {
                return _args;
            }
        }

        public PolicyType PolicyType
        {
            get
            {
                return _type;
            }
        }

        public static PolicyConfiguration Capacity(string name, int desiredCapacity, TimeSpan cooldown)
        {
            return new PolicyConfiguration(name, PolicyType.Webhook, desiredCapacity, cooldown, null, null, null);
        }

        public static PolicyConfiguration IncrementalChange(string name, int change, TimeSpan cooldown)
        {
            if (change == 0)
                throw new ArgumentException("change cannot be 0", "change");

            return new PolicyConfiguration(name, PolicyType.Webhook, null, cooldown, change, null, null);
        }

        public static PolicyConfiguration PercentageChange(string name, double changePercentage, TimeSpan cooldown)
        {
            return new PolicyConfiguration(name, PolicyType.Webhook, null, cooldown, null, changePercentage, null);
        }

        public static PolicyConfiguration PercentageChangeAtTime(string name, double changePercentage, TimeSpan cooldown, DateTimeOffset time)
        {
            const string timeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff'Z'";
            string serializedTime = time.ToUniversalTime().ToString(timeFormat);

            JObject arguments = new JObject(
                new JProperty("at", JValue.CreateString(serializedTime)));

            return new PolicyConfiguration(name, PolicyType.Schedule, null, cooldown, null, changePercentage, arguments);
        }

        internal static PolicyConfiguration PercentageChangeAtTime(string name, double changePercentage, TimeSpan cooldown, string time)
        {
            JObject arguments = new JObject(
                new JProperty("at", JValue.CreateString(time)));

            return new PolicyConfiguration(name, PolicyType.Schedule, null, cooldown, null, changePercentage, arguments);
        }
    }
}
