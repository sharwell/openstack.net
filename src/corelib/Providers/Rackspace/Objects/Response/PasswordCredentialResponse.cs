using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using net.openstack.Providers.Rackspace.Objects.Request;

namespace net.openstack.Providers.Rackspace.Objects.Response
{
    [DataContract]
    internal class PasswordCredentialResponse
    {
        [DataMember(Name = "passwordCredentials")]
        public PasswordCredential PasswordCredential { get; set; }
    }
}
