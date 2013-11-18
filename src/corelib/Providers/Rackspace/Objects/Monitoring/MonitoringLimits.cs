namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class MonitoringLimits
    {
        [JsonProperty("resource")]
        private IDictionary<string, int> _resourceLimits;

        [JsonProperty("rate")]
        private IDictionary<string, RateLimit> _rateLimits;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringLimits"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MonitoringLimits()
        {
        }

        public ReadOnlyDictionary<string, int> ResourceLimits
        {
            get
            {
                if (_resourceLimits == null)
                    return null;

                return new ReadOnlyDictionary<string, int>(_resourceLimits);
            }
        }

        public ReadOnlyDictionary<string, RateLimit> RateLimits
        {
            get
            {
                if (_rateLimits == null)
                    return null;

                return new ReadOnlyDictionary<string, RateLimit>(_rateLimits);
            }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class RateLimit
        {
            [JsonProperty("limit")]
            private int? _limit;

            [JsonProperty("used")]
            private int? _used;

            [JsonProperty("window")]
            private string _window;

            /// <summary>
            /// Initializes a new instance of the <see cref="RateLimit"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected RateLimit()
            {
            }

            public int? Limit
            {
                get
                {
                    return _limit;
                }
            }

            public int? Used
            {
                get
                {
                    return _used;
                }
            }

            public string Window
            {
                get
                {
                    return _window;
                }
            }
        }
    }
}
