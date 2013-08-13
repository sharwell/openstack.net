using System.Runtime.Serialization;

namespace net.openstack.Core.Domain
{
    /// <summary>
    /// Provides basic information about a flavor. A flavor is an available hardware configuration for a server.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-compute/2/content/Flavors-d1e4180.html">Flavors (OpenStack Compute API v2 and Extensions Reference - API v2)</seealso>
    /// <seealso href="http://docs.rackspace.com/servers/api/v2/cs-devguide/content/Flavors-d1e4180.html">Flavors (Rackspace Next Generation Cloud Servers Developer Guide  - API v2)</seealso>
    [DataContract]
    public class Flavor
    {
        /// <summary>
        /// Gets the unique identifier for the flavor.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Gets the "links" property associated with the flavor.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        [DataMember]
        public Link[] Links { get; set; }

        /// <summary>
        /// Gets the name of the flavor.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}
