using System.Runtime.Serialization;
using net.openstack.Core.Providers;

namespace net.openstack.Core.Domain
{
    /// <summary>
    /// Represents the type of a volume in the Block Storage service.
    /// </summary>
    /// <seealso cref="IBlockStorageProvider.ListVolumeTypes"/>
    /// <seealso cref="IBlockStorageProvider.DescribeVolumeType"/>
    [DataContract]
    public class VolumeType
    {
        /// <summary>
        /// Gets the volume type ID.
        /// </summary>
        [DataMember]
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the volume type.
        /// </summary>
        [DataMember]
        public string Name { get; private set; }
    }
}
