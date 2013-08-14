using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class VirtualInterface
    {
        [DataMember(Name = "id")]
        public string Id { get; private set; }

        [DataMember(Name = "ip_addresses")]
        public VirtualInterfaceAddress[] Addresses { get; private set; }

        [DataMember(Name = "mac_address")]
        public string MACAddress { get; private set; }
    }
}
