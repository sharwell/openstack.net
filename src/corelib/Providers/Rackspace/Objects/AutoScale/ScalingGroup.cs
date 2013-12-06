namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System.Collections.ObjectModel;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ScalingGroup : ScalingGroupConfiguration<Policy>
    {
        [JsonProperty("id")]
        private ScalingGroupId _id;

        [JsonProperty("links")]
        private Link[] _links;

        [JsonProperty("state")]
        private GroupState _state;

        public ScalingGroupId Id
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

        public GroupState State
        {
            get
            {
                return _state;
            }
        }
    }
}
