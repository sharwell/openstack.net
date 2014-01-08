namespace net.openstack.Providers.Rackspace.Objects.Hadoop
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResizeClusterConfiguration
    {
        [JsonProperty("resize", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResizeClusterProperties _resize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeClusterConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResizeClusterConfiguration()
        {
        }

        public ResizeClusterConfiguration(int nodeCount)
        {
            if (nodeCount < 0)
                throw new ArgumentOutOfRangeException("nodeCount");

            _resize = new ResizeClusterProperties(nodeCount);
        }

        [JsonObject(MemberSerialization.OptIn)]
        protected class ResizeClusterProperties
        {
            [JsonProperty("nodeCount", DefaultValueHandling = DefaultValueHandling.Ignore)]
            private int? _nodeCount;

            /// <summary>
            /// Initializes a new instance of the <see cref="ResizeClusterProperties"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected ResizeClusterProperties()
            {
            }

            public ResizeClusterProperties(int nodeCount)
            {
                if (nodeCount < 0)
                    throw new ArgumentOutOfRangeException("nodeCount");

                _nodeCount = nodeCount;
            }
        }
    }
}
