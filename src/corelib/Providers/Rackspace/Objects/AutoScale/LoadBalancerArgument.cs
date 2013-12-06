namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using Newtonsoft.Json;
    using LoadBalancerId = net.openstack.Providers.Rackspace.Objects.LoadBalancers.LoadBalancerId;

    [JsonObject(MemberSerialization.OptIn)]
    public class LoadBalancerArgument
    {
        [JsonProperty("loadBalancerId")]
        private LoadBalancerId _loadBalancerId;

        [JsonProperty("port")]
        private int? _port;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBalancerArgument"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected LoadBalancerArgument()
        {
        }

        public LoadBalancerArgument(LoadBalancerId loadBalancerId, int port)
        {
            if (loadBalancerId == null)
                throw new ArgumentNullException("loadBalancerId");
            if (port <= 0 || port > 65535)
                throw new ArgumentOutOfRangeException("port");

            _loadBalancerId = loadBalancerId;
            _port = port;
        }

        public LoadBalancerId LoadBalancerId
        {
            get
            {
                return _loadBalancerId;
            }
        }

        public int? Port
        {
            get
            {
                return _port;
            }
        }
    }
}
