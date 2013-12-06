namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System.Collections.ObjectModel;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Policy : PolicyConfiguration
    {
        [JsonProperty("id")]
        private PolicyId _id;

        [JsonProperty("links")]
        private Link[] _links;

        public PolicyId Id
        {
            get
            {
                return _id;
            }
        }

        public ReadOnlyCollection<Link> Links
        {
            get
            {
                if (_links == null)
                    return null;

                return new ReadOnlyCollection<Link>(_links);
            }
        }
    }
}
