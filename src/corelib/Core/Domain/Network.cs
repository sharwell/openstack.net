using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class Network
    {
        [DataMember]
        public string Id { get; private set; }

        [DataMember(Name = "ip")]
        public AddressDetails[] Addresses { get; private set; }

        public Network(string id, AddressDetails[] addresses)
        {
            Id = id;
            Addresses = addresses;
        }
    }
}
