namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class TraceRouteConfiguration
    {
        [JsonProperty("target")]
        private string _target;

        [JsonProperty("target_resolver")]
        private TargetResolverType _targetResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceRouteConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TraceRouteConfiguration()
        {
        }

        public TraceRouteConfiguration(IPAddress target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target.AddressFamily != AddressFamily.InterNetwork && target.AddressFamily != AddressFamily.InterNetworkV6)
                throw new NotSupportedException("The family of the target address is not supported.");

            _target = target.ToString();
        }

        public TraceRouteConfiguration(string target, TargetResolverType resolverType)
        {
            _target = target;
            _targetResolver = resolverType;
        }

        public string Target
        {
            get
            {
                return _target;
            }
        }

        public TargetResolverType ResolverType
        {
            get
            {
                return _targetResolver;
            }
        }
    }
}
