namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using net.openstack.Core;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json.Linq;

    public class ReadOnlyCollectionPage<T, TMarker> : ReadOnlyCollection<T>
        where TMarker : ResourceIdentifier<TMarker>
    {
        private IDictionary<string, object> _metadata;

        public ReadOnlyCollectionPage(IList<T> list, IDictionary<string, object> metadata)
            : base(list)
        {
            _metadata = metadata ?? new Dictionary<string, object>();
        }

        public ReadOnlyDictionary<string, object> Metadata
        {
            get
            {
                return new ReadOnlyDictionary<string, object>(_metadata);
            }
        }

        public TMarker Marker
        {
            get
            {
                object marker;
                if (!_metadata.TryGetValue("marker", out marker) || marker == null)
                    return null;

                JToken token = JToken.FromObject(marker);
                return token.ToObject<TMarker>();
            }
        }
    }
}
