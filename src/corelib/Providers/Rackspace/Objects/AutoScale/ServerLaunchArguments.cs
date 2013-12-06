namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ServerLaunchArguments
    {
        [JsonProperty("loadBalancers", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private LoadBalancerArgument[] _loadBalancers;

        [JsonProperty("server", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ServerArgument _server;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerLaunchArguments"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ServerLaunchArguments()
        {
        }

        public ServerLaunchArguments(ServerArgument server)
            : this(server, null)
        {
        }

        public ServerLaunchArguments(ServerArgument server, IEnumerable<LoadBalancerArgument> loadBalancers)
        {
            if (server == null)
                throw new ArgumentNullException("server");

            _server = server;
            if (loadBalancers != null)
            {
                _loadBalancers = loadBalancers.ToArray();
                if (_loadBalancers.Contains(null))
                    throw new ArgumentException("loadBalancers cannot contain any null values", "loadBalancers");
            }
        }

        public ServerArgument Server
        {
            get
            {
                return _server;
            }
        }

        public ReadOnlyCollection<LoadBalancerArgument> LoadBalancers
        {
            get
            {
                if (_loadBalancers == null)
                    return null;

                return new ReadOnlyCollection<LoadBalancerArgument>(_loadBalancers);
            }
        }
    }
}
