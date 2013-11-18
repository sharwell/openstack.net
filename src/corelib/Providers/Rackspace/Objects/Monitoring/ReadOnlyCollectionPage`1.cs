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

        /// <summary>
        /// Gets the marker for the current page.
        /// </summary>
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

        /// <summary>
        /// Gets the marker for the next page.
        /// </summary>
        public TMarker NextMarker
        {
            get
            {
                object marker;
                if (!_metadata.TryGetValue("next_marker", out marker) || marker == null)
                    return null;

                JToken token = JToken.FromObject(marker);
                return token.ToObject<TMarker>();
            }
        }
    }
}
