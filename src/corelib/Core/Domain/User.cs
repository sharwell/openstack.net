using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "RAX-AUTH:defaultRegion")]
        public string DefaultRegion { get; private set; }

        [DataMember(Name="id", EmitDefaultValue = true)]
        public string Id { get; private set; }

        [DataMember(Name="username")]
        public string Username { get; private set; }

        [DataMember(Name="email")]
        public string Email { get; private set; }

        [DataMember(Name = "enabled")]
        public bool Enabled { get; private set; }
    }
}