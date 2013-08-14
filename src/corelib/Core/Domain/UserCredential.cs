using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class UserCredential
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember(Name = "username")]
        public string Username { get; private set; }

        [DataMember(Name = "apiKey")]
        public string APIKey { get; private set; }

        public UserCredential(string name, string username, string apiKey)
        {
            Name = name;
            Username = username;
            APIKey = apiKey;
        }
    }
}
