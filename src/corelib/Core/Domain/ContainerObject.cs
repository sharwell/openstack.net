using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace net.openstack.Core.Domain
{
    /// <summary>
    /// Provides the details of an object stored in an Object Storage provider.
    /// </summary>
    [DataContract]
    public class ContainerObject
    {
        /// <summary>
        /// Gets a "name" associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/serialized-list-output.html">Serialized List Output (OpenStack Object Storage API v1 Reference)</seealso>
        [DataMember]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the "hash" value associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/serialized-list-output.html">Serialized List Output (OpenStack Object Storage API v1 Reference)</seealso>
        [DataMember]
        public Guid Hash { get; private set; }

        /// <summary>
        /// Gets the "bytes" value associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/serialized-list-output.html">Serialized List Output (OpenStack Object Storage API v1 Reference)</seealso>
        [DataMember]
        public long Bytes { get; private set; }

        /// <summary>
        /// Gets the "content type" value associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/serialized-list-output.html">Serialized List Output (OpenStack Object Storage API v1 Reference)</seealso>
        [DataMember(Name = "content_type")]
        public string ContentType { get; private set; }

        /// <summary>
        /// Gets the "last modified" value associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/serialized-list-output.html">Serialized List Output (OpenStack Object Storage API v1 Reference)</seealso>
        [DataMember(Name = "last_modified")]
        public DateTime LastModified { get; private set; }
    }
}
