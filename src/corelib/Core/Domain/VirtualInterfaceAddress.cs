using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class VirtualInterfaceAddress
    {
        [DataMember(Name = "address")]
        public string Address { get; private set; }

        [DataMember(Name = "network_id")]
        public string NetworkId { get; private set; }

        [DataMember(Name = "network_label")]
        public string NetworkLabel { get; private set; }
    }
}