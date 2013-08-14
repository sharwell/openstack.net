using System;
using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    [DataContract]
    public class Snapshot
    {
        [DataMember(Name = "status")]
        private string _status;

        [DataMember]
        public string Id { get; private set; }

        [DataMember(Name = "display_name")]
        public string DisplayName { get; private set; }

        [DataMember(Name = "display_description")]
        public string DisplayDescription { get; private set; }

        [DataMember(Name = "volume_id")]
        public string VolumeId { get; private set; }

        public SnapshotState Status
        {
            get
            {
                if (string.IsNullOrEmpty(_status))
                    return null;

                return SnapshotState.FromName(_status);
            }
        }

        [DataMember]
        public string Size { get; private set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; private set; }
    }
}
