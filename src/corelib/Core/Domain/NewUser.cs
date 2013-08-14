using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class NewUser
    {
        [DataMember(Name = "OS-KSADM:password")]
        public string Password { get; internal set; }

        [DataMember(Name = "id", EmitDefaultValue = true)]
        public string Id { get; private set; }

        [DataMember(Name = "username")]
        public string Username { get; private set; }

        [DataMember(Name = "email")]
        public string Email { get; private set; }

        [DataMember(Name = "enabled")]
        public bool Enabled { get; private set; }

        public NewUser(string username, string email, string password = null, bool enabled = true)
        {
            Username = username;
            Email = email;
            Password = password;
            Enabled = enabled;
        }
    }
}
