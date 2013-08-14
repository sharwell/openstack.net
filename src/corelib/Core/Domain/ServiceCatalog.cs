namespace net.openstack.Core.Domain
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ServiceCatalog
    {
        [DataMember(Name = "endpoints")]
        public Endpoint[] Endpoints { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "type")]
        public string Type { get; private set; }
    }
}