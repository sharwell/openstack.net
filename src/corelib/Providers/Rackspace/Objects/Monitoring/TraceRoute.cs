namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class TraceRoute
    {
        [JsonProperty("result")]
        private TraceRouteHop[] _hops;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceRoute"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TraceRoute()
        {
        }

        public ReadOnlyCollection<TraceRouteHop> Hops
        {
            get
            {
                if (_hops == null)
                    return null;

                return new ReadOnlyCollection<TraceRouteHop>(_hops);
            }
        }
    }
}
