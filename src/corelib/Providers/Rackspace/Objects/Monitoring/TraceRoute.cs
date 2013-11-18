namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class TraceRoute
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("result")]
        private TraceRouteHop[] _hops;
#pragma warning restore 649

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
