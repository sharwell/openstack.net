using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace net.openstack.Providers.Rackspace.Objects.Request
{
    [DataContract]
    internal class SetPasswordRequest
    {
        [DataMember(Name = "passwordCredentials")]
        public PasswordCredential PasswordCredential { get; set; }
    }

    [DataContract]
    internal class PasswordCredential
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
