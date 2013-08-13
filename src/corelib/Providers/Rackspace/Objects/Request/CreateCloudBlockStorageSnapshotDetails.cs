using System.Runtime.Serialization;

namespace net.openstack.Providers.Rackspace.Objects.Request
{
    [DataContract]
    internal class CreateCloudBlockStorageSnapshotDetails
    {
        [DataMember(Name = "volume_id")]
        public string VolumeId { get; set; }

        [DataMember]
        public bool Force { get; set; }

        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }
        
        [DataMember(Name = "display_description")]
        public string DisplayDescription { get; set; }
    }
}
