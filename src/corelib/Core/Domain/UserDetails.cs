using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public string Id { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public Role[] Roles { get; private set; }

        [DataMember(Name = "RAX-AUTH:defaultRegion")]
        public string DefaultRegion { get; private set; }
    }
}
